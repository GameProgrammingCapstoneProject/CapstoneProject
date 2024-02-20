using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damage);
    public void DoDamage(int damage, Character target);
}
