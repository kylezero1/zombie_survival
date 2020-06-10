using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSoundController : MonoBehaviour
{
    private AudioSource deathSoundPlayer;

    public AudioClip character_death;
    public AudioClip zombie_death_1;
    public AudioClip zombie_death_2;

    // Start is called before the first frame update
    void Start()
    {
        deathSoundPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCharacterDeath()
    {
        deathSoundPlayer.PlayOneShot(character_death);
    }

    public void PlayZombieDeath()
    {
        float random = Random.Range(0f, 1f);
        if (random < .5f)
        {
            deathSoundPlayer.PlayOneShot(zombie_death_1);
        } else {
            deathSoundPlayer.PlayOneShot(zombie_death_2);
        }        
    }
}
