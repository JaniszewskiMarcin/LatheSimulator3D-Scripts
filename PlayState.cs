using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayState 
{
//    protected PlaySystem system;

//    public PlayState(PlaySystem system)
//    {
//        this.system = system;
//    }

//    public abstract void Enter();

//    public abstract void Exit();

//    public abstract void Update();

//    public abstract void Reset();
//}

//public class TurningState : PlayState
//{

//    public GameObject targetGo;

//    public GameObject parent;

//    private GameObject chiselGo;

//    public Transform targetTransform;

//    public Transform chiselSpawnTransform;

//    public TurningState(PlaySystem system) : base(system)
//    {
//    }

//    public void ReplaceChisel(GameObject chiselPrefab)
//    {
//        //parent = GameObject.Find("KnifeSupport");
//        //chiselGo.transform.parent = parent.transform;

//        if (chiselGo && chiselGo.name == chiselPrefab.name) return;

//        Vector3 chiselOldPosition = chiselSpawnTransform.position;
//        if (chiselGo)
//        {
//            chiselOldPosition = chiselGo.transform.position;
//            Object.Destroy(chiselGo);
//        }

//        chiselGo = Object.Instantiate(chiselPrefab, chiselOldPosition, Quaternion.identity);
//        chiselGo.GetComponent<TurningChisel>().Subject = targetGo.GetComponent<TurningController>();
//    }

//    public override void Enter()
//    {
//        GUIEventDispatcher.Instance.NotifyEvent(GUIEventID.EnterTurning);
//    }

//    public override void Exit()
//    {
//        Object.Destroy(chiselGo);

//        GUIEventDispatcher.Instance.NotifyEvent(GUIEventID.ExitTurning);
//    }

//    public override void Update()
//    {
//        //if (SelectedObjects.inTurning == true)
//        //{
//        targetTransform.Rotate(new Vector3(TurningController.spiningSpeed * Time.deltaTime, 0f, 0f));
//        //}
//    }

//    public override void Reset()
//    {
//        chiselGo.GetComponent<TurningChisel>().ChangePosition(chiselSpawnTransform.position);
//        targetGo.GetComponent<TurningController>().ResetState();
//    }
}

