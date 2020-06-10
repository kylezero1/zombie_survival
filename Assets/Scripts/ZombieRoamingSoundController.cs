using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRoamingSoundController : MonoBehaviour
{
    private AudioSource audioSource;

    private float waitTime = 0.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        waitTime = Random.Range(5.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            audioSource.Play();
            waitTime = Random.Range(5.0f, 10.0f);
            timer = 0.0f;
        }
    }
}
