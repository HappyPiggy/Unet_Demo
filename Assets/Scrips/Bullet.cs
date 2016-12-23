using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    void OnCollisionEnter(Collision col) {
        Health health = col.gameObject.GetComponent<Health>();
        health.GetDamage(10);

        Destroy(gameObject);
    
    }
}
