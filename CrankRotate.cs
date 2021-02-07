using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankRotate : MonoBehaviour
{
    [SerializeField] GameObject crankRotateText;
    private Collider colliderCrank;
    public float speed = 100f;


    AudioSource audioSource;
    [SerializeField] AudioClip rotateSmall;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = rotateSmall;
        colliderCrank = gameObject.GetComponent<Collider>();
        audioSource.volume = 0.2f;
    }

    void OnMouseOver()
    {
        if (!GameManager.isErrorOn)
        {
            crankRotateText.SetActive(true);
        }
        Rotate();
    }

    private void OnMouseExit()
    {
        crankRotateText.SetActive(false);
        if (audioSource.isPlaying == true)
        {
            audioSource.Stop();
        }
    }

    public void Rotate()
    {
        if(CrankLeaver.isCrankLeaverOnPlace == true)
        {

            if (Input.GetKey("e") && SlideWhenRotatingCrank.stopRotating == false)
            {
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                }
                transform.RotateAround(colliderCrank.bounds.center, new Vector3(-1f, 0f, 0f), speed * Time.deltaTime);
            }

            else if (Input.GetKey("q") && SlideWhenRotatingCrank.stopRotating == false)
            {
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                }
                transform.RotateAround(colliderCrank.bounds.center, new Vector3(1f, 0f, 0f), speed * Time.deltaTime);
            }
            else if(SlideWhenRotatingCrank.stopRotating == true || Input.GetKeyUp("q") || Input.GetKeyUp("e"))
            {
                if (audioSource.isPlaying == true)
                {
                    audioSource.Stop();
                }
            }
        }

    }
}
