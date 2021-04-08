using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    public float damage;

    void OnEnable()
    {
        // Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
        if (other.gameObject.layer == (int) Layers.Character)
        {
            DoDamage(other.gameObject);
        }
    }
    

    void DoDamage(GameObject gameObject)
    {
        gameObject.GetComponent<Character>().TakeDamage(damage);
    }
}

enum Layers {
    Character = 3,
}
