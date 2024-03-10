using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Cinemachine;
using Core.Components;
using TMPro;

[RequireComponent(typeof(RigidbodyComponent))]
public class CharacterEffect : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    public float flashDuration = 0.2f;
    [SerializeField]
    private Material _hitMat;
    
    private Material _originalMat;

    [Header("Screen shake FX")]
    [SerializeField]
    private CinemachineImpulseSource _screenShake;
    [SerializeField]
    private float _shakeMultiplier;
    [SerializeField]
    private Vector3 _shakePower;
    
    [Header("Hit FX")]
    [SerializeField] private GameObject _hitFX01Prefab;
    [SerializeField] private GameObject _hitFX02Prefab;

    /*[Header("Slash FX")]
    [SerializeField] private GameObject _slashFX01Prefab;
    [SerializeField] private GameObject _slashFX02Prefab;*/
    

    [SerializeField]
    private RigidbodyComponent _rb;
    private void Start()
    {
        _originalMat = _spriteRenderer.material;
    }

    public IEnumerator FlashFX()
    {
        _spriteRenderer.material = _hitMat;
        yield return new WaitForSeconds(flashDuration);
        _spriteRenderer.material = _originalMat;
    }

    public void BlinkRedColor()
    {
        if (_spriteRenderer.color != Color.white)
            _spriteRenderer.color = Color.white;
        else
            _spriteRenderer.color = Color.red;
    }

    public void CancelRedBlink()
    {
        CancelInvoke();
        _spriteRenderer.color = Color.white;
    }

    public void ShakeScreen()
    {
        int shakingDirection = _rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT ? 1 : -1;
        _screenShake.m_DefaultVelocity =
            new Vector3(_shakePower.x * shakingDirection, _shakePower.y) * _shakeMultiplier;
        _screenShake.GenerateImpulse();
    }

    public void GenerateHitFX(Transform targetTransform)
    {
        float xPosition = UnityEngine.Random.Range(-0.5f, 0.5f);
        float yPosition = UnityEngine.Random.Range(-0.5f, 0.5f);
        float zRotation = UnityEngine.Random.Range(-90, 90);
        int whichHitFX = UnityEngine.Random.Range(0, 10);
        GameObject newHitFX = null;
        if (whichHitFX <= 5)
        {
            newHitFX = Instantiate(_hitFX01Prefab, targetTransform.position + new Vector3(xPosition, yPosition), Quaternion.identity);
            newHitFX.transform.Rotate(new Vector3(0,0, zRotation));
        }
        else
        {
            float yRotation = 0;
            zRotation = UnityEngine.Random.Range(-45, 45);
            if (GetComponent<RigidbodyComponent>().CurrentFacingDirection == RigidbodyComponent.FacingDirections.LEFT)
                yRotation = 180;
            Vector3 hitFXRotation = new Vector3(0, yRotation, zRotation);
            newHitFX = Instantiate(_hitFX02Prefab, targetTransform.position + new Vector3(xPosition, yPosition), Quaternion.identity);
            newHitFX.transform.Rotate(hitFXRotation);
        }
        Destroy(newHitFX, 0.5f);
    }
}
