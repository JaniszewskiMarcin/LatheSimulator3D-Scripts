using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffBut : MonoBehaviour
{
    [SerializeField] GameObject turnOffButtonText;

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
            turnOffButtonText.SetActive(true);
        }
        ClickOnButton();
    }

    private void OnMouseExit()
    {
        turnOffButtonText.SetActive(false);
    }

    public void ClickOnButton()
    {
        if (Input.GetKeyDown("e") && TurnOnBut.isTurnOnButtonOn == true && TurnOnPowerBut.isTurnOnPowerButtonOn == true)
        {
            TurnOnBut.isTurnOnButtonOn = false;
            Debug.Log("Wylaczyles tokarke!");
        }
        if (Input.GetKeyDown("e"))
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
