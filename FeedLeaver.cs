using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedLeaver : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject feedDirLeaver;

    public static bool isFeedLeaverOnPlace = false;  // false - support, true - wholeSupport
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
            feedDirLeaver.SetActive(true);
        }
        ChangeLeaverPos();
    }

    private void OnMouseExit()
    {
        feedDirLeaver.SetActive(false);
    }

    public void ChangeLeaverPos()
    {
        if (Input.GetKeyDown("e") && isFeedLeaverOnPlace == false && FeedLeaverConfirm.isFeedConfirmLeaverOnPlace == false)
        {
            audioSource.Play();
            parent.transform.Rotate(0f, 45f, 0f);
            isFeedLeaverOnPlace = true;
        }

        if (Input.GetKeyDown("q") && isFeedLeaverOnPlace == true && FeedLeaverConfirm.isFeedConfirmLeaverOnPlace == false)
        {
            audioSource.Play();
            parent.transform.Rotate(0f, -45f, 0f);
            isFeedLeaverOnPlace = false;
        }

    }
}
