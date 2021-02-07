using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHorizontalSupport : MonoBehaviour
{
    [SerializeField] GameObject rotateHorText;
    [SerializeField] GameObject target;
    [SerializeField] GameObject materialObj;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject parentSupport;
    [SerializeField] GameObject objectSupportPos;
    [SerializeField] GameObject horsePos;
    [SerializeField] GameObject chiselPos;
    [SerializeField] GameObject slowMovementPosObject;
    [SerializeField] GameObject slowMovementPosMagnitudeObject;
    Vector3 actualHorsePos;
    Vector3 rotateObjectHorCenter;
    Vector3 distance;
    Vector3 maxXPos;
    Vector3 magnitudeSlowMovement;
    bool hasPlayed = false;

    AudioSource aSrc;
    [SerializeField] AudioClip rotateBig;

    private void Start()
    {
        aSrc = gameObject.AddComponent<AudioSource>();
        aSrc.clip = rotateBig;
    }

    private void Update()
    {
        magnitudeSlowMovement = slowMovementPosObject.transform.position - slowMovementPosMagnitudeObject.transform.position;
    }

    private void OnMouseOver()
    {
        if (!GameManager.isErrorOn)
        {
            rotateHorText.SetActive(true);
        }

        actualHorsePos = horsePos.transform.position;
        rotateObjectHorCenter = gameObject.GetComponent<Collider>().bounds.center;
        distance = parent.transform.position - parentSupport.transform.position;

        Vector3 distancePointEdge = parent.transform.position - chiselPos.transform.position;
        Vector3 checkPointSup = parentSupport.transform.position - objectSupportPos.transform.position;

        if (CylinderRotatingLeaver.isRotatingLeaverOff == true || PlaySystem.inDrilling == true)
        {
            if (chiselPos.transform.position.y >= -target.GetComponent<TurningController>().height)
            {
                maxXPos.x = -target.GetComponent<TurningController>().width - distance.x + distancePointEdge.x - 0.005f - 0.079f;
            }
            else
            {
                maxXPos.x = -0.234f - distance.x + distancePointEdge.x;
            }
        }

        if (TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true) && PlaySystem.inDrilling == false)
        {
            maxXPos.x = -0.227f - distance.x - distancePointEdge.x - magnitudeSlowMovement.x*2f;
        }

        if ((Input.GetKey("q") && SupportFuncs.collideWithMaterial == false && FeedLeaverConfirm.isFeedConfirmLeaverOnPlace == false) || (Input.GetKey("q") && FeedLeaverConfirm.isFeedConfirmLeaverOnPlace == true && FeedLeaver.isFeedLeaverOnPlace != true && SupportFuncs.collideWithMaterial == false))
        {
            parentSupport.transform.position = new Vector3(Mathf.Clamp(parentSupport.transform.position.x + ChiselMovement.maxDragSpeedStatic * Time.deltaTime, actualHorsePos.x + checkPointSup.x, maxXPos.x), parentSupport.transform.position.y, parentSupport.transform.position.z);

            if (parentSupport.transform.position.x < maxXPos.x)
            {
                if (aSrc.isPlaying == false)
                {
                    aSrc.Play();
                }
                transform.RotateAround(rotateObjectHorCenter, new Vector3(0f, -1f, 0f), ChiselMovement.maxDragSpeedStatic * Time.deltaTime * 500f);
            }
            else
            {
                if (aSrc.isPlaying)
                {
                    aSrc.Stop();
                }
            }
        }

        else if ((Input.GetKey("e") && FeedLeaverConfirm.isFeedConfirmLeaverOnPlace == false) || (Input.GetKey("e") && FeedLeaverConfirm.isFeedConfirmLeaverOnPlace == true && FeedLeaver.isFeedLeaverOnPlace != true))
        {
            parentSupport.transform.position = new Vector3(Mathf.Clamp(parentSupport.transform.position.x - ChiselMovement.maxDragSpeedStatic * Time.deltaTime, actualHorsePos.x + checkPointSup.x, maxXPos.x), parentSupport.transform.position.y, parentSupport.transform.position.z);

            if (parentSupport.transform.position.x > actualHorsePos.x + checkPointSup.x)
            {
                if (aSrc.isPlaying == false)
                {
                    aSrc.Play();
                }
                transform.RotateAround(rotateObjectHorCenter, new Vector3(0f, 1f, 0f), ChiselMovement.maxDragSpeedStatic * Time.deltaTime * 500f);
            }
            else
            {
                if (aSrc.isPlaying)
                {
                    aSrc.Stop();
                }
            }
        }
        else
        {
            if (aSrc.isPlaying)
            {
                aSrc.Stop();
            }
        }

    }

    private void OnMouseExit()
    {
        rotateHorText.SetActive(false);
        if (aSrc.isPlaying)
        {
            aSrc.Stop();
            hasPlayed = false;
        }
    }
}
