using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSupportMovement : MonoBehaviour
{
    [SerializeField] GameObject keySupportText;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject targetSpawn;
    [SerializeField] GameObject parentToMoveWithSupport;
    [SerializeField] GameObject parentToMoveWith;
    [SerializeField] GameObject steadyObjectToMeasureOffset;
    [SerializeField] float offset = 0.4f;

    Vector3 startPos;

    private void Update()
    {
        startPos = steadyObjectToMeasureOffset.transform.position;
    }

    private void OnMouseOver()
    {
        if(!GameManager.isErrorOn)
        {
            keySupportText.SetActive(true);
        }

        if(Input.GetKey(KeyCode.E) && parentToMoveWith.transform.position.x <= startPos.x + offset && parentToMoveWith.transform.position.x <= targetSpawn.transform.position.x)
        {
            parentToMoveWith.transform.position = new Vector3(parentToMoveWith.transform.position.x + ChiselMovement.maxDragSpeedStatic/100f * Time.deltaTime, parentToMoveWith.transform.position.y, parentToMoveWith.transform.position.z);
            parentToMoveWithSupport.transform.position = new Vector3(parentToMoveWithSupport.transform.position.x + ChiselMovement.maxDragSpeedStatic/100f * Time.deltaTime, parentToMoveWithSupport.transform.position.y, parentToMoveWithSupport.transform.position.z);
            transform.RotateAround(parent.transform.position, new Vector3(-1f, 0f, 0f), ChiselMovement.maxDragSpeedStatic * Time.deltaTime * 1000f);
        }

        if (Input.GetKey(KeyCode.Q) && parentToMoveWith.transform.position.x >= startPos.x)
        {
            transform.RotateAround(parent.transform.position, new Vector3(1f, 0f, 0f), ChiselMovement.maxDragSpeedStatic * Time.deltaTime * 1000f);
            parentToMoveWith.transform.position = new Vector3(parentToMoveWith.transform.position.x - ChiselMovement.maxDragSpeedStatic/100f * Time.deltaTime, parentToMoveWith.transform.position.y, parentToMoveWith.transform.position.z);
            parentToMoveWithSupport.transform.position = new Vector3(parentToMoveWithSupport.transform.position.x - ChiselMovement.maxDragSpeedStatic/100f * Time.deltaTime, parentToMoveWithSupport.transform.position.y, parentToMoveWithSupport.transform.position.z);
        }
    }

    private void OnMouseExit()
    {
        keySupportText.SetActive(false);
    }

}
