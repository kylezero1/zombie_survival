using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    private AudioSource audioSource;

    public bool canShoot;

    public float bulletForce = 20f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canShoot = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && canShoot)
        Shoot();        
    }

    void Shoot()
    {
        audioSource.Play();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
