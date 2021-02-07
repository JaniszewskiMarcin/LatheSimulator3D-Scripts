using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRotationLeaver : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject speedLeaver;

    int counter = 0; 
    int scale = 1;

    AudioSource audioSource;
    [SerializeField] AudioClip leaverSound;

    private void Start()
    {
        PlaySystem.cylinderSpinningSpeed = 200f / scale;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = leaverSound;
        audioSource.volume = 0.4f;
    }

    private void OnMouseOver()
    {
        if(!GameManager.isErrorOn)
        {
            speedLeaver.SetActive(true);
        }

        if (CylinderRotatingLeaver.isRotatingLeaverOff == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && counter + 1 >= 0 && counter + 1 <= 4)
            {
                audioSource.Play();
                parent.transform.Rotate(0.0f, -15f, 0.0f);
                counter++;
                ChangeSpeedRotation();
            }

            if (Input.GetKeyDown(KeyCode.Q) && counter - 1 >= 0 && counter - 1 <= 4)
            {
                audioSource.Play();
                parent.transform.Rotate(0.0f, 15f, 0.0f);
                counter--;
                ChangeSpeedRotation();
            }
        }
    }

    private void OnMouseExit()
    {
        speedLeaver.SetActive(false);
    }

    public void ChangeSpeedRotation()
    {
        if(SpeedRotationScaleLeaver.isSpeedRotationScaleOnPlace == false)
        {
            scale = 1;
        }
        else
        {
            scale = 8;
        }

        if(counter == 0)
        {
            PlaySystem.cylinderSpinningSpeed = 200f/scale;
        }
        if (counter == 1)
        {
            PlaySystem.cylinderSpinningSpeed = 400f/scale;
        }
        if (counter == 2)
        {
            PlaySystem.cylinderSpinningSpeed = 600f/scale;
        }
        if (counter == 3)
        {
            PlaySystem.cylinderSpinningSpeed = 800f/scale;
        }
        if (counter == 4)
        {
            PlaySystem.cylinderSpinningSpeed = 1200f/scale;
        }
    }

}
