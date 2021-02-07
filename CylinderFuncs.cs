using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderFuncs : MonoBehaviour
{
    [SerializeField] GameObject chooseMaterialUI;
    [SerializeField] GameObject target;
    [SerializeField] GameObject playSystem;
    [SerializeField] GameObject cylinderAdd;
    [SerializeField] GameObject wholeCylinder;
    [SerializeField] GameObject playerUI;
    [SerializeField] GameObject UIError;
    [SerializeField] GameObject playerController;
    [SerializeField] GameObject cylinderText;

    public static bool isChooseMaterialMenuOn = false;

    private void Start()
    {
        SpawnCylinderClone();
    }

    private void Update()
    {
        MakeUnselectableWhenMoving();
        RotateWholeCylinder();
    }

    private void OnMouseOver()
    {
        if(!GameManager.isErrorOn)
        {
            cylinderText.SetActive(true);
        }
        OnCylinderClick();
    }

    private void OnMouseExit()
    {
        cylinderText.SetActive(false);
    }


    public void OnCylinderClick()
    {
        if (Input.GetKeyDown("e") && GameManager.isMainMenuOn == false && gameObject.tag == "selectable")
        {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                target.SetActive(false);
                playerController.GetComponent<PlayerMovement>().enabled = false;
                playerController.GetComponentInChildren<MouseLook>().enabled = false;
                playerUI.SetActive(false);
                chooseMaterialUI.SetActive(true);
                isChooseMaterialMenuOn = true;
        }
    }

    public void OnCylinderExit()
    {
        if(TextReader.materialInterferenceWithHorse == false && TextReader.materialInterferenceWithSupport == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerController.GetComponent<PlayerMovement>().enabled = true;
            playerController.GetComponentInChildren<MouseLook>().enabled = true;
            playerUI.SetActive(true);
            chooseMaterialUI.SetActive(false);
            isChooseMaterialMenuOn = false;
            cylinderAdd.SetActive(true);
            SpawnCylinderClone();
            target.SetActive(true);
            playSystem.GetComponent<PlaySystem>().ResetTarget();
            target.GetComponent<TurningMeshBuilder>().IntializeAgain();
            target.GetComponent<TurningController>().Start();
            target.GetComponent<TurningController>().ResetState();
        }
        else if(TextReader.materialInterferenceWithHorse == true)
        {
            cylinderAdd.SetActive(false);
            chooseMaterialUI.SetActive(false);
            isChooseMaterialMenuOn = false;
            UIError.SetActive(true);
        }
        else if (TextReader.materialInterferenceWithSupport == true)
        {
            cylinderAdd.SetActive(false);
            chooseMaterialUI.SetActive(false);
            isChooseMaterialMenuOn = false;
            UIError.SetActive(true);
        }
    }

    public void SpawnCylinderClone()    //Inicjalizacja cylindra w bębnie, na którym zaciskają się szczęki
    { 
        cylinderAdd.transform.position = new Vector3(LevelStartValueCache.targetSpawnPos.position.x + 0.1f, LevelStartValueCache.targetSpawnPos.position.y, LevelStartValueCache.targetSpawnPos.position.z);
        cylinderAdd.transform.localScale = new Vector3(target.GetComponent<TurningController>().height * 2f, 0.1f, target.GetComponent<TurningController>().height * 2f);
    }

    public void OnErrorExit()
    {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerController.GetComponent<PlayerMovement>().enabled = true;
            playerController.GetComponentInChildren<MouseLook>().enabled = true;
            playerUI.SetActive(true);
            cylinderAdd.SetActive(false);
    }

    public void MakeUnselectableWhenMoving()
    {
        if(TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true))
        {
            gameObject.tag = "Moving";
        }
        else
        {
            gameObject.tag = "selectable";

        }
    }

    public void RotateWholeCylinder()
    {
        if (TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true))
        {
            wholeCylinder.transform.Rotate(PlaySystem.cylinderSpinningSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
    }

}
