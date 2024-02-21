using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Entity;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Lightning Strike Ability", fileName = "LightningStrikeAbility", order = 5)]
public class LightningStrikeAbility : PlayerAbility
{
    [SerializeField]
    private Lightning _lightningStrikePrefab;
    [SerializeField]
    private float _xOffset = 3f;
    public event System.Action<LightningStrikeAbility> OnLightningStrikeAbilityCoolDown;
    [HideInInspector]
    public List<GameObject> targets;
    [SerializeField]
    private float _activateTime = 6f;
    private bool _isActivated = false;
    private float elapsedTime = 0f;
    
    public override void AbilityUpdate()
    {
        base.AbilityUpdate();
        if (_isActivated)
        {
            elapsedTime += Time.deltaTime;
        }
    }
    protected override void Activate()
    {
        _isActivated = true;
        elapsedTime = 0;
        targets = AbilityHandler.ScanForEnemiesInArea().ToList();
        AbilityHandler.StartRoutine(this);
        OnLightningStrikeAbilityCoolDown?.Invoke(this);
    }

    public override IEnumerator ActivateRoutine()
    {
        if (targets.Count <= 0) yield break;
        while (elapsedTime < _activateTime && _isActivated)
        {
            yield return new WaitForSeconds(Random.Range(0f, 1f));
            GameObject randomTarget = targets[Random.Range(0, targets.Count)];
            GenerateLightning(randomTarget.transform.position.x + Random.Range(-_xOffset, _xOffset), randomTarget.transform.position.y);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
            randomTarget = targets[Random.Range(0, targets.Count)];
            GenerateLightning(randomTarget.transform.position.x + Random.Range(-_xOffset, _xOffset), randomTarget.transform.position.y);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
            randomTarget = targets[Random.Range(0, targets.Count)];
            GenerateLightning(randomTarget.transform.position.x + Random.Range(-_xOffset, _xOffset), randomTarget.transform.position.y);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
            randomTarget = targets[Random.Range(0, targets.Count)];
            GenerateLightning(randomTarget.transform.position.x + Random.Range(-_xOffset, _xOffset), randomTarget.transform.position.y);
        }
        _isActivated = false;
        elapsedTime = 0f;
    }
    
    private void GenerateLightning(float xPos, float yPos)
    {
        Vector3 lightningPosition = new Vector3(xPos, yPos + 2.5f, 0);
        Instantiate(_lightningStrikePrefab, lightningPosition, Quaternion.identity);
    }
}
