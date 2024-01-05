using System.Collections;
using System.Collections.Generic;using Core.Entity;
using UnityEngine;

public class EntityAnimationTrigger<T> : MonoBehaviour where T : Entity
{
    protected T _theEntity => GetComponentInParent<T>();
}
