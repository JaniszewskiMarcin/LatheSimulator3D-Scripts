using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnBut : MonoBehaviour
{
    [SerializeField] GameObject turnOnButtonText;

    public static bool isTurnOnButtonOn = false;
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
        if(!GameManager.isErrorOn)
        {
            turnOnButtonText.SetActive(true);
        }
        ClickOnButton();
    }

    private void OnMouseExit()
    {
        turnOnButtonText.SetActive(false);
    }

    public void ClickOnButton()
    {
        if (Input.GetKeyDown("e") && isTurnOnButtonOn == false && TurnOnPowerBut.isTurnOnPowerButtonOn == true)
        {
            isTurnOnButtonOn = true;
            Debug.Log("Uruchomiles tokarke!");
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
