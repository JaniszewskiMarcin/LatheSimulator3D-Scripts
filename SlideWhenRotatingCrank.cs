using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideWhenRotatingCrank : MonoBehaviour     //Skrypt podłączony do "Horse Edge" odpowiada za wysunięcie części konika przy obracaniu kołem
{
    [SerializeField] Transform crankParent; //Cały element BackCrank
    [SerializeField] GameObject latheLength;
    [SerializeField] GameObject target;
    [SerializeField] GameObject drillTransform;
    GameObject drill;

    public float speed = 0.2f;
    float targetOffset = 0.0792f;
    private GameObject selectedObject;
    Vector3 edgePos;
    Vector3 startPos;
    Vector3 edgeEndPos;
    Vector3 distanceEdge;
    Vector3 distanceEdgeDrill;
    Vector3 actualEdgeDrill;

    public static bool stopRotating = false;    //Jeśli część konika jest minmalnie lub maksymalnie wychylona blokuj możliwość obracania

    private void Start()
    {
        distanceEdge = transform.position - crankParent.position;
        distanceEdgeDrill = drillTransform.transform.position - crankParent.position;
        startPos = transform.position;
    }

    private void Update()
    {
        if (PlaySystem.inDrilling)
        {
            drill = GameObject.FindObjectOfType<TurningDrill>().gameObject;
            actualEdgeDrill = drill.transform.GetChild(0).gameObject.transform.position - transform.position;
            distanceEdgeDrill = drillTransform.transform.position - transform.position;
        }

        CheckTheDistance();
        StartSlide();
    }

    public void StartSlide()    //Jeśli patrzymy się na koło i nim obracamy to wtedy wysuwamy część konika
    {
        edgePos = transform.position;

        if (CrankLeaver.isCrankLeaverOnPlace == true && PlaySystem.inTurning)
        {
                //edgePos = transform.position;

            selectedObject = GameObject.Find(SelectionManager.selectedObject);
            if (Input.GetKey("e") && selectedObject.name == "Crank")
            {
                edgePos.x += transform.right.x * speed * Time.deltaTime;
            }
            if (Input.GetKey("q") && selectedObject.name == "Crank")
            {
                edgePos.x -= transform.right.x * speed * Time.deltaTime;
            }

            //edgePos.x = Mathf.Clamp(edgePos.x, crankParent.position.x + distanceEdge.x, edgeEndPos.x);

            //transform.position = new Vector3(edgePos.x, transform.position.y, startPos.z);

            if (edgePos.x <= crankParent.position.x + distanceEdge.x || edgePos.x >= edgeEndPos.x)
            {
                stopRotating = true;
            }
            else
            {
                stopRotating = false;
            }
        }

        if (CrankLeaver.isCrankLeaverOnPlace == true && PlaySystem.inDrilling)
        {
            //edgePos = transform.position;

            selectedObject = GameObject.Find(SelectionManager.selectedObject);
            if (Input.GetKey("e") && selectedObject.name == "Crank")
            {
                edgePos.x += transform.right.x * speed * Time.deltaTime;
            }
            if (Input.GetKey("q") && selectedObject.name == "Crank")
            {
                edgePos.x -= transform.right.x * speed * Time.deltaTime;
            }

            //edgePos.x = Mathf.Clamp(edgePos.x, crankParent.position.x + distanceEdge.x, edgeEndPos.x);

            //transform.position = new Vector3(edgePos.x, transform.position.y, startPos.z);

            if (edgePos.x <= crankParent.position.x + distanceEdge.x || edgePos.x >= edgeEndPos.x)
            {
                stopRotating = true;
            }
            else
            {
                stopRotating = false;
            }
        }

        edgePos.x = Mathf.Clamp(edgePos.x, crankParent.position.x + distanceEdge.x, edgeEndPos.x);
        transform.position = new Vector3(edgePos.x, transform.position.y, startPos.z);
    }

    public void CheckTheDistance()
    {
        if (PlaySystem.inTurning)
        {
            edgeEndPos = latheLength.transform.position;

            if (target == null)
            {
                return;
            }
            else
            {
                if (TurningController.baseXpos.x < latheLength.transform.position.x)
                {
                    if (TurningController.baseXpos.x < crankParent.position.x + 1.35f)
                    {
                        edgeEndPos.x = TurningController.baseXpos.x;
                    }
                    else
                    {
                        edgeEndPos.x = crankParent.position.x + 1.35f;
                    }
                }
                else
                {
                    if (latheLength.transform.position.x < crankParent.position.x + 1.35f)
                    {
                        edgeEndPos.x = latheLength.transform.position.x;
                    }
                    else
                    {
                        edgeEndPos.x = crankParent.position.x + 1.35f;
                    }
                }
            }
        }

        if (PlaySystem.inDrilling)
        {
            if (TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true))
            {
                    if (TurningController.baseXposTop.x - distanceEdgeDrill.x - 0.01f < crankParent.position.x + 1.35f)
                    {
                        edgeEndPos.x = TurningController.baseXposTop.x - distanceEdgeDrill.x - 0.01f;
                    }
                    else
                    {
                        edgeEndPos.x = crankParent.position.x + 1.35f;
                    }
                    if (Vector3.zero.x - 0.20f - actualEdgeDrill.x < crankParent.position.x + 1.35f)
                    {
                        edgeEndPos.x = Vector3.zero.x - 0.20f - actualEdgeDrill.x;
                    }
                    else
                    {
                        edgeEndPos.x = crankParent.position.x + 1.35f;
                    }
            }

            else
            {
                if(target == null)
                {
                    if (Vector3.zero.x - 0.15f < crankParent.position.x + 1.35f)
                    {
                        edgeEndPos.x = Vector3.zero.x - 0.15f;
                    }
                    else
                    {
                        edgeEndPos.x = crankParent.position.x + 1.35f;
                    }
                }
                else
                {
                    if(Vector3.zero.x - 0.15f < crankParent.position.x + 1.35f)
                    {
                        edgeEndPos.x = Vector3.zero.x - 0.15f;
                    }
                    if(TurningController.baseXposTop.x - actualEdgeDrill.x < crankParent.position.x + 1.35f)
                    {
                        edgeEndPos.x = TurningController.baseXposTop.x - actualEdgeDrill.x - 0.01f;
                    }
                    else
                    {
                        edgeEndPos.x = crankParent.position.x + 1.35f;
                    }
                }
            }
        }
    }
}

