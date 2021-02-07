using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextReader : MonoBehaviour     //Podłączony do tekstu ukazującego wymiary materiału, pobiera je i zapisuje
{
    Text valueText;
    [SerializeField] InputField inputHeight;
    [SerializeField] InputField inputWidth;
    float checkValueHorse = 0f;
    float checkValueSupport = 0f;
    [SerializeField] GameObject target;
    [SerializeField] GameObject edge;
    [SerializeField] GameObject chiselPos;
    GameObject drill;
    GameObject dirtParticle;
    public static bool materialInterferenceWithHorse = false;
    public static bool materialInterferenceWithSupport = false;

    private void Start()
    {
        valueText = GetComponent<Text>();
    }

    private void Update()
    {
        CheckInterferenceSupport(checkValueSupport);
        CheckInterferenceHorse(checkValueHorse);

        if (PlaySystem.inDrilling)
        {
            drill = GameObject.FindObjectOfType<TurningDrill>().gameObject;
            dirtParticle = drill.transform.GetChild(0).gameObject;
        }
    }

    //Metoda przekształcająca wartość promienia wpisywaną w panelu interfejsu do zmiennej liczbowej,
    //oraz przypisuje ją do wymiarów materiału obrabianego.
    public void HeightValueChange(string newHeight)
    {
        //Zmienna pomocnicza potrzebna przy użyciu metody "TryParse".
        int result = 0;

        //Jeśli nie wpisano nic do panelu dalszy kod nie wykona się.
        if(newHeight == null)
        {
            return;
        }

        //Metoda statyczna konwerująca typ "string" do zmiennej liczbowej "int"
        int.TryParse(newHeight, out result);

        //Przypisanie wartości wpisanego przez użytkownika promienia do kolejnej 
        //zmiennej zadeklarowanej w całej klasie, oraz przekształcenie jej do poprawnej jednostki.
        checkValueSupport = result / 1000f;

        //Instrukcja sterująca sprawdzająca czy wartości nie są zbyt wielkie lub zbyt małe.
        if (checkValueSupport > 0.2f)
        {
            checkValueSupport = 0.2f;
            inputHeight.text = "200";
        }
        else if(checkValueSupport < 0.02f)
        {
            checkValueSupport = 0.02f;
            inputHeight.text = "20";
        }

        //Metoda sprawdza czy na miejscu materiału tej wielkości nie znajduje się suport wraz z dłutem.
        CheckInterferenceSupport(checkValueSupport);

        //Instrukcja sterująca wykonuje się jeśli wszystko jest poprawnie ustawione.
        if (!materialInterferenceWithSupport)
        {
            //Przypisanie nowej wartości promienia do materiału obrabianego.
            target.GetComponent<TurningController>().height = Mathf.Round(checkValueSupport * 1000f) / 1000f;
        }
    }

    public void WidthValueChange(string newWidth) //Funkcja pobierająca wprowadzoną długość przypisuje nową
    {
        int result = 0;

        if(newWidth == null)
        {
            return;
        }

        int.TryParse(newWidth, out result);
        checkValueHorse = result / 1000f;

        if (checkValueHorse > 4.5f)
        {
            checkValueHorse = 4.5f;
            inputWidth.text = "4500";
        }
        else if(checkValueHorse < 0.1f)
        {
            checkValueHorse = 0.1f;
            inputWidth.text = "100";
        }
        CheckInterferenceHorse(checkValueHorse);
        if (!materialInterferenceWithHorse)
        {
            target.GetComponent<TurningController>().width = (Mathf.Round(checkValueHorse * 1000f) / 1000f) + 0.15f;
        }
    }

    //public void textUpdate(float value) //Przypisujemy wartość wprowadzoną z suwaka
    //{
    //    valueText.text = value.ToString("0.000" + " m");
    //}

    public bool CheckInterferenceHorse(float width)
    {
        if(PlaySystem.inTurning)
        {
            if (edge.transform.position.x > Vector3.zero.x - width - 0.15f)
            {
                materialInterferenceWithHorse = true;
            }
            else
            {
                materialInterferenceWithHorse = false;
            }
        }

        if(PlaySystem.inDrilling)
        {
            if (dirtParticle.transform.position.x > Vector3.zero.x - width - 0.15f || edge.transform.position.x > Vector3.zero.x - width - 0.15f)
            {
                materialInterferenceWithHorse = true;
            }
            else
            {
                materialInterferenceWithHorse = false;
            }
        }

        return materialInterferenceWithHorse;
    }

    //Metoda sprawdza czy na miejscu materiału tej wielkości nie znajdują się suport wraz z dłutem.
    //Przyjmuje jeden argument w postaci liczby zmiennoprzecinkowej, oraz zwraca wartość logiczną typu "bool".
    public bool CheckInterferenceSupport(float height)
    {
        //Instrukcja sterująca sprawdzająca czy dłuto znajduje się w miejscu pojawienia się nowego 
        //materiału obrabianego o wybranej przez użytkownika wartości promieniu.
        if (chiselPos.transform.position.y >= Vector3.zero.y - height)
        {
            //Jeśli tak, ustawiana jest wartość zmienianej logicznej, która nie zezwala na wstawienie
            //materiału obrabianego.
            materialInterferenceWithSupport = true;
        }
        else
        {
            //Jeśli nic nie stoi na przeszkodzie do umieszczenia we wrzecionie materiału obrabianego,
            //zmienna logiczna ustawiana jest na wartość fałszu.
            materialInterferenceWithSupport = false;
        }

        //Zwrócenie wartości funkcji w postaci zmiennej logicznej.
        return materialInterferenceWithSupport;
    }
}
