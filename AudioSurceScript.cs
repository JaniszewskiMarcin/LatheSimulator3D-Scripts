using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSurceScript : MonoBehaviour
{
    [SerializeField] public AudioSource powerButtonOn;
    [SerializeField] public AudioSource powerButtonOff;
    [SerializeField] public AudioSource spinningLeaver;
    [SerializeField] public AudioSource horse;
    [SerializeField] public AudioSource bigRotating;
    [SerializeField] public AudioSource smallRotating;
    [SerializeField] public AudioSource shortLeaver1;
    [SerializeField] public AudioSource shortLeaver2;
    [SerializeField] public AudioSource spinning1500;
    [SerializeField] public AudioSource spinning3000;
    [SerializeField] public AudioSource spinningSlow;
    [SerializeField] public AudioSource letOffKey;
    [SerializeField] public AudioSource getUpKey;
    [SerializeField] public AudioSource button1;
    [SerializeField] public AudioSource button2;
    [SerializeField] public AudioSource turningSound;
    [SerializeField] public AudioSource latheRunning;
    [SerializeField] public AudioSource startRotateSound;


    public void PlayPowerOnButton()
    {
        powerButtonOn.Play();
    }
}
