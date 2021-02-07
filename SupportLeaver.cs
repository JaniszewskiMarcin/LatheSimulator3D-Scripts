using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportLeaver : MonoBehaviour
{
    [SerializeField] GameObject parent;

    public static bool isSupportLeaverOnPlace = false;

    //private void OnMouseOver()
    //{
    //    ChangeLeaverPos();
    //}

    //public void ChangeLeaverPos()
    //{
    //        if (Input.GetKeyDown("e") && isSupportLeaverOnPlace == false)
    //        {
    //            parent.transform.Rotate(0f, 0f, 45f);
    //            isSupportLeaverOnPlace = true;
    //        }

    //        if (Input.GetKeyDown("q") && isSupportLeaverOnPlace == true)
    //        {
    //            parent.transform.Rotate(0f, 0f, -45f);
    //            isSupportLeaverOnPlace = false;
    //        }
    //}
}
