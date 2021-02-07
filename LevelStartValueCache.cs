using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartValueCache : MonoBehaviour   //Przechowuje pozycje powielania noża tokarskiego oraz materiału obrabianego
{

    public Transform targetSpawnTransform;

    public Transform chiselSpawnTransform;

    public static Transform targetSpawnPos;

    public static Transform chiselSpawnPos;

    private void Awake()    //Inicjalizuje na zmiennej statycznej pozycję materiału obrabianego
    {
        targetSpawnPos = targetSpawnTransform;
        chiselSpawnPos = chiselSpawnTransform;
    }
}
