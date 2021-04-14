using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float coolDown;
    public float cameraShakeIntencity;
    public int maxBullets;
    public int maxReloads;
    public float reloadTime;
    private bool reloading = false;
    [SerializeField]
    private ParticleSystem muzzleFlash;
    private float lastShot;
    [SerializeField]
    private GameObject muzzle;
    private int bullets;
    private int reloads;
    [SerializeField]
    private UnityEngine.UI.Text bulletCounter;
    [SerializeField]
    private UnityEngine.UI.Text reloadCounter;
    private CinemachineCameraShaker shaker;

    void Start()
    {
        shaker = Camera.main.GetComponent<CinemachineCameraShaker>();

        bullets = maxBullets;
        reloads = maxReloads;
        bulletCounter.text = bullets.ToString();
        reloadCounter.text = reloads.ToString();
    }

    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time - lastShot > coolDown)
            {
                lastShot = Time.time;

                if (!IsInvoking("Shoot"))
                {
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        if (bullets <= 0)
        {
            if (reloads <= 0 || reloading)
            {
                return;
            }

            StartCoroutine(Reload());
            return;
        }

        shaker.ShakeCamera(cameraShakeIntencity);
        Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation * muzzleFlash.transform.rotation).GetComponent<ParticleSystem>().Play();

        GameObject bullet = Instantiate (bulletPrefab, muzzle.transform.position, muzzle.transform.rotation * bulletPrefab.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(muzzle.transform.forward * bulletSpeed, ForceMode.Impulse);

        UseBullet();
    }

    void UseBullet()
    {
        bullets--;
        bulletCounter.text = bullets.ToString();
    }

    protected virtual IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);

        bullets = maxBullets;
        bulletCounter.text = bullets.ToString();
        
        reloads--;
        reloadCounter.text = reloads.ToString();
        reloading = false;
    }
}
