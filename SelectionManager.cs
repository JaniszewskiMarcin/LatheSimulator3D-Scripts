using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour   //Skrypt załączony do obiektu w scenie "SelectionManager" opisuje on zaznaczanie i wykrywanie obiektów
{
    public static string selectedObject;
    private Transform _selection;
    private string selectableTag = "selectable";
    [SerializeField] Material outlineMaterial;
    Material normalMaterial;

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (SupportFuncs.cameraTurningOn == true)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            if (selectionRenderer != null)
            {
                if (selectionRenderer.material == normalMaterial)
                {
                    return;
                }
                else
                {
                    selectionRenderer.material = normalMaterial;
                }
            }
        }

        else
        {
            if (_selection != null)
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = normalMaterial;
                }
                _selection = null;
            }

            if (Physics.Raycast(ray, out hit))
            {
                selectedObject = hit.transform.gameObject.name;
                var selection = hit.transform;
                if (selection.CompareTag(selectableTag))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if (selectionRenderer != null)
                    {
                            normalMaterial = selectionRenderer.material;
                            selectionRenderer.material = outlineMaterial;
                    }
                    _selection = selection;
                }
            }
        }

    }
}



