using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationUI : MonoBehaviour
{
    public Image energyButton;
    public Image powerOnOffButton;
    public GameObject tutorialPanel;
    public GameObject tutorialPanelInfoUI;

    public static bool turnOnTheTutorial = false;

    private void Start()
    {
        StartCoroutine(WaitToShowUpTutorial());
        energyButton.color = new Color(255f, 0f, 0f, 255f);
        powerOnOffButton.color = new Color(255f, 0f, 0f, 255f);
    }

    private void Update()
    {

        if (TurnOnPowerBut.isTurnOnPowerButtonOn)
        {
            energyButton.color = new Color(0f, 255f, 0f, 255f);
        }
        else
        {
            energyButton.color = new Color(255f, 0f, 0f, 255f);
        }


        if(TurnOnBut.isTurnOnButtonOn)
        {
            powerOnOffButton.color = new Color(0f, 255f, 0f, 255f);
        }
        else
        {
            powerOnOffButton.color = new Color(255f, 0f, 0f, 255f);
        }
    }

    public void AcceptTutorial()
    {
        ShowOrHideCursor(false);
    }

    public IEnumerator WaitToShowUpTutorial()
    {
        yield return new WaitForSeconds(0.5f);
        tutorialPanel.SetActive(true);
        ShowOrHideCursor(true);
    }

    public static void ShowOrHideCursor(bool show)
    {
        if(show)
        {
            if (!Cursor.visible)
            {
                Cursor.visible = true;
            }
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            if (Time.timeScale != 0f)
            {
                Time.timeScale = 0f;
            }
        }
        else
        {
            if (Cursor.visible)
            {
                Cursor.visible = false;
            }
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (Time.timeScale != 1f)
            {
                Time.timeScale = 1f;
            }
        }
    }
}
