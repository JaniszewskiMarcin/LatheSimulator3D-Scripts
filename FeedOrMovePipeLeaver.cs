using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedOrMovePipeLeaver : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject feedOrPipeText;

    public static bool isThreadPipeRunning = false;

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
            feedOrPipeText.SetActive(true);
        }
        ChangeLeaverPos();
    }

    private void OnMouseExit()
    {
        feedOrPipeText.SetActive(false);
    }

    public void ChangeLeaverPos()
    {
        if (Input.GetKeyDown("e") && isThreadPipeRunning == false)
        {
            audioSource.Play();
            parent.transform.Rotate(0f, -40f, 0f);
            isThreadPipeRunning = true;
        }

        if (Input.GetKeyDown("q") && isThreadPipeRunning == true)
        {
            audioSource.Play();
            parent.transform.Rotate(0f, 40f, 0f);
            isThreadPipeRunning = false;
        }
    }
}
