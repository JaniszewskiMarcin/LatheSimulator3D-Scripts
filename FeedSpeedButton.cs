using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedSpeedButton : MonoBehaviour
{
    [SerializeField] GameObject feedSpeedKnifeUI;
    [SerializeField] GameObject target;
    [SerializeField] GameObject feedSpeedButtonText;
    [SerializeField] Text rotationSpeed;
    [SerializeField] Text result;


    public static bool isSpeedKnifeInfoUION = false;
    public static float resultNumber;

    private void Update()
    {

        rotationSpeed.text = Mathf.Abs(PlaySystem.cylinderSpinningSpeed).ToString();

        resultNumber = Mathf.Abs(((2f * target.GetComponent<TurningController>().height * 3.14f * PlaySystem.cylinderSpinningSpeed) / 1000f) * 16.6f);

        result.text = resultNumber.ToString("0.0");
    }

    private void OnMouseOver()
    {
        if(!GameManager.isErrorOn)
        {
            feedSpeedButtonText.SetActive(true);
        }

        if(Input.GetKey(KeyCode.E))
        {
            InformationUI.ShowOrHideCursor(true);
            if(!feedSpeedKnifeUI.activeSelf)
            {
                isSpeedKnifeInfoUION = true;
                feedSpeedKnifeUI.SetActive(true);
            }
        }
    }

    private void OnMouseExit()
    {
        feedSpeedButtonText.SetActive(false);
    }

    public void ResetBoolOnExit()
    {
        isSpeedKnifeInfoUION = false;
        InformationUI.ShowOrHideCursor(false);
    }
}
