using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

namespace Core.Animation
{
    public class EntityAnimationTrigger<T> : MonoBehaviour where T : Entity.Entity
    {
        protected T _theEntity => GetComponentInParent<T>();
    }

}
