using System;
using System.Collections;
using System.Collections.Generic;
using Core.Components;
using Core.Entity;
using Core.Gameplay;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class CollisionComponent : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider2D _collider;
    [SerializeField] 
    private RigidbodyComponent _rb;
    [SerializeField]
    private LayerMask _groundLayerMask;
    public bool CanInteract { get; private set; }
    public IInteractable targetItem { get; private set; }
    private readonly float _offsetGroundCheck = 0.1f;
    private readonly float _offsetWallCheck = 0.3f;
    public GameObject attackArea;
    public float attackRadius;
    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Item>()) return;
        Item item = other.GetComponent<Item>();
        IInteractable interactableItem = item.GetComponent<IInteractable>();
        if (interactableItem == null || item.haveBeenInteracted) return;
        CanInteract = true;
        targetItem = interactableItem;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.GetComponent<Item>()) return;
        Item item = other.GetComponent<Item>();
        if (item.haveBeenInteracted)
            CanInteract = false;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.GetComponent<Item>()) return;
        IInteractable interactableItem = other.gameObject.GetComponent<IInteractable>();
        if (interactableItem == null) return;
        CanInteract = false;
        targetItem = null;
    }

    public bool IsStandingOnGround()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, Mathf.Abs(_collider.size.y / 2 + Mathf.Abs(_collider.offset.y) + _offsetGroundCheck), _groundLayerMask);
    }
    public bool IsInteractingWithWall()
    {
        int facingDirection = _rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT ? 1 : -1;
        return Physics2D.Raycast(this.transform.position, Vector2.right * facingDirection, Mathf.Abs(_collider.size.x / 2 + Mathf.Abs(_collider.offset.x) + _offsetWallCheck), _groundLayerMask);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackArea.transform.position, attackRadius);
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - _collider.size.y / 2 + _collider.offset.y + _offsetGroundCheck));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _collider.size.x / 2 + _collider.offset.x + _offsetWallCheck, transform.position.y));
    }
}
