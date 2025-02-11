using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class CharacterInfoBase
    {
        [SerializeField] int health = 0;
        [SerializeField] int maxHealth = 0;
        [SerializeField] float speed = 0;

        #region C'tor
        public CharacterInfoBase() { }

        public CharacterInfoBase(CharacterInfoBase clone)
        {
            health = clone.health;
            maxHealth = clone.maxHealth;
            speed = clone.speed;
        }

        #endregion C'tor

        public float MaxHealth
        {
            get
            {
                return maxHealth;
            }
        }
        public float Health
        {
            get
            {
                return health;
            }
        }

        public float Speed
        {
            get
            {
                return speed;
            }
        }

        public void InitializePlayer()
        {
            health = 10;
            maxHealth = 10;
            speed = 5;
        }

        public int ReceiveDamage(int damage)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
            }

            return health;
        }
    }
}
