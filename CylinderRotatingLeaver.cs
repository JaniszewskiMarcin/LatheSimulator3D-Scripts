using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderRotatingLeaver : MonoBehaviour
{
    [SerializeField] GameObject leaverText;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject parentSupport;

    public static int counterLeaver = 1; // 0 - left, 1 - off, 2 - right

    public static bool isRightRotatingLeaverOn = false;
    public static bool isLeftRotatingLeaverOn = false;
    public static bool isRotatingLeaverOff = true;

    AudioSource audioSource;
    [SerializeField] AudioClip leaverSound;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = leaverSound;
        audioSource.volume = 0.6f;
    }

    private void OnMouseOver()
    {
        if(!GameManager.isErrorOn)
        {
            leaverText.SetActive(true);
        }
        ChangeLeaverPos();
    }

    private void OnMouseExit()
    {
        leaverText.SetActive(false);
    }

    public void ChangeLeaverPos()
    {
            if (Input.GetKeyDown(KeyCode.E) && counterLeaver + 1 >= 0 && counterLeaver + 1 <= 2)
            {
            audioSource.Play();
            parent.transform.Rotate(-20f, 0f, 0f);
                parentSupport.transform.Rotate(-20f, 0f, 0f);
                counterLeaver++;
                LeaverRoatatingDirection();
            }

            if (Input.GetKeyDown(KeyCode.Q) && counterLeaver - 1 >= 0 && counterLeaver - 1 <= 2)
            {
            audioSource.Play();
            parent.transform.Rotate(20f, 0f, 0f);
                parentSupport.transform.Rotate(20f, 0f, 0f);
                counterLeaver--;
                LeaverRoatatingDirection();
            }
    }

    public void LeaverRoatatingDirection()
    {
            if (counterLeaver == 0 && isLeftRotatingLeaverOn == false)
            {
                PlaySystem.cylinderSpinningSpeed *= -1;
                isRotatingLeaverOff = false;
                isRightRotatingLeaverOn = false;
                isLeftRotatingLeaverOn = true;
            }

            if (counterLeaver == 1 && isRotatingLeaverOff == false)
            {
                isRotatingLeaverOff = true;
                isRightRotatingLeaverOn = false;
                isLeftRotatingLeaverOn = false;
            }

            if (counterLeaver == 2 && isRightRotatingLeaverOn == false)
            {
                PlaySystem.cylinderSpinningSpeed *= -1;
                isRightRotatingLeaverOn = true;
                isLeftRotatingLeaverOn = false;
                isRotatingLeaverOff = false;
            }
    }

}
