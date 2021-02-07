using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SupportWithMaterialInterference : MonoBehaviour
{
    [SerializeField] GameObject target;

    private void Update()
    {
        transform.position = target.transform.position;
        transform.localScale = new Vector3(target.GetComponent<TurningController>().height * 2f, target.GetComponent<TurningController>().width/2, target.GetComponent<TurningController>().height * 2f);
    }
}
