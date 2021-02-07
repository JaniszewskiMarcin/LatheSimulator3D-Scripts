using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffEmergencyBut : MonoBehaviour
{
    [SerializeField] GameObject turnOffEmergencyButtonText;

    AudioSource buttonDown;
    [SerializeField] AudioClip buttonDownClip;
    bool hasPlayed = false;

    private void Start()
    {
        buttonDown = gameObject.AddComponent<AudioSource>();
        buttonDown.clip = buttonDownClip;
        buttonDown.volume = 0.4f;
    }

    private void OnMouseOver()
    {
        if (!GameManager.isErrorOn)
        {
            turnOffEmergencyButtonText.SetActive(true);
        }
        ClickOnButton();
    }

    private void OnMouseExit()
    {
        turnOffEmergencyButtonText.SetActive(false);
    }

    public void ClickOnButton()
    {
        if (Input.GetKeyDown("e") && TurnOnPowerBut.isTurnOnPowerButtonOn == true)
        {
            TurnOnBut.isTurnOnButtonOn = false;
            TurnOnPowerBut.isTurnOnPowerButtonOn = false;
            Debug.Log("Wylaczyles wszystko!");
        }
        if(Input.GetKeyDown("e"))
        {
            if (buttonDown.isPlaying == false && hasPlayed == false)
            {
                buttonDown.Play();
                hasPlayed = true;
            }
        }
        if (Input.GetKeyUp("e"))
        {
            hasPlayed = false;
        }
    }
}
