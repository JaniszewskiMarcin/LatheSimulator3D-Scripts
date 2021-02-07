using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedObjects : MonoBehaviour        //Skrypt dołączany do każdego elementu, który jest zaznaczany
{
    //[SerializeField] GameObject chooseMaterialUI;

    ////turning components
    //[SerializeField] GameObject levelManager;
    //[SerializeField] GameObject playSystem;
    //[SerializeField] GameObject targetSpawn;
    //[SerializeField] GameObject chiselSpawn;
    //[SerializeField] GameObject target;
    //[SerializeField] GameObject turningUI;
    //[SerializeField] GameObject turningCamera;
    //[SerializeField] GameObject cylinderAdd;

    ////play objects
    //[SerializeField] GameObject scrub;
    //[SerializeField] GameObject playerUI;
    //[SerializeField] GameObject selectionManager;
    //[SerializeField] GameObject playerController;

    //GameObject cylinderClone;
    //Collider colliderComponent;
    //Vector3 centerPos;

    //public static bool lookingAtObject = false;
    //public GameObject selectedObject;
    //public static bool inTurning = false;

    //public static bool isHorseOnPlace = false;          //Sprawdza czy konik jest na miejscu
    ///*public static bool isCrankOnPlace = false;*/          //Sprawdza czy część konika jest dokręcona na max
    //public static bool isMaterialDimensions = false;    //Sprawdza czy zostały wprowadzone wymiary materiału
    //public static bool materialIsIn = false;            //Sprawdza czy materiał został już zatwierdzony oraz obrabiany
    //public static bool isChooseMaterialMenuOn = false;

    ////private void Update()
    ////{
    ////    RotateCylinderAdd();
    ////}

    //private void OnMouseOver() //Funkcja wykonuje się kiedy najedziemy myszką na obiekt
    //{
    //    OnCylinderClick();  //Jeśli klikniemy w cylinder, wyświetli nam się wybór wymiarów materiału

    //    /*OnSupportClick();*/   //Przechodzimy do stanu toczenia

    //    /* OnCrankClick();*/     //Kręcimy kołem by dokręcić część konika

    //    OnJawsScrewClick(); //Dokręcamy szczęki kręcąc śrubą
    //}

    //private void OnMouseExit()  //Funkcja wykonuje się gdy przestajemy najeżdżać myszką na obiekt
    //{
    //    lookingAtObject = false;
    //}

    ////public void FromPlayToTurning() //Funkcja przejścia z gry do toczenia
    ////{
    ////    inTurning = true;

    ////    Cursor.lockState = CursorLockMode.Confined;
    ////    Cursor.visible = true;

    ////    levelManager.SetActive(true);
    ////    chiselSpawn.SetActive(true);
    ////    turningUI.SetActive(true);
    ////    turningCamera.SetActive(true);

    ////    playerUI.SetActive(false);
    ////    playerController.SetActive(false);
    ////}

    ////public void FromTurningToPlay() //Funkcja przejścia z toczenia do gry
    ////{
    ////    inTurning = false;

    ////    Cursor.lockState = CursorLockMode.Locked;
    ////    Cursor.visible = false;

    ////    levelManager.SetActive(false);
    ////    chiselSpawn.SetActive(false);
    ////    turningUI.SetActive(false);
    ////    turningCamera.SetActive(false);

    ////    playerUI.SetActive(true);
    ////    playerController.SetActive(true);
    ////}

    //public void SpawnCylinderClone()    //Inicjalizacja cylindra w bębnie, na którym zaciskają się szczęki
    //{
    //    cylinderAdd.SetActive(true);
    //    cylinderAdd.transform.position = new Vector3(LevelStartValueCache.targetSpawnPos.position.x + 0.1f, LevelStartValueCache.targetSpawnPos.position.y, LevelStartValueCache.targetSpawnPos.position.z);
    //    cylinderAdd.transform.Rotate(0f, 0f, 90f);
    //    cylinderAdd.transform.localScale = new Vector3(target.GetComponent<TurningController>().height * 2f, 0.1f, target.GetComponent<TurningController>().height * 2f);
    //    colliderComponent = cylinderAdd.GetComponent<Collider>();
    //    centerPos = colliderComponent.bounds.center;
    //}

    //public void RotateCylinderAdd()
    //{
    //    if (cylinderAdd == null)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        if (inTurning)
    //        {
    //            cylinderAdd.transform.RotateAround(centerPos, new Vector3(1f, 0f, 0f), TurningController.spiningSpeed * Time.deltaTime);
    //        }
    //    }
    //}

    //public void OnCylinderClick()
    //{
    //    selectedObject = GameObject.Find(SelectionManager.selectedObject);
    //    Collider selectedCollider = selectedObject.GetComponent(typeof(Collider)) as Collider;
    //    lookingAtObject = true;

    //    if (selectedObject.name == "Cylinder" && Input.GetKeyDown("e") && materialIsIn == false && GameManager.isMainMenuOn == false)
    //    {
    //        Cursor.lockState = CursorLockMode.Confined;
    //        target.SetActive(false);
    //        playerController.GetComponent<PlayerMovement>().enabled = false;
    //        playerController.GetComponentInChildren<MouseLook>().enabled = false;
    //        playerUI.SetActive(false);
    //        chooseMaterialUI.SetActive(true);
    //        playerController.GetComponentInChildren<MouseLook>().enabled = false;
    //        isChooseMaterialMenuOn = true;
    //        Cursor.visible = true;
    //        //SelectedObjects.isHorseOnPlace = false;
    //        //SelectedObjects.isCrankOnPlace = false;
    //        if (cylinderAdd != null)
    //        {
    //            cylinderAdd.SetActive(false);
    //        }
    //    }
    //}

    ////public void OnSupportClick()
    ////{
    ////    selectedObject = GameObject.Find(SelectionManager.selectedObject);
    ////    Collider selectedCollider = selectedObject.GetComponent(typeof(Collider)) as Collider;
    ////    lookingAtObject = true;

    ////    if (selectedObject.name == "Support" && Input.GetKeyDown("e"))
    ////    {
    ////        if (isMaterialDimensions == true /*&& isCrankOnPlace == true*/ && isHorseOnPlace == true && CheckTheJaws.isAllJawOnPlace == true)
    ////        {
    ////            FromPlayToTurning();
    ////            TurningController.spiningSpeed = 1200f;
    ////            materialIsIn = true;
    ////        }
    ////        else
    ////        {
    ////            Debug.Log("Nie mozna tego zrobic.");
    ////        }
    ////    }
    ////}

    ////public void OnCrankClick()
    ////{
    ////    selectedObject = GameObject.Find(SelectionManager.selectedObject);
    ////    Collider selectedCollider = selectedObject.GetComponent(typeof(Collider)) as Collider;
    ////    lookingAtObject = true;

    ////    if (selectedObject.name == "Crank" && Input.GetKey("e") && isCrankOnPlace == false && SlideWhenRotatingCrank.stopRotating == false /*&& isMaterialDimensions == true*/ && isHorseOnPlace == true /*&& SlideHorse.isWidthTooLong == true*/)
    ////    {
    ////        selectedObject.transform.RotateAround(selectedCollider.bounds.center, new Vector3(-1f, 0f, 0f), 300f * Time.deltaTime);
    ////    }

    ////    if (selectedObject.name == "Crank" && Input.GetKey("q") && SlideWhenRotatingCrank.stopRotating == false/* && isMaterialDimensions == true*/ && isHorseOnPlace == true /*&& SlideHorse.isWidthTooLong == true*/)
    ////    {
    ////        selectedObject.transform.RotateAround(selectedCollider.bounds.center, new Vector3(1f, 0f, 0f), 300f * Time.deltaTime);
    ////    }
    ////}

    //public void OnJawsScrewClick()
    //{
    //    selectedObject = GameObject.Find(SelectionManager.selectedObject);
    //    lookingAtObject = true;

    //    if (selectedObject.name == "JawsScrew" && Input.GetKey("e") && isMaterialDimensions == true && CheckTheJaws.isAllJawOnPlace == false && materialIsIn == false)
    //    {
    //        selectedObject.transform.RotateAround(scrub.transform.position, scrub.transform.forward, 100f * Time.deltaTime);
    //    }
    //}

    //public void SetVariableChoosMenuFalse()
    //{
    //    isChooseMaterialMenuOn = false;
    //    Cursor.visible = false;
    //    //}
    //}
}