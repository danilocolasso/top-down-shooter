using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth, currentHealth, moveSpeed, rotationSpeed, jumpSpeed, despawnTime;
    [SerializeField]
    protected GameObject blood;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Destroy(gameObject, despawnTime);
    }

    public void Bleed()
    {
        // TODO fix rotation
        Instantiate(blood, transform.position, transform.rotation * blood.transform.rotation).GetComponent<ParticleSystem>().Play();
    }
}
