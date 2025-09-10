using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    AudioSource diceSound;
    void Start()
    {
        diceSound = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        diceSound.Play();
    }
}
