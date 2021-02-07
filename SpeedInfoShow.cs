using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedInfoShow : MonoBehaviour
{
    [SerializeField] GameObject infoSpeedUI;
    [SerializeField] GameObject speedMechnismText;

    private void OnMouseOver()
    {
        if(!GameManager.isErrorOn)
        {
            speedMechnismText.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            infoSpeedUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }

    private void OnMouseExit()
    {
        speedMechnismText.SetActive(false);
    }

    public void ExitSpeedUI()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    
}
