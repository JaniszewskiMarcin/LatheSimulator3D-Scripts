using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour //Skrypt podłączony do "Turning Camera" obejmuje przybliżanie oraz przesuwanie kamery podczas toczenia
{
    [SerializeField] float cameraSens = 5f;
    [SerializeField] float boundSize = 10f;
    [SerializeField] float scrollSpeed = 50f;
    Vector3 camPos;

    private void Update()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        if (gameObject == null)
        {
            return;
        }

        else
        {
            if (SupportFuncs.cameraTurningOn == true)  //Funkcja wykonuje się tylko gdy kamera jest włączona oraz gdy znajdujemy się podczas toczenia
            {
                camPos = transform.localPosition;

                if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= boundSize)
                {
                    camPos.x -= transform.right.x * cameraSens * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - boundSize)
                {
                    camPos.x += transform.right.x * cameraSens * Time.deltaTime;
                }

                float scroll = Input.GetAxis("Mouse ScrollWheel");
                camPos += transform.forward * scroll * scrollSpeed * Time.deltaTime;

                camPos.x = Mathf.Clamp(camPos.x, -5.26f, -0.46f);
                camPos.y = Mathf.Clamp(camPos.y, -3.59128f, -0.3973357f);
                camPos.z = Mathf.Clamp(camPos.z, 0.4039186f, 3.541734f);

                transform.localPosition = camPos;
            }
        }
    }
}
