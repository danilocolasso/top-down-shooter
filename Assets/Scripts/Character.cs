using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed, rotationSpeed, jumpSpeed, despawnTime;
    [SerializeField]
    protected int maxHealth, health;

    [SerializeField]
    protected GameObject blood;
    [SerializeField]
    private HealthBar healthBar;

    protected virtual void Start()
    {
        healthBar.SetHealth(maxHealth, health);
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
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
        Instantiate(blood, transform.position, transform.rotation * blood.transform.rotation).GetComponent<ParticleSystem>().Play();
    }
}
