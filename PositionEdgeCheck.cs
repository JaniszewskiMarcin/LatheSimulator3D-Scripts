using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionEdgeCheck : MonoBehaviour      //Skrypt załączony do dwóch obiektów "Check", które określają odległość dokręcenia konika 
{
    Vector3 posEdgeCheck;

    private void Update()
    {
        MakeEdgeDimension();
    }

    public void MakeEdgeDimension()
    {
        posEdgeCheck = new Vector3(TurningController.baseXpos.x, 0f, 0f);
        transform.position = posEdgeCheck;
    }
}
