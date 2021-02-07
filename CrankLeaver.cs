using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankLeaver : MonoBehaviour
{
    [SerializeField] GameObject crankLeaverText;
    [SerializeField] GameObject parent;

    public static bool isCrankLeaverOnPlace = false;

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
            crankLeaverText.SetActive(true);
        }
        ChangeLeaverPos();
    }

    private void OnMouseExit()
    {
        crankLeaverText.SetActive(false);
    }

    public void ChangeLeaverPos()
    {
        if (Input.GetKeyDown("e") && isCrankLeaverOnPlace == false)
        {
                audioSource.Play();
            parent.transform.Rotate(45f, 0f, 0f);
            isCrankLeaverOnPlace = true;
        }

        if (Input.GetKeyDown("q") && isCrankLeaverOnPlace == true)
        {
                audioSource.Play();
            parent.transform.Rotate(-45f, 0f, 0f);
            isCrankLeaverOnPlace = false;
        }
    }
}
