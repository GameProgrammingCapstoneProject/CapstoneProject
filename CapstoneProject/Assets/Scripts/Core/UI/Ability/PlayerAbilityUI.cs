using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityUI : MonoBehaviour
{
    public DashAbility DashAbility;
    public ShieldAbility ShieldAbility;
    public BowShootingAbility BowShootingAbility;
    public HealthRegenAbility HealthRegenAbility;
    public ProjectileShootingAbility ProjectileShootingAbility;
    public LightningStrikeAbility LightningStrikeAbility;
    [SerializeField]
    private Image _firstAbilitySlot;
    [SerializeField]
    private Image _secondAbilitySlot;
    [SerializeField]
    private Image _firstAbilityCooldownImage;
    [SerializeField]
    private Image _secondAbilityCooldownImage;

    private PlayerAbility _firstPlayerAbility;
    private PlayerAbility _secondPlayerAbility;
    
    private void Start()
    {
        PlayerAbilityComponent.OnCurrentAbilitiesChanged += UpdateCurrentSetupAbilityUI;
        DashAbility.OnDashAbilityCoolDown += SetCoolDownOfDashAbility;
        ShieldAbility.OnShieldAbilityCoolDown += SetCoolDownOfShieldAbility;
        BowShootingAbility.OnBowShootingAbilityCoolDown += SetCoolDownOfBowShootingAbility;
        HealthRegenAbility.OnHealthRegenAbilityCoolDown += SetCoolDownOfHealthRegenAbility;
        ProjectileShootingAbility.OnProjectileShootingAbilityCoolDown += SetCoolDownOfProjectileShootingAbility;
        LightningStrikeAbility.OnLightningStrikeAbilityCoolDown += SetCoolDownOfLightningStrikeAbility;
    }

    private void Update()
    {
        if (_firstPlayerAbility != null)
            CheckCoolDownOf(_firstAbilityCooldownImage, _firstPlayerAbility.GetCooldown());
        if (_secondPlayerAbility != null)
            CheckCoolDownOf(_secondAbilityCooldownImage, _secondPlayerAbility.GetCooldown());
    }

    private void UpdateCurrentSetupAbilityUI(PlayerAbility ability, int index)
    {
        if (index == 0)
        {
            _firstPlayerAbility = ability;
            _firstAbilitySlot.enabled = true;
            _firstAbilitySlot.sprite = ability.abilityIcon;
            _firstAbilityCooldownImage.sprite = ability.abilityIcon;
        }
        else
        {
            _secondPlayerAbility = ability;
            _secondAbilitySlot.enabled = true;
            _secondAbilitySlot.sprite = ability.abilityIcon;
            _secondAbilityCooldownImage.sprite = ability.abilityIcon;
        }
    }
    public void CheckCoolDownOf(Image image, float cooldown)
    {
        if (image.fillAmount > 0)
        {
            image.fillAmount -= 1 / cooldown * Time.deltaTime;
        }
    }
    private void SetCoolDownOfDashAbility(DashAbility ability)
    {
        SetCoolDownOfAbility(ability);
    }
    private void SetCoolDownOfShieldAbility(ShieldAbility ability)
    {
        SetCoolDownOfAbility(ability);
    }
    private void SetCoolDownOfHealthRegenAbility(HealthRegenAbility ability)
    {
        SetCoolDownOfAbility(ability);
    }
    private void SetCoolDownOfBowShootingAbility(BowShootingAbility ability)
    {
        SetCoolDownOfAbility(ability);
    }
    private void SetCoolDownOfProjectileShootingAbility(ProjectileShootingAbility ability)
    {
        SetCoolDownOfAbility(ability);
    }
    private void SetCoolDownOfLightningStrikeAbility(LightningStrikeAbility ability)
    {
        SetCoolDownOfAbility(ability);
    }
    private void SetCoolDownOfAbility<T>(T ability) where T : PlayerAbility
    {
        if (ability == _firstPlayerAbility)
        {
            if (_firstAbilityCooldownImage.fillAmount <= 0)
            {
                _firstAbilityCooldownImage.enabled = true;
                _firstAbilityCooldownImage.fillAmount = 1;
            }
        }
        else if (ability == _secondPlayerAbility)
        {
            if (_secondAbilityCooldownImage.fillAmount <= 0)
            {
                _secondAbilityCooldownImage.enabled = true;
                _secondAbilityCooldownImage.fillAmount = 1;
            }
        }
    }

    private void OnDestroy()
    {
        PlayerAbilityComponent.OnCurrentAbilitiesChanged -= UpdateCurrentSetupAbilityUI;
        DashAbility.OnDashAbilityCoolDown -= SetCoolDownOfDashAbility;
        ShieldAbility.OnShieldAbilityCoolDown -= SetCoolDownOfShieldAbility;
        BowShootingAbility.OnBowShootingAbilityCoolDown -= SetCoolDownOfBowShootingAbility;
        HealthRegenAbility.OnHealthRegenAbilityCoolDown -= SetCoolDownOfHealthRegenAbility;
        ProjectileShootingAbility.OnProjectileShootingAbilityCoolDown -= SetCoolDownOfProjectileShootingAbility;
        LightningStrikeAbility.OnLightningStrikeAbilityCoolDown -= SetCoolDownOfLightningStrikeAbility;
    }
}
