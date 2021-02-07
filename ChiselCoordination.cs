using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChiselCoordination : MonoBehaviour
{
    [SerializeField] Text valueText;
    [SerializeField] Transform chiselPos;
    [SerializeField] Transform targetPos;
    Vector3 position;

    private void Start()
    {
        valueText = GetComponent<Text>();
        //if(targetPos.gameObject != null)
        //{
        //    chiselPos.transform.position = chiselPos.transform.InverseTransformPoint(targetPos.position);

        //}
    }

    private void Update()
    {
        if(gameObject.name == "X")
        {
            xUpdate();
        }
        if (gameObject.name == "Y")
        {
            yUpdate();
        }
    }

    public void xUpdate() //Przypisujemy wartość wprowadzoną z suwaka
    {
        position = new Vector3(-chiselPos.transform.position.x - 0.234f, -chiselPos.transform.position.y, chiselPos.transform.position.z);
        valueText.text = position.x.ToString("X : " + "0.000" + " [m]");
    }

    public void yUpdate() //Przypisujemy wartość wprowadzoną z suwaka
    {
        position = new Vector3(-chiselPos.transform.position.x - 0.234f, -chiselPos.transform.position.y, chiselPos.transform.position.z);
        valueText.text = position.y.ToString("Y : " + "0.000" + " [m]");
    }
}
