using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private Animator animator;
    private AudioSource audioSource;

    public bool canShoot;
    private bool timerOn = false;
    private float timer = 0.6f;

    public float bulletForce = 20f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        canShoot = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot && timer >= .4f)
        {
            Shoot();
        }
        if (timerOn)
        {
            timer += Time.deltaTime;
            if (timer >= .4f)
            {
                timerOn = false;
            }
        }
    }

    void Shoot()
    {
        audioSource.Play();
        animator.SetTrigger("gunShot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        timerOn = true;
        timer = 0.0f;
    }
}
