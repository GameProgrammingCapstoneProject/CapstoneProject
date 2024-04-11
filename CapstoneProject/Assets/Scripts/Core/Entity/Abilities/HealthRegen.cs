using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    private Player _owner;
    public void Setup(Player player)
    {
        _owner = player;
    }

    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
    private void Update()
    {
        transform.position = _owner.transform.position;
    }
}
