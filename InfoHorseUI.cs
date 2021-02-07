using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHorseUI : MonoBehaviour
{
    [SerializeField] GameObject horseText;

    private void OnMouseOver()
    {
        if (!GameManager.isErrorOn)
        {
            horseText.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        horseText.SetActive(false);
    }
}
