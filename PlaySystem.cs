using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySystem : MonoBehaviour
{
    //public TurningController controller;

    //public LevelStartValueCache startValueCache;

    //public ChiselTable chiselTable;

    //public GameObject targetGo;

    //private static PlaySystem instance;

    //private PlayState currentState;
    //public static PlaySystem Instance
    //{
    //    get
    //    {
    //        if (!instance)
    //        {
    //            instance = GameObject.FindObjectOfType<PlaySystem>();

    //            if (!instance)
    //            {
    //                var go = new GameObject("PlaySystem");
    //                go.AddComponent<PlaySystem>();
    //            }
    //        }

    //        return instance;
    //    }
    //}

    //private void Awake()
    //{
    //    if (instance != null && instance.GetInstanceID() != GetInstanceID())
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    instance = this;
    //}

    //private void OnDestroy()
    //{
    //    instance = null;
    //}

    //public void Start()
    //{
    //    CreateOrChangeToNextState();
    //}

    //public void Update()
    //{
    //    if (currentState != null) currentState.Update();
    //}

    //public void SetState(PlayState state)
    //{
    //    if (currentState != null)
    //    {
    //        currentState.Exit();
    //    }

    //    currentState = state;

    //    if (currentState != null)
    //    {
    //        currentState.Enter();
    //    }
    //}

    //public PlayState GetState()
    //{
    //    return currentState;
    //}

    //public void CreateOrChangeToNextState()
    //{
    //    if (currentState == null)
    //    {
    //        LevelManager.Instance.Load(true);

    //        GUIEventDispatcher.Instance.NotifyEvent(GUIEventID.StartLevel);

    //        Transform targetTransform = targetGo.transform;
    //        targetTransform.position = new Vector3(startValueCache.targetSpawnTransform.position.x - targetGo.GetComponent<TurningController>().width / 2 + 0.15f, startValueCache.targetSpawnTransform.position.y, startValueCache.targetSpawnTransform.position.z); ;
    //        targetTransform.rotation = startValueCache.targetSpawnTransform.rotation;
    //        targetGo.GetComponent<TurningController>().ResetState();
    //        targetGo.SetActive(true);

    //        GUIEventDispatcher.Instance.NotifyEvent(GUIEventID.UpdateMoney, LevelManager.Instance.PlayerData.moneyScore);

    //        TurningState state = new TurningState(this);
    //        state.targetGo = targetGo;
    //        state.targetTransform = targetTransform;
    //        state.chiselSpawnTransform = startValueCache.chiselSpawnTransform;

    //        SetState(state);
    //    }
    //}

    public Image turningImage;
    public Image drillingImage;
    public GameObject edge;
    public GameObject targetGo;
    public GameObject chiselPos;
    private GameObject chiselPrefab;
    private GameObject drillPrefab;
    [HideInInspector] public GameObject drillGo;
    [HideInInspector] public GameObject chiselGo;
    public LevelStartValueCache startValueCache;
    public Transform chiselSpawnTransform;
    public Transform drillSpawnTransform;
    public ChiselTable chiselTable;
    public ChiselTable drillTable;

    Vector3 drillOldPosition;

    public static float cylinderSpinningSpeed = 0f;
    public static string chiselName;

    public static bool inTurning = true;
    public static bool inDrilling = false;

    private void Start()
    {
        Transform targetTransform = targetGo.transform;
        targetTransform.position = new Vector3(startValueCache.targetSpawnTransform.position.x - targetGo.GetComponent<TurningController>().width / 2 + 0.15f, startValueCache.targetSpawnTransform.position.y, startValueCache.targetSpawnTransform.position.z);
        targetTransform.rotation = startValueCache.targetSpawnTransform.rotation;
        targetGo.GetComponent<TurningController>().ResetState();
        targetGo.SetActive(true);

        ChiselInfo chiselInfo = chiselTable.GetChiselInfoAtIndex(2);
        chiselPrefab = chiselInfo.prefab;

        ReplaceChisel(chiselPrefab);
        chiselName = chiselInfo.name;

        inTurning = true;
        inDrilling = false;

        drillingImage.color = new Color(0.35f, 0.35f, 0.35f, 250f);
        turningImage.color = new Color(0.7f, 0.7f, 0.7f, 250f);
    }

    private void Update()
    {
        TargetSpinning();
    }

    public void ReplaceChisel(GameObject chiselPrefab)
    {
        if(inTurning == true)
        {
            if (chiselGo && chiselGo.name == chiselPrefab.name) return;

            Vector3 chiselOldPosition = chiselSpawnTransform.position;

            if (chiselGo)
            {
                chiselOldPosition = chiselPos.transform.position;
                Object.Destroy(chiselGo);
            }

            chiselGo = Object.Instantiate(chiselPrefab, chiselOldPosition, Quaternion.identity);
            chiselGo.GetComponent<TurningChisel>().Subject = targetGo.GetComponent<TurningController>();
            chiselGo.GetComponent<ChiselOverlapCondition>().Subject = targetGo.GetComponent<TurningController>();
        }
    }

    public void ReplaceDrill(GameObject drillPrefab)
    {
        if (inDrilling == true)
        {
            if (drillGo && drillGo.name == drillPrefab.name) return;

            drillOldPosition = drillSpawnTransform.position;

            if (drillGo)
            {
                drillOldPosition = drillGo.transform.position;
                Object.Destroy(drillGo);
            }

            drillGo = Object.Instantiate(drillPrefab, drillOldPosition, Quaternion.identity);
            drillGo.GetComponent<TurningDrill>().Subject = targetGo.GetComponent<TurningController>();
        }
    }

    public void TargetSpinning()
    {
        if(TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true))
        {
            Transform targetTransform = targetGo.transform;
            targetTransform.Rotate(new Vector3(cylinderSpinningSpeed * Time.deltaTime, 0f, 0f));
        }
    }

    public void ResetTarget()
    {
        Transform targetTransform = targetGo.transform;
        targetTransform.position = new Vector3(startValueCache.targetSpawnTransform.position.x - targetGo.GetComponent<TurningController>().width / 2 + 0.15f, startValueCache.targetSpawnTransform.position.y, startValueCache.targetSpawnTransform.position.z);
        targetTransform.rotation = startValueCache.targetSpawnTransform.rotation;
        targetGo.GetComponent<TurningController>().ResetState();
    }

    public void GoToDrilling()
    {
        if(inTurning == false || -TurningController.baseXpos.x - 0.079f >= 1f)
        {
            return;
        }
        else
        {
            inTurning = false;
            inDrilling = true;

            drillingImage.color = new Color(0.7f, 0.7f, 0.7f, 250f);
            turningImage.color = new Color(0.35f, 0.35f, 0.35f, 250f);

            ChiselInfo drillInfo = drillTable.GetChiselInfoAtIndex(0);
            drillPrefab = drillInfo.prefab;

            ReplaceDrill(drillPrefab);
            edge.SetActive(false);
        }
    }

    public void GoToTurning()
    {
        if(inDrilling == false)
        {
            return;
        }
        else
        {
            if(drillGo)
            {
                drillOldPosition = drillGo.transform.position;
                Destroy(drillGo);
            }

            inTurning = true;
            inDrilling = false;

            drillingImage.color = new Color(0.35f, 0.35f, 0.35f, 250f);
            turningImage.color = new Color(0.7f, 0.7f, 0.7f, 250f);

            ChiselInfo chiselInfo = chiselTable.GetChiselInfoAtIndex(2);
            chiselPrefab = chiselInfo.prefab;

            ReplaceChisel(chiselPrefab);
            edge.SetActive(true);
        }
    }
}
