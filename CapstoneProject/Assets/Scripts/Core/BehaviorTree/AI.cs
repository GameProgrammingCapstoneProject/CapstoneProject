using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using Core.Components;
using Core.Entity;

public class AI : MonoBehaviour
{
    Enemy enemy;
    RigidbodyComponent _rb;
    Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<RigidbodyComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Task]
    public void Roam()
    {
        _rb.SetVelocity( -1.0f, _rb.velocity.y);
        Task.current.Succeed();
    }
}
