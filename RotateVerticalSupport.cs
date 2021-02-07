using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateVerticalSupport : MonoBehaviour
{
    [SerializeField] GameObject rotateVerText;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject target;
    [SerializeField] GameObject parentSupport;
    [SerializeField] GameObject chiselPos;
    [SerializeField] GameObject materialObj;
    Vector3 rotateObjectVerCenter;
    Vector3 maxYPos;

    AudioSource aSrc;
    [SerializeField] AudioClip rotateSmall;
    private void Start()
    {
        aSrc = gameObject.AddComponent<AudioSource>();
        aSrc.clip = rotateSmall;
        aSrc.volume = 0.2f;
    }

    private void OnMouseOver()
    {
        if (!GameManager.isErrorOn)
        {
            rotateVerText.SetActive(true);
        }

        rotateObjectVerCenter = gameObject.GetComponent<Collider>().bounds.center;

        Vector3 distancePointEdge = parent.transform.position - chiselPos.transform.position;

        if (CylinderRotatingLeaver.isRotatingLeaverOff == true || PlaySystem.inDrilling == true)
        {
            if (chiselPos.transform.position.x >= (-target.GetComponent<TurningController>().width - 0.079f))
            {
                maxYPos.y = TurningChisel.ClampPositionYStatic + distancePointEdge.y - target.GetComponent<TurningController>().height - 0.001f;
            }
            else
            {
                maxYPos.y = TurningChisel.ClampPositionYStatic + distancePointEdge.y;
            }
        }

        if (TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true) && PlaySystem.inDrilling == false)
        {
            maxYPos.y = TurningChisel.ClampPositionYStatic + distancePointEdge.y;
        }

        if ((Input.GetKey("e") && SupportFuncs.collideWithMaterial == false && FeedLeaverConfirm.isFeedConfirmLeaverOnPlace == false) || (Input.GetKey("e") && FeedLeaverConfirm.isFeedConfirmLeaverOnPlace == true && FeedLeaver.isFeedLeaverOnPlace != false && SupportFuncs.collideWithMaterial == false))
        {
            parent.transform.position = new Vector3(parent.transform.position.x, Mathf.Clamp(parent.transform.position.y + ChiselMovement.maxDragSpeedStatic * Time.deltaTime, GameManager.knifeSupportPosition.y, maxYPos.y), parent.transform.position.z);
            if (parent.transform.position.y < maxYPos.y)
            {
                if (aSrc.isPlaying == false)
                {
                    aSrc.Play();
                }
                transform.RotateAround(rotateObjectVerCenter, new Vector3(0f, -1f, 0f), ChiselMovement.maxDragSpeedStatic * Time.deltaTime * 5000f);
            }
            else
            {
                if (aSrc.isPlaying)
                {
                    aSrc.Stop();
                }
            }
        }

        else if ((Input.GetKey("q") && FeedLeaverConfirm.isFeedConfirmLeaverOnPlace == false) || (Input.GetKey("q") && FeedLeaverConfirm.isFeedConfirmLeaverOnPlace == true && FeedLeaver.isFeedLeaverOnPlace != false))
        {
            parent.transform.position = new Vector3(parent.transform.position.x, Mathf.Clamp(parent.transform.position.y - ChiselMovement.maxDragSpeedStatic * Time.deltaTime, GameManager.knifeSupportPosition.y, maxYPos.y), parent.transform.position.z);
            if (parent.transform.position.y > GameManager.knifeSupportPosition.y)
            {
                if (aSrc.isPlaying == false)
                {
                    aSrc.Play();
                }
                transform.RotateAround(rotateObjectVerCenter, new Vector3(0f, 1f, 0f), ChiselMovement.maxDragSpeedStatic * Time.deltaTime * 5000f);
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
        rotateVerText.SetActive(false);
        if(aSrc.isPlaying)
        {
            aSrc.Stop();
        }
    }
}
