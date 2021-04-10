using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    public int damage;
    [SerializeField]
    private GameObject collisionParticles;

    void OnEnable()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision other)
    {  
        collideParticles(other.gameObject);
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

    void collideParticles(GameObject gameObject)
    {
        if(gameObject.GetComponent<Character>())
            gameObject.GetComponent<Character>().Bleed();
        else
            Instantiate(collisionParticles, transform.position, transform.rotation * collisionParticles.transform.rotation).GetComponent<ParticleSystem>().Play();
    }
}

enum Layers {
    Character = 3,
}
