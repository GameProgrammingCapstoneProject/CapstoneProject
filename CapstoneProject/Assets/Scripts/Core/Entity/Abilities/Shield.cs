using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float _existTime;
    private Player _owner;
    public void Setup(float existTime, Player player)
    {
        _existTime = existTime;
        _owner = player;
    }

    private void Update()
    {
        _existTime -= Time.deltaTime;
        if (_existTime < 0)
            Destroy(this.gameObject);
        transform.position = _owner.transform.position;
    }
}