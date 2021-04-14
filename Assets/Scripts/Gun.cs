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

    [SerializeField]
    protected AudioClip[] shootSounds;
    [SerializeField]
    protected AudioClip reloadSound;
    [SerializeField]
    protected AudioClip[] noBulletsSounds;

    [SerializeField]
    private ParticleSystem muzzleFlash;
    [SerializeField]
    private GameObject muzzle;
    [SerializeField]
    private UnityEngine.UI.Text bulletCounter;
    [SerializeField]
    private UnityEngine.UI.Text reloadCounter;
    private bool reloading = false;
    private float lastShot;
    private int bullets;
    private int reloads;
    private CinemachineCameraShaker shaker;
    private AudioSource audioSource;


    void Start()
    {
        shaker = Camera.main.GetComponent<CinemachineCameraShaker>();
        audioSource = GetComponent<AudioSource>();

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
        // Stop shooting when without bullets and reloads
        if (bullets <= 0 || reloading)
        {
            if (!reloading)
            {
                // Play random no bullets sound
                int index = Random.Range(0, noBulletsSounds.Length);
                audioSource.PlayOneShot(noBulletsSounds[index]);
            }
            
            return;
        }

        // Shake the camera
        shaker.ShakeCamera(cameraShakeIntencity);
        Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation * muzzleFlash.transform.rotation).GetComponent<ParticleSystem>().Play();

        // Fire!
        GameObject bullet = Instantiate (bulletPrefab, muzzle.transform.position, muzzle.transform.rotation * bulletPrefab.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(muzzle.transform.forward * bulletSpeed, ForceMode.Impulse);

        // Decrease bullets counter and reload if necessary
        UseBullet();
    }

    void UseBullet()
    {
        // Play random shot sound
        int index = Random.Range(0, shootSounds.Length);
        audioSource.PlayOneShot(shootSounds[index]);

        // Decrease bullet counter
        bullets--;
        bulletCounter.text = bullets.ToString();

        //Reload
        if (bullets <= 0 && reloads > 0)
        {
            StartCoroutine(Reload());
        }
    }

    protected virtual IEnumerator Reload()
    {
        reloading = true;
        
        // Play reloading sound
        audioSource.PlayOneShot(reloadSound);

        // Wait n seconds
        yield return new WaitForSeconds(reloadTime);

        // Reset bullets counter
        bullets = maxBullets;
        bulletCounter.text = bullets.ToString();

        // Decrease reloads counter
        reloads--;
        reloadCounter.text = reloads.ToString();
        reloading = false;
    }
}
