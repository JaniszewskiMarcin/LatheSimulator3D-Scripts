using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour    //podłączony do nie wyłączanego obiektu gry, obsługuje ogólne funkcje takie jak menu
{
    [SerializeField] GameObject speedPanelFeedUI;
    [SerializeField] GameObject backUpArrow;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject cameraStop;
    [SerializeField] GameObject knifeSupport;
    [SerializeField] GameObject errorHorseAndSupport;
    [SerializeField] GameObject knifeConditionUI;
    [SerializeField] GameObject tutorialUI;
    [SerializeField] GameObject interfaceInfoUI;
    [SerializeField] GameObject startInfoUI;
    [SerializeField] GameObject textArrow;
    LevelStartValueCache startValueCache;
    private GameObject chiselGo;
    public Transform chiselSpawnTransform;
    public GameObject targetGo;
    GameObject chiselPrefab;

    public ChiselTable chiselTabel;

    public static bool isMainMenuOn = false;
    public static Vector3 knifeSupportPosition;
    public static int counterJaw = 0;
    public static int counterHorse = 0;
    public static float counterSquare = 0;
    public static float counterTriangle = 0;
    public static float counterRound = 0;
    public static float counterTrapezLeft = 0;
    public static float counterTrapezRight = 0;
    public static float chiselStrenght = 6f;

    bool square = false;
    bool rounded = false;
    bool triangle = false;
    bool trapezLeft = false;
    bool trapezRight = false;

    bool hasPlayedStart = false;

    public static string actualChisel;
    public static bool isErrorOn = false;

    float startTimeJawsNoOnPlace = 0f;
    [SerializeField] float durationTimeJawsNoOnPlace = 2f;
    bool startNoJawsOnPlace = false;

    float startTimeHorseNoOnPlace = 0f;
    [SerializeField] float durationTimeHorseNoOnPlace = 2f;
    bool startNoHorseOnPlace = false;

    AudioSource rotatingFast;
    [SerializeField] AudioClip rotatingFastClip;
    AudioSource rotatingMid;
    [SerializeField] AudioClip rotatingMidClip;
    AudioSource rotatingSlow;
    [SerializeField] AudioClip rotatingSlowClip;
    AudioSource startRotating;
    [SerializeField] AudioClip startRotatingClip;
    AudioSource running;
    [SerializeField] AudioClip runningClip;
    private void Awake()
    {
        //ChiselInfo chiselInfo = chiselTabel.GetChiselInfoAtIndex(0);
        //chiselPrefab = chiselInfo.prefab;
        //SpawnChisel(chiselPrefab);
        //SpawnTarget();
    }

    private void Start()
    {
        rotatingFast = gameObject.AddComponent<AudioSource>();
        rotatingFast.clip = rotatingFastClip;
        rotatingFast.volume = 0.6f;

        rotatingMid = gameObject.AddComponent<AudioSource>();
        rotatingMid.clip = rotatingMidClip;
        rotatingMid.volume = 0.6f;

        rotatingSlow = gameObject.AddComponent<AudioSource>();
        rotatingSlow.clip = rotatingSlowClip;
        rotatingSlow.volume = 0.4f;

        startRotating = gameObject.AddComponent<AudioSource>();
        startRotating.clip = startRotatingClip;
        startRotating.volume = 0.8f;

        running = gameObject.AddComponent<AudioSource>();
        running.clip = runningClip;
        running.volume = 0.4f;

        knifeSupportPosition = knifeSupport.transform.position;
        Cursor.visible = false;
    }

    private void Update()
    {
        TurnOnOrOffMenu();
        TurnOnOffInterfaceInfoUI();
        TurnOnOffTutorialUI();

        CheckJawsPositionTime();
        CheckTheJawsAndRotation();

        CheckTheorsePositionTime();
        CheckTheHorse();

        CheckTheKnifeCondition();

        ShowBackUpArrow();

        RunningLatheSound();
        StartRotatingSound();
    }

    public void RunningLatheSound()
    {
        if (TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == false || CylinderRotatingLeaver.isRightRotatingLeaverOn == false))
        {
            if(running.isPlaying == false)
            {
                running.Play();
            }
        }
        else if (TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true))
        {
            if (running.isPlaying == true)
            {
                running.Stop();
            }
        }
        else if (TurnOnBut.isTurnOnButtonOn == false && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == false || CylinderRotatingLeaver.isRightRotatingLeaverOn == false))
        {
            if (running.isPlaying == true)
            {
                running.Stop();
            }
        }
    }

    public void StartRotatingSound()
    {
        if (TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true))
        {
            if (startRotating.isPlaying == false && hasPlayedStart == false)
            {
                startRotating.Play();
                hasPlayedStart = true;
            }

                if (Mathf.Abs(PlaySystem.cylinderSpinningSpeed) <= 200f)
                {
                    rotatingMid.Stop();
                    rotatingFast.Stop();

                    if (rotatingSlow.isPlaying == false)
                    {
                        rotatingSlow.Play();
                    }
                }
                else if (Mathf.Abs(PlaySystem.cylinderSpinningSpeed) > 200f && Mathf.Abs(PlaySystem.cylinderSpinningSpeed) <= 1000f)
                {
                    rotatingSlow.Stop();
                    rotatingFast.Stop();

                    if (rotatingMid.isPlaying == false)
                    {
                        rotatingMid.Play();
                    }
                }
                else if (Mathf.Abs(PlaySystem.cylinderSpinningSpeed) > 1000f)
                {
                    rotatingSlow.Stop();
                    rotatingMid.Stop();

                    if (rotatingFast.isPlaying == false)
                    {
                        rotatingFast.Play();
                    }
                }
        }
        else if (TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == false || CylinderRotatingLeaver.isRightRotatingLeaverOn == false))
        {
            hasPlayedStart = false;
            if (startRotating.isPlaying == true)
            {
                startRotating.Stop();
            }
            else if (rotatingSlow.isPlaying == true)
            {
                rotatingSlow.Stop();
            }
            else if (rotatingMid.isPlaying == true)
            {
                rotatingMid.Stop();
            }
            else if (rotatingFast.isPlaying == true)
            {
                rotatingFast.Stop();
            }

        }
        else
        {
            hasPlayedStart = false;
            if (startRotating.isPlaying == true)
            {
                startRotating.Stop();
            }
            else if (rotatingSlow.isPlaying == true)
            {
                rotatingSlow.Stop();
            }
            else if (rotatingMid.isPlaying == true)
            {
                rotatingMid.Stop();
            }
            else if (rotatingFast.isPlaying == true)
            {
                rotatingFast.Stop();
            }
        }
    }

    public void TurnOnOffTutorialUI()
    {
        if (Input.GetKeyDown(KeyCode.P) && CylinderFuncs.isChooseMaterialMenuOn != true && knifeConditionUI.activeSelf == false && interfaceInfoUI.activeSelf == false && mainMenu.activeSelf == false && startInfoUI.activeSelf == false)
        {
            if(!SupportFuncs.cameraTurningOn)
            {
                if (tutorialUI.activeSelf == false)
                {
                    tutorialUI.SetActive(true);
                    InformationUI.ShowOrHideCursor(true);
                }
                else
                {
                    tutorialUI.SetActive(false);
                    InformationUI.ShowOrHideCursor(false);
                }
            }
            else
            {
                if (tutorialUI.activeSelf == false)
                {
                    tutorialUI.SetActive(true);
                    InformationUI.ShowOrHideCursor(true);
                }
                else
                {
                    tutorialUI.SetActive(false);
                    Time.timeScale = 1f;
                }
            }
        }
    }

    public void TurnOnOffInterfaceInfoUI()
    {
        if(Input.GetKeyDown(KeyCode.I) && CylinderFuncs.isChooseMaterialMenuOn != true && knifeConditionUI.activeSelf == false && tutorialUI.activeSelf == false && mainMenu.activeSelf == false && startInfoUI.activeSelf == false)
        {
            if(!SupportFuncs.cameraTurningOn)
            {
                if (interfaceInfoUI.activeSelf == false)
                {
                    textArrow.SetActive(false);
                    interfaceInfoUI.SetActive(true);
                    speedPanelFeedUI.SetActive(true);
                    InformationUI.ShowOrHideCursor(true);
                }
                else
                {
                    textArrow.SetActive(false);
                    interfaceInfoUI.SetActive(false);
                    speedPanelFeedUI.SetActive(false);
                    InformationUI.ShowOrHideCursor(false);
                }
            }
            else
            {
                if (interfaceInfoUI.activeSelf == false)
                {
                    textArrow.SetActive(true);
                    interfaceInfoUI.SetActive(true);
                    speedPanelFeedUI.SetActive(true);
                    InformationUI.ShowOrHideCursor(true);
                }
                else
                {
                    textArrow.SetActive(false);
                    interfaceInfoUI.SetActive(false);
                    speedPanelFeedUI.SetActive(false);
                    Time.timeScale = 1f;
                }
            }
        }
    }

    public void TurnOnOrOffMenu()
    {
        if (Input.GetButtonDown("Cancel") && CylinderFuncs.isChooseMaterialMenuOn != true && knifeConditionUI.activeSelf == false && tutorialUI.activeSelf == false && interfaceInfoUI.activeSelf == false && startInfoUI.activeSelf == false && FeedSpeedButton.isSpeedKnifeInfoUION == false)
        {

            if(!SupportFuncs.cameraTurningOn)
            {
                if (mainMenu.activeSelf == true)
                {
                    mainMenu.SetActive(false);
                    InformationUI.ShowOrHideCursor(false);
                }
                else
                {
                    mainMenu.SetActive(true);
                    InformationUI.ShowOrHideCursor(true);
                }
            }
            else
            {
                if (mainMenu.activeSelf == true)
                {
                    mainMenu.SetActive(false);
                    Time.timeScale = 1f;
                }
                else
                {
                    mainMenu.SetActive(true);
                    InformationUI.ShowOrHideCursor(true);
                }
            }
        }
    }

    public void CheckJawsPositionTime()
    {
        if (TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true) && CheckTheJaws.isAllJawOnPlace != true && startNoJawsOnPlace == false)
        {
            startTimeJawsNoOnPlace = Time.time;
            startNoJawsOnPlace = true;
        }
    }

    public void CheckTheJawsAndRotation()
    {
        if(targetGo.activeSelf == true)
        {
                if (Time.time >= startTimeJawsNoOnPlace + durationTimeJawsNoOnPlace && startNoJawsOnPlace == true)
                {
                    TurnOnBut.isTurnOnButtonOn = false;
                    TurnOnPowerBut.isTurnOnPowerButtonOn = false;
                    StartCoroutine(ShowAndHideError(errorHorseAndSupport, 5f));
                    startNoJawsOnPlace = false;
                    Debug.Log("JAWS");
                }
        }
    }

    public void CheckTheorsePositionTime()
    {
        if (TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true) && CrankOnPlace.isCrankOnPlace != true && -TurningController.baseXpos.x - 0.229f >= 1f && startNoHorseOnPlace == false)
        {
            startTimeHorseNoOnPlace = Time.time;
            startNoHorseOnPlace = true;
        }
    }

    public void CheckTheHorse()
    {
        if (targetGo.activeSelf == true)
        {
                if (Time.time >= startTimeHorseNoOnPlace + durationTimeHorseNoOnPlace && startNoHorseOnPlace == true)
                {
                    TurnOnBut.isTurnOnButtonOn = false;
                    TurnOnPowerBut.isTurnOnPowerButtonOn = false;
                    StartCoroutine(ShowAndHideError(errorHorseAndSupport, 5f));
                    startNoHorseOnPlace = false;
                    Debug.Log("HORSE");
                }
        }
    }

    IEnumerator ShowAndHideError(GameObject error, float delay)
    {
        error.SetActive(true);
        isErrorOn = true;
        yield return new WaitForSeconds(delay);
        error.SetActive(false);
        isErrorOn = false;
    }

    public void ResetAllVariables()
    {
        CylinderFuncs.isChooseMaterialMenuOn = false;
        CheckTheJaws.isAllJawOnPlace = false;
        CheckTheJaws.isJawOnPlace1 = false;
        CheckTheJaws.isJawOnPlace2 = false;
        CheckTheJaws.isJawOnPlace3 = false;
        isMainMenuOn = false;
    }

    public void CheckTheKnifeCondition()
    {
        if (GameObject.Find("Square_Chisel(Clone)") || GameObject.Find("Square_Chisel 30(Clone)") || GameObject.Find("Square_Chisel 45(Clone)") || GameObject.Find("Square_Chisel-30(Clone)") || GameObject.Find("Square_Chisel-45(Clone)"))
        {
            actualChisel = "Square";

            if (counterSquare >= chiselStrenght)
            {
                square = true;
                EnableKnifeConditionUI();
            }
        }

        if (GameObject.Find("Rounded_Chisel(Clone)") || GameObject.Find("Rounded_Chisel 30(Clone)") || GameObject.Find("Rounded_Chisel 45(Clone)") || GameObject.Find("Rounded_Chisel-30(Clone)") || GameObject.Find("Rounded_Chisel-45(Clone)"))
        {
            actualChisel = "Round";

            if (counterRound >= chiselStrenght)
            {
                rounded = true;
                EnableKnifeConditionUI();
            }
        }

        if (GameObject.Find("Triangle_Chisel(Clone)") || GameObject.Find("Triangle_Chisel 30(Clone)") || GameObject.Find("Triangle_Chisel 45(Clone)") || GameObject.Find("Triangle_Chisel-30(Clone)") || GameObject.Find("Triangle_Chisel-45(Clone)"))
        {
            actualChisel = "Triangle";

            if (counterTriangle >= chiselStrenght)
            {
                triangle = true;
                EnableKnifeConditionUI();
            }
        }

        if (GameObject.Find("TrapezLeft_Chisel(Clone)") || GameObject.Find("TrapezLeft_Chisel 30(Clone)") || GameObject.Find("TrapezLeft_Chisel 45(Clone)") || GameObject.Find("TrapezLeft_Chisel-30(Clone)") || GameObject.Find("TrapezLeft_Chisel-45(Clone)"))
        {
            actualChisel = "TrapezLeft";

            if (counterTrapezLeft >= chiselStrenght)
            {
                trapezLeft = true;
                EnableKnifeConditionUI();
            }
        }

        if (GameObject.Find("TrapezRight_Chisel(Clone)") || GameObject.Find("TrapezRight_Chisel 30(Clone)") || GameObject.Find("TrapezRight_Chisel 45(Clone)") || GameObject.Find("TrapezRight_Chisel-30(Clone)") || GameObject.Find("TrapezRight_Chisel-45(Clone)"))
        {
            actualChisel = "TrapezRight";

            if (counterTrapezRight >= chiselStrenght)
            {
                trapezRight = true;
                EnableKnifeConditionUI();
            }
        }
    }

    public void EnableKnifeConditionUI()
    {
        InformationUI.ShowOrHideCursor(true);
        knifeConditionUI.SetActive(true);
    }

    public void ResetKnifeCondition()
    {
        if (square == true)
        {
            counterSquare = 0f;
            square = false;
        }

        if (rounded == true)
        {
            counterRound = 0f;
            rounded = false;
        }

        if (triangle == true)
        {
            counterTriangle = 0f;
            triangle = false;
        }

        if (trapezLeft == true)
        {
            counterTrapezLeft = 0f;
            trapezLeft = false;
        }

        if (trapezRight == true)
        {
            counterTrapezRight = 0f;
            trapezRight = false;
        }

        Time.timeScale = 1f;
        ChiselOverlapCondition.counter = 0f;
    }

    public void ShowBackUpArrow()
    {
        if(SupportFuncs.cameraTurningOn)
        {
            if(!backUpArrow.activeSelf)
            {
                backUpArrow.SetActive(true);
            }
        }
        else
        {
            if (backUpArrow.activeSelf)
            {
                backUpArrow.SetActive(false);
            }
        }
    }
}
