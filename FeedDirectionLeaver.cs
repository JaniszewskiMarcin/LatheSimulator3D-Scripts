using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedDirectionLeaver : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject directionFeedLeaver;

    public static bool isFeedDirectionLeaverOnPlace = false;

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
            directionFeedLeaver.SetActive(true);
        }
        ChangeLeaverPos();
    }

    private void OnMouseExit()
    {
        directionFeedLeaver.SetActive(false);
    }

    public void ChangeLeaverPos()
    {
        if (Input.GetKeyDown("e") && isFeedDirectionLeaverOnPlace == false && FeedLeaverConfirm.isFeedConfirmLeaverOnPlace == false)
        {
            audioSource.Play();
            parent.transform.Rotate(0f, -8f, 0f);
            isFeedDirectionLeaverOnPlace = true;
        }

        if (Input.GetKeyDown("q") && isFeedDirectionLeaverOnPlace == true && FeedLeaverConfirm.isFeedConfirmLeaverOnPlace == false)
        {
            audioSource.Play();
            parent.transform.Rotate(0f, 8f, 0f);
            isFeedDirectionLeaverOnPlace = false;
        }
    }
}
