using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class CharacterInfoBase
    {
        [SerializeField] int health;
        [SerializeField] float speed;

        public float Speed
        {
            get
            {
                return speed;
            }
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
