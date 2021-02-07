using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankOnPlace : MonoBehaviour       //Skrypt podłączony do stożka konika
{
    //Zadeklarowanie zmiennej logicznej określająca czy konik jest na miejscu.
    public static bool isCrankOnPlace = false;

    //Funkcja biblioteki Unity, wykonująca się wtedy kiedy jeden obiekt z komponentem 
    //"Collider" ulegnie kolizji z kolejnym określonym w argumencie.
    public void OnTriggerEnter(Collider other)
    {
        //Instrukcja sterująca, która ma za zadanie upewnić się, że obiekt
        //który uległ kolizji z element konika to "EdgeCheck".
        if (other.gameObject.name == "EdgeCheck")
        {
            //Jeśli tak to zmienna logiczna zmieniana jest na wartość prawdy,
            //co nie pozwala na wywołanie błędu, przy określonej długości materiału
            //oraz włączonych obrotach wrzeciona.
            isCrankOnPlace = true;
        }
    }

    //Funkcja biblioteki Unity, wykonująca się wtedy kiedy jeden obiekt z komponentem 
    //"Collider" przestanie ulegać kolizji z kolejnym określonym w argumencie.
    public void OnTriggerExit(Collider other)
    {
        //Instrukcja sterująca, która ma za zadanie upewnić się, że obiekt
        //który przestanie ulegać kolizji z element konika to "EdgeCheck".
        if (other.gameObject.name == "EdgeCheck")
        {
            //Jeśli tak to zmienna logiczna zmieniana jest na wartość fałszu,
            //co pozwala na wywołanie błędu, przy określonej długości materiału
            //oraz włączonych obrotach wrzeciona.
            isCrankOnPlace = false;
        }
    }
}
