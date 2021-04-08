using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject prefab;
    public float bulletSpeed;
    public float coolDown;
    private float lastShot;

    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time - lastShot > coolDown)
            {
                lastShot = Time.time;

                if (!IsInvoking("InstantiateBullet"))
                {
                    InstantiateBullet();
                }
            }
        }
    }

    void InstantiateBullet()
    {
        GameObject bullet = Instantiate (prefab, transform.position, transform.rotation * prefab.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}
