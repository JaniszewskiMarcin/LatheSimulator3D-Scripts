using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTheJaws : MonoBehaviour   //Skrypt podłączony do każdej szczęki, obejmuje ich ruch po wprowadzeniu materiału
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject parentPos;
    [SerializeField] public float speed = 0.1f;

    public static bool isAllJawOnPlace = false;
    public static bool isAllJawOnStart = false;

    public static bool isJawOnPlace1 = false;
    public static bool isJawOnPlace2 = false;
    public static bool isJawOnPlace3 = false;

    public static bool isJawOnStart1 = false;
    public static bool isJawOnStart2 = false;
    public static bool isJawOnStart3 = false;

    void FixedUpdate()   //Funkcja analizująca jeśli patrzymy na śrubke do obracania szczęk, to dociskamy szczęki, jeśli spełnione są warunki
    {
        if(target == null)
        {
            StartPosJaws();
        }

            if (JawScrewPlacement.rotatingScrew == true)
            {
                if (isJawOnPlace1 == false && gameObject.name == "Jaw1")
                {
                    parent.transform.position = Vector3.MoveTowards(parent.transform.position, new Vector3(parent.transform.position.x, 0f, 0f), speed * Time.deltaTime);
                }
                if (isJawOnPlace2 == false && gameObject.name == "Jaw2")
                {
                    parent.transform.position = Vector3.MoveTowards(parent.transform.position, new Vector3(parent.transform.position.x, 0f, 0f), speed * Time.deltaTime);
                }
                if (isJawOnPlace3 == false && gameObject.name == "Jaw3")
                {
                    parent.transform.position = Vector3.MoveTowards(parent.transform.position, new Vector3(parent.transform.position.x, 0f, 0f), speed * Time.deltaTime);
                }
            }

        if (JawScrewPlacement.unRotatingScrew == true)
        {
            if (gameObject.name == "Jaw1" && isJawOnStart1 == false)
            {
                parent.transform.position = Vector3.MoveTowards(parent.transform.position, parentPos.transform.position, speed * Time.deltaTime);
            }
            if (gameObject.name == "Jaw2" && isJawOnStart2 == false)
            {
                parent.transform.position = Vector3.MoveTowards(parent.transform.position, parentPos.transform.position, speed * Time.deltaTime);
            }
            if (gameObject.name == "Jaw3" && isJawOnStart3 == false)
            {
                parent.transform.position = Vector3.MoveTowards(parent.transform.position, parentPos.transform.position, speed * Time.deltaTime);
            }
        }

            if (parent.transform.position == parentPos.transform.position && gameObject.name == "Jaw1")
            {
                isJawOnStart1 = true;
            }
            else
            {
                isJawOnStart1 = false;
            }

            if (isJawOnPlace1 == true) //Jeśli wszystkie szczęki są na miejscu zmień zmienną na true
            {
                isAllJawOnPlace = true;
            }

            else
            {
                isAllJawOnPlace = false;
            }
        

    }

    public void StartPosJaws()
    {
        parent.transform.position = parentPos.transform.position;
    }

    private void OnTriggerEnter(Collider other)     //Wykrywa kiedy szczęki stykają się z materiałem obrabianym co stopuje ich dalszy ruch
    {

            if (gameObject.name == "Jaw1" && other.gameObject.name == "CylinderAdd")
            {
                isJawOnPlace1 = true;
            }

            if (gameObject.name == "Jaw2" && other.gameObject.name == "CylinderAdd")
            {
                isJawOnPlace2 = true;
            }

            if (gameObject.name == "Jaw3" && other.gameObject.name == "CylinderAdd")
            {
                isJawOnPlace3 = true;
            }
        
    }

    private void OnTriggerExit(Collider other)  //Wykrywa kiedy szczęki przestają stykać się z materiałem obrabianym co zmienia zmienną na false
    {

            if (gameObject.name == "Jaw1" && other.gameObject.name == "CylinderAdd")
            {
                isJawOnPlace1 = false;
            }

            if (gameObject.name == "Jaw2" && other.gameObject.name == "CylinderAdd")
            {
                isJawOnPlace2 = false;
            }

            if (gameObject.name == "Jaw3" && other.gameObject.name == "CylinderAdd")
            {
                isJawOnPlace3 = false;
            }
        
    }
}
