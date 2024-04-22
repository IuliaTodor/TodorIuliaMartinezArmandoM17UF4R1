using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GenshintImpact2
{

    public class Enemy : MonoBehaviour
    {
        public GameObject HealthBar;

        public float maxHealth;
        
        public float health;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void HandleDamage(float damageTaken)
        {
            if (health > 0)
            {
                health -= damageTaken;
                UIManager.instance.UpdateHealthBar(HealthBar.GetComponent<Slider>(), -damageTaken / maxHealth);

                if (health <= 0)
                {
                    health = 0;
                    // Death
                }
            }
        }

    }
}
