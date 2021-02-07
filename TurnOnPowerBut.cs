using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnPowerBut : MonoBehaviour
{
    [SerializeField] GameObject powerButtontext;
    public static bool isTurnOnPowerButtonOn = false;
    AudioSource powerButtonOn;
    AudioSource powerButtonOff;

    [SerializeField] AudioClip powerButtonOffClip;
    [SerializeField] AudioClip powerButtonOnClip;
    private void Start()
    {
        powerButtonOn = gameObject.AddComponent<AudioSource>();
        powerButtonOff = gameObject.AddComponent<AudioSource>();

        powerButtonOn.volume = 0.3f;
        powerButtonOff.volume = 0.3f;
    }

    private void OnMouseOver()
    {
        if (!GameManager.isErrorOn)
        {
            powerButtontext.SetActive(true);
        }
        ClickOnButton();
    }

    private void OnMouseExit()
    {
        powerButtontext.SetActive(false);
    }

    public void ClickOnButton()
    {
        if(Input.GetKeyDown("e") && isTurnOnPowerButtonOn == false)
        {
            powerButtonOn.clip = powerButtonOnClip;
            powerButtonOn.Play();
            isTurnOnPowerButtonOn = true;
            Debug.Log("Uruchomiles prad!");
            return;
        }

        if (Input.GetKeyDown("e") && isTurnOnPowerButtonOn == true)
        {
            powerButtonOff.clip = powerButtonOffClip;
            powerButtonOff.Play();
            isTurnOnPowerButtonOn = false;
            TurnOnBut.isTurnOnButtonOn = false;
            Debug.Log("Wylaczyles prad!");
            return;
        }
    }
}
