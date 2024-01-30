using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Attributes
{
    public class PlayerAttributes : MonoBehaviour
    {
        private int _currentHealth;
        private bool _isDead;
        private bool _isInvincible;
        public event System.Action OnHealthChanged;
        
    }
}

