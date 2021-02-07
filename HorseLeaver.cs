using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class HorseLeaver : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject horseLeaverText;

    public static bool isHorseLeaverOnPlace = false;

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
        if (!GameManager.isErrorOn)
        {
            horseLeaverText.SetActive(true);
        }
        ChangeLeaverPos();
    }

    private void OnMouseExit()
    {
        horseLeaverText.SetActive(false);
    }

    public void ChangeLeaverPos()
    {
        if (Input.GetKeyDown("e") && isHorseLeaverOnPlace == false)
        {
            audioSource.Play();
            parent.transform.Rotate(0f, 90f, 0f);
            isHorseLeaverOnPlace = true;
        }

        if (Input.GetKeyDown("q") && isHorseLeaverOnPlace == true)
        {
            audioSource.Play();
            parent.transform.Rotate(0f, -90f, 0f);
            isHorseLeaverOnPlace = false;
        }
    }
}
