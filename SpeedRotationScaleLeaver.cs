using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRotationScaleLeaver : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject speedLeaver;
    [SerializeField] GameObject leaverText;
    public static bool isSpeedRotationScaleOnPlace = false; //false - 1:1, true 1:8

    AudioSource audioSource;
    [SerializeField] AudioClip leaverSound;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = leaverSound;
        audioSource.volume = 0.4f;
    }

    private void OnMouseOver()
    {
        if(!GameManager.isErrorOn)
        {
            leaverText.SetActive(true);
        }

        if (CylinderRotatingLeaver.isRotatingLeaverOff == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && isSpeedRotationScaleOnPlace == false)
            {
                audioSource.Play();
                parent.transform.Rotate(0.0f, -60f, 0.0f);
                isSpeedRotationScaleOnPlace = true;
            }

            if (Input.GetKeyDown(KeyCode.Q) && isSpeedRotationScaleOnPlace == true)
            {
                audioSource.Play();
                parent.transform.Rotate(0.0f, 60f, 0.0f);
                isSpeedRotationScaleOnPlace = false;
            }

            speedLeaver.GetComponent<SpeedRotationLeaver>().ChangeSpeedRotation();
        }
    }

    private void OnMouseExit()
    {
        leaverText.SetActive(false);
    }
}
