using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChiselInfo", menuName = "ScriptableObjects/ChiselInfo", order = 1)]
public class ChiselInfo : ScriptableObject
{
    public string nameInfo;
    public Texture2D buttonTexture;
    public GameObject prefab;
}
