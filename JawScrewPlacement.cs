using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawScrewPlacement : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject scrubText;
    Vector3 keyStartPos;
    Quaternion keyStartRot;
    public static bool rotatingScrew = false;
    public static bool unRotatingScrew = false;
    bool hasPlayed = false;

    AudioSource keyUp;
    [SerializeField] AudioClip keyUpClip;

    private void Start()
    {
        keyUp = gameObject.AddComponent<AudioSource>();
        keyUp.clip = keyUpClip;

        keyStartPos = key.transform.position;
        keyStartRot = key.transform.rotation;
    }

    private void Update()
    {
        MakeUnselectableWhenMoving();
    }

    private void OnMouseOver()
    {
        if(!GameManager.isErrorOn)
        {
            scrubText.SetActive(true);
        }

        OnJawsScrewClick();
    }

    private void OnMouseExit()
    {
        if (keyUp.isPlaying == true)
        {
            keyUp.Stop();
        }
        scrubText.SetActive(false);
        rotatingScrew = false;
        unRotatingScrew = false;
    }

    public void OnJawsScrewClick()
    {
        if (gameObject.tag == "selectable")
        {
            if (Input.GetKey("e") && CheckTheJaws.isAllJawOnPlace == false)
            {
                if(keyUp.isPlaying == false && hasPlayed == false)
                {
                    keyUp.Play();
                    hasPlayed = true;
                }
                key.transform.position = parent.transform.position;
                key.transform.rotation = parent.transform.rotation;
                parent.transform.Rotate(0f, 0f, 100f * Time.deltaTime);
                rotatingScrew = true;
            }

            if (Input.GetKeyUp("e"))
            {
                hasPlayed = false;
                if (keyUp.isPlaying == true)
                {
                    keyUp.Stop();
                }
                rotatingScrew = false;
                key.transform.position = keyStartPos;
                key.transform.rotation = keyStartRot;
            }

            if (Input.GetKey("q") && CheckTheJaws.isJawOnStart1 == false)
            {
                if (keyUp.isPlaying == false && hasPlayed == false)
                {
                    keyUp.Play();
                    hasPlayed = true;
                }
                key.transform.position = parent.transform.position;
                key.transform.rotation = parent.transform.rotation;
                parent.transform.Rotate(0f, 0f, -100f * Time.deltaTime);
                unRotatingScrew = true;
            }

            if (Input.GetKeyUp("q"))
            {
                hasPlayed = false;
                if (keyUp.isPlaying == true)
                {
                    keyUp.Stop();
                }
                unRotatingScrew = false;
                key.transform.position = keyStartPos;
                key.transform.rotation = keyStartRot;
            }
        }
    }

    public void MakeUnselectableWhenMoving()
    {
        if (TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true))
        {
            gameObject.tag = "Moving";
        }
        else
        {
            gameObject.tag = "selectable";

        }
    }
}
