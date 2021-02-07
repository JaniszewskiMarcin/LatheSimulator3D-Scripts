using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportFuncs : MonoBehaviour
{
    [SerializeField] GameObject supportText;
    [SerializeField] GameObject target;
    [SerializeField] GameObject targetSpawn;
    [SerializeField] GameObject chiselPos;
    [SerializeField] GameObject turningCamera;
    [SerializeField] GameObject playerController;
    [SerializeField] GameObject playerUI;
    [SerializeField] GameObject turningUI;
    [SerializeField] GameObject selectionManager;
    [SerializeField] PlaySystem system;
    [SerializeField] GameObject edgeCheck;
    public GameObject parent;
    public ChiselTable chiselTabel;
    private GameObject chiselPrefab;
    private GameObject actualChiselPrefab;

    int chiselNumberInfo = 0;
    int i = 2;
    float[] supportRotation = new float[5] { -45f, -30f, 0f, 30f, 45f};

    public static bool cameraTurningOn = false;
    public static bool collideWithMaterial = false;


    private void OnMouseOver()
    {
        if (!GameManager.isErrorOn)
        {
            supportText.SetActive(true);
        }
        OnSupportClick();
    }

    private void OnMouseExit()
    {
        supportText.SetActive(false);
    }

    public void OnSupportClick()        
    {
        if(Input.GetKey("e") && SupportLeaver.isSupportLeaverOnPlace == false)
        {
            TurnTurningModeOn();
        }

        if (SupportLeaver.isSupportLeaverOnPlace == true && target != null)
        { 
            if (Input.GetKeyDown("e") && i + 1 < supportRotation.Length && chiselPos.transform.position.y <= TurningChisel.ClampPositionYStatic - target.GetComponent<TurningController>().height)
            {
                i++;
                parent.transform.rotation = Quaternion.Euler(0f, 0f, supportRotation[i]);
                UpdateChiselPrefab();
            }

            if (Input.GetKeyDown("q") && i - 1 >= 0 && chiselPos.transform.position.x <= targetSpawn.transform.position.x - 0.15f)
            {
                i--;
                parent.transform.rotation = Quaternion.Euler(0f, 0f, supportRotation[i]);
                UpdateChiselPrefab();
            }
        }

        if(SupportLeaver.isSupportLeaverOnPlace == true && target == null)
        {
            if ((Input.GetKeyDown("e") && i + 1 < supportRotation.Length && chiselPos.transform.position.y <= TurningChisel.ClampPositionYStatic - 0.15f))
            {
                i++;
                parent.transform.rotation = Quaternion.Euler(0f, 0f, supportRotation[i]);
                UpdateChiselPrefab();
            }

            if ((Input.GetKeyDown("q") && i - 1 >= 0 && chiselPos.transform.position.x <= targetSpawn.transform.position.x - 0.15f) )
            {
                i--;
                parent.transform.rotation = Quaternion.Euler(0f, 0f, supportRotation[i]);
                UpdateChiselPrefab();
            }
        }
    }

    //Metoda przenosząca użytkownika do specjalnego trybu toczenia.
    public void TurnTurningModeOn()
    {
        //Metody odblokowywujące kursor oraz włączające jego widoczność na ekranie.
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        //Zmienna "bool" pozwalająca na określenie czy użytkownik korzysta aktualnie z specjalnego trybu.
        cameraTurningOn = true;
        //Uruchomienie obiektu z kamerą zapewniającą wygodniejszy widok.
        turningCamera.SetActive(true);
        //Wyłączenie kodu odpowiedzialnego za poruszanie się postaci.
        playerController.SetActive(false);
        //Wyłączenie celownika.
        playerUI.SetActive(false);
    }

    public void TurnTurningModeOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraTurningOn = false;
        turningCamera.SetActive(false);
        playerController.SetActive(true);
        playerUI.SetActive(true);
        turningUI.SetActive(false);
    }

    public void SetChisel(int chiselIndex)
    {
        ChiselInfo chiselInfo = chiselTabel.GetChiselInfoAtIndex(chiselIndex);
        chiselPrefab = chiselInfo.prefab;
        system.ReplaceChisel(chiselPrefab);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Target")
        {
            collideWithMaterial = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Target")
        {
            collideWithMaterial = false;
        }
    }

    public void UpdateChiselPrefab()
    {
            actualChiselPrefab = FindObjectOfType<TurningChisel>().gameObject;


            if(actualChiselPrefab.name == "Square_Chisel(Clone)")
            {
                chiselNumberInfo = 0;
            }

            if (actualChiselPrefab.name == "Rounded_Chisel(Clone)")
            {
                chiselNumberInfo = 5;
            }

        if (actualChiselPrefab.name == "Triangle_Chisel(Clone)")
        {
            chiselNumberInfo = 10;
        }

        if (actualChiselPrefab.name == "TrapezRight_Chisel(Clone)")
        {
            chiselNumberInfo = 15;
        }

        if (actualChiselPrefab.name == "TrapezLeft_Chisel(Clone)")
        {
            chiselNumberInfo = 20;
        }

        if (i == 0) //-45 stopni
            {
                SetChisel(chiselNumberInfo);
            }

            if (i == 1) //-30 stopni
            {
                SetChisel(chiselNumberInfo + 1);
            }

            if (i == 2) //0 stopni
            {
                SetChisel(chiselNumberInfo + 2);
            }

            if (i == 3) //30 stopni
            {
                SetChisel(chiselNumberInfo + 3);
            }

            if (i == 4) //45 stopni
            {
                SetChisel(chiselNumberInfo + 4);
            }
    }
}
