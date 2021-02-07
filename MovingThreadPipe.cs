﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingThreadPipe : MonoBehaviour
{
    [SerializeField] GameObject parent;

    private void FixedUpdate()
    {
        if(FeedOrMovePipeLeaver.isThreadPipeRunning == true && TurnOnBut.isTurnOnButtonOn == true && (CylinderRotatingLeaver.isLeftRotatingLeaverOn == true || CylinderRotatingLeaver.isRightRotatingLeaverOn == true))
        {
            parent.transform.Rotate(40f, 0f, 0f);
        }
    }
}
