using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedLeaverConfirm : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject leaverConfirmText;

    public static bool isFeedConfirmLeaverOnPlace = false;

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
            leaverConfirmText.SetActive(true);
        }
        ChangeLeaverPos();
    }

    private void OnMouseExit()
    {
        leaverConfirmText.SetActive(false);
    }

    //Metoda zmiany pozycji dźwigni oraz 
    //zmiennej statycznej informującej o zmianie położenia.
    public void ChangeLeaverPos()
    {
        //Sprawdzenie instrukcją isterującą if w której pozycji jest dźwignia
        //oraz czy został wciśnięty klawisz funkcyjny.
        if (Input.GetKeyDown("e") && isFeedConfirmLeaverOnPlace == false)
        {
            audioSource.Play();
            //Zmiana pozycji dźwigni.
            parent.transform.Rotate(0f, -20f, 0f);
            //Zmiana zmiennej statycznej.
            isFeedConfirmLeaverOnPlace = true;
        }

        if (Input.GetKeyDown("q") && isFeedConfirmLeaverOnPlace == true)
        {
            audioSource.Play();
            parent.transform.Rotate(0f, 20f, 0f);
            isFeedConfirmLeaverOnPlace = false;
        }
    }
}
