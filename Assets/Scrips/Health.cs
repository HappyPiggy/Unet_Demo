using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;


   public void GetDamage(int damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {

            currentHealth = 0;
            print("Dead");
        }
    
    }
}
