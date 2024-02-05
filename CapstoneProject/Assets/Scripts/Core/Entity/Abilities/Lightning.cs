using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private BoxCollider2D _collider;

    private LightningStrikeAbility _ability;

    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
    
}
