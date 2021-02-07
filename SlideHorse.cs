using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideHorse : MonoBehaviour     //Skrypt podłączony do całego konika o nazwie "BackCrank"
{
    public float speed = 1f;
    float targetOffset = 0.0792f;
    private GameObject selectedObject;
    private GameObject drill;
    private GameObject dirtParticle;

    [SerializeField] GameObject drillTransform;
    [SerializeField] GameObject target;
    [SerializeField] GameObject edge;
    [SerializeField] GameObject objectSupportPos;

    Vector3 horsePos;
    Vector3 startPos;
    Vector3 endHorsePos;
    Vector3 horseEndBySupportPos;
    Vector3 distanceEdge;
    Vector3 distanceEdgeDrill;
    Vector3 actualEdgeDrill;

    AudioSource audioSource;
    [SerializeField] AudioClip horseSlide;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.3f;
        audioSource.clip = horseSlide;
        distanceEdgeDrill = drillTransform.transform.position - transform.position;
        startPos = transform.position;
    }

    private void Update()
    {
        if(PlaySystem.inDrilling)
        {
            drill = GameObject.FindObjectOfType<TurningDrill>().gameObject;
            dirtParticle = drill.transform.GetChild(0).gameObject;
            actualEdgeDrill = dirtParticle.transform.position - transform.position;
            distanceEdgeDrill = drillTransform.transform.position - transform.position;
        }

        UpdateSupportPosAndDistance();
        UpdateHorseEndPos();
        StartSlide();                   //Funkcja obejmująca logikę przysuwania całego konika
    }
    
    public void StartSlide()
    {
        horsePos = transform.position;

        if (HorseLeaver.isHorseLeaverOnPlace == true)
        {
            selectedObject = GameObject.Find(SelectionManager.selectedObject);

            if (Input.GetKey("e") && selectedObject.name == "Horse")
            {
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                }
                horsePos.x += transform.right.x * speed * Time.deltaTime;
            }
            else if (Input.GetKey("q") && selectedObject.name == "Horse" )
            {
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                }
                horsePos.x -= transform.right.x * speed * Time.deltaTime;
            }
            else if(Input.GetKeyUp("q") || Input.GetKeyUp("e") || selectedObject.name != "Horse")
            {
                if (audioSource.isPlaying == true)
                {
                    audioSource.Stop();
                }
            }

            //horsePos.x = Mathf.Clamp(horsePos.x, startPos.x, endHorsePos.x);

            //transform.position = new Vector3(horsePos.x, transform.position.y, startPos.z);
        }

        horsePos.x = Mathf.Clamp(horsePos.x, startPos.x, endHorsePos.x);
        transform.position = new Vector3(horsePos.x, transform.position.y, startPos.z);

        if(transform.position.x <= startPos.x || transform.position.x >= endHorsePos.x)
        {
            if (audioSource.isPlaying == true)
            {
                audioSource.Stop();
            }
        }
       
    }

    public void UpdateSupportPosAndDistance()
    {
        horseEndBySupportPos = objectSupportPos.transform.position;
        distanceEdge = edge.transform.position - transform.position;
    }

    public void UpdateHorseEndPos()
    {
        if(PlaySystem.inTurning == true)
        {
            endHorsePos.x = horseEndBySupportPos.x; //jeśli nie ma materiału obrabianego konik zatrzymuje się przy supporcie

            if (target == null)
            {
                return;
            }
            else
            {
                if (horseEndBySupportPos.x < TurningController.baseXpos.x - distanceEdge.x)
                {
                    endHorsePos.x = horseEndBySupportPos.x;
                }
                else
                {
                    endHorsePos.x = TurningController.baseXpos.x - distanceEdge.x;
                }
            }
        }

        if(PlaySystem.inDrilling == true)
        {
            if(TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true) && -TurningController.baseXpos.x - 0.079f <= 1f)
            {
                if (target == null)
                {
                    endHorsePos.x = horseEndBySupportPos.x;
                }
                else
                {
                    if (TurningController.baseXposTop.x - distanceEdgeDrill.x - 0.01f < horseEndBySupportPos.x)
                    {
                        endHorsePos.x = TurningController.baseXposTop.x - 0.01f - distanceEdgeDrill.x;
                    }
                    else
                    {
                        endHorsePos.x = horseEndBySupportPos.x;
                    }
                }
            }
            else
            {
                if (target == null)
                {
                    endHorsePos.x = horseEndBySupportPos.x;
                }
                else
                {
                    if (TurningController.baseXposTop.x - actualEdgeDrill.x - 0.01f < horseEndBySupportPos.x)
                    {
                        endHorsePos.x = TurningController.baseXposTop.x - actualEdgeDrill.x - 0.01f;
                    }
                    else
                    {
                        endHorsePos.x = horseEndBySupportPos.x;
                    }
                }
            }
        }
    }

}
