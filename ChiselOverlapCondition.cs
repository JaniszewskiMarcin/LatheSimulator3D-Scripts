using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector2i = ClipperLib.IntPoint;
using Polygon = System.Collections.Generic.List<ClipperLib.IntPoint>;

public class ChiselOverlapCondition : MonoBehaviour
{
    float startTimeGoingTooDeep = 0f;

    bool goingTooDeep = false;

    public static float counter = 0;

    protected TurningController subject;

    protected ChiselMovement chiselDrag;
    public TurningController Subject { set { subject = value; } get { return subject; } }

    protected Vector2[] basePolygonCheck;

    protected List<Vector2i> clipPolygonCheck = new List<Vector2i>();

    protected List<Vector2> clipPointsCheck = new List<Vector2>();

    public List<Vector2i> PolygonCheck => clipPolygonCheck;

    private Vector2 currentPosition;

    private Vector2 previousPosition;

    private BoundingBox boundCheck;

    protected bool contacting = false;

    Vector2 offsetCheck = Vector2.zero;

    Vector2 offsetPos = Vector2.zero;



    void Start()
    {
        //if(GameManager.counterSquare >= 5)
        //{
        //    GameManager.counterSquare = 0;
        //}
        //if (GameManager.counterRound >= 5)
        //{
        //    GameManager.counterRound = 0;
        //}
        //if (GameManager.counterTriangle >= 5)
        //{
        //    GameManager.counterTriangle = 0;
        //}
        //if (GameManager.counterTrapezLeft >= 5)
        //{
        //    GameManager.counterTrapezLeft = 0;
        //}
        //if (GameManager.counterTrapezRight >= 5)
        //{
        //    GameManager.counterTrapezRight = 0;
        //}

        previousPosition = transform.position;

        offsetCheck = Vector2.zero;
        Vector2[] pointsCheck = null;

        if (GetComponent<ChiselCheckShape>())
        {
            var chiselCheckShape = GetComponent<ChiselCheckShape>();
            pointsCheck = chiselCheckShape.pointsCheck;                    //punkty poligonów w przestrzeni reprezentujące kształt
            offsetCheck = chiselCheckShape.offsetCheck;
        }

        basePolygonCheck = new Vector2[pointsCheck.Length];

        for (int i = 0; i < pointsCheck.Length; i++)
        {
            basePolygonCheck[i] = pointsCheck[i] + offsetCheck;
        }
    }

    //Funkcja, która wywołuje raz się w każdej klatce działania programu.
    void Update()
    {
        //Sprawdzamy czy materiał obrabiany jest aktywny.
        if (subject)
        {
            //Przypisujemy aktualną pozycję dłuta do zmiennej lokalnej.
            currentPosition = transform.localPosition;

            //Sprawdzamy czy pozycja dłuta uległa zmianie.
            if (currentPosition.x != previousPosition.x)
            {
                //Metoda odpowiedzialna za ustawienie poprawnego wychylenia kształtu 
                //w lewo lub prawo poza kształt bazowy odpowiedzialny za skrawanie materiału.
                SetOffset(currentPosition, previousPosition);

                //Funkcja przenosząca kształt wykrywający zbyt głębokie skrawanie do aktualnej pozycji.
                BuildClipPolygonCheck(previousPosition, currentPosition);

                //Jeśli kształt zazębia się z materiałem wykonujemy dalszy kod.
                if (subject.OverlapChisel(clipPointsCheck, boundCheck)) //jeśli materiał zazębia się z dłutem -> prawda
                {
                    //Metoda odpowiedzialna za nastawę zmiennej logicznej o stanie głębokości skrawania 
                    //oraz czasu w którym oba elementy uległy zazębieniu.
                    StartOfGoingTooDeep();

                    //Metoda odpowiedzialna za dodanie nastawionej wartości do zmiennej określającej wytrzymałość dłuta.
                    CheckIfTurningTooDeep();
                }
            }
            //Przypisujemy aktualną pozycję dłuta do zmiennej lokalnej pod koniec wykonywania się funkcji Update.
            previousPosition = currentPosition;
        }
    }

    public void SetOffset(Vector3 actualPos, Vector3 previousPos)
    {
        offsetCheck = Vector2.zero;

        if (gameObject.tag == "Normal")
        {
            if (actualPos.x > previousPos.x)
            {
                offsetCheck.x = 0.005f;
            }

            if (actualPos.x < previousPos.x)
            {
                offsetCheck.x = -0.005f;
            }

            if (actualPos.x == previousPos.x)
            {
                offsetCheck.x = 0f;
            }
        }

        if (gameObject.tag == "30Angle")
        {
            if (actualPos.x > previousPos.x)
            {
                offsetPos.x = ((Mathf.Sqrt(3f) * 0.0025f) / 2f);
                offsetPos.y = 0.5f * 0.0025f;
            }

            if (actualPos.x < previousPos.x)
            {
                offsetPos.x = -((Mathf.Sqrt(3f) * 0.0025f) / 2f);
                offsetPos.y = -0.5f * 0.0025f;
            }

            if (actualPos.x == previousPos.x)
            {
                offsetPos.x = 0f;
                offsetPos.y = 0f;
            }
        }

        if (gameObject.tag == "45Angle")
        {
            if (actualPos.x > previousPos.x)
            {
                offsetPos.x = 0.0025f * Mathf.Sqrt(2f);
                offsetPos.y = 0.0025f * Mathf.Sqrt(2f);
            }

            if (actualPos.x < previousPos.x)
            {
                offsetPos.x = -0.0025f * Mathf.Sqrt(2f);
                offsetPos.y = -0.0025f * Mathf.Sqrt(2f);
            }

            if (actualPos.x == previousPos.x)
            {
                offsetPos.x = 0f;
                offsetPos.y = 0f;
            }
        }

        if (gameObject.tag == "30NegAngle")
        {
            if (actualPos.x > previousPos.x)
            {
                offsetPos.x = ((Mathf.Sqrt(3f) * 0.0025f) / 2f);
                offsetPos.y = -0.5f * 0.0025f;
            }

            if (actualPos.x < previousPos.x)
            {
                offsetPos.x = -((Mathf.Sqrt(3f) * 0.0025f) / 2f);
                offsetPos.y = -0.5f * 0.0025f;
            }

            if (actualPos.x == previousPos.x)
            {
                offsetPos.x = 0f;
                offsetPos.y = 0f;
            }
        }

        if (gameObject.tag == "45NegAngle")
        {
            if (actualPos.x > previousPos.x)
            {
                offsetPos.x = 0.0025f * Mathf.Sqrt(2f);
                offsetPos.y = -0.0025f * Mathf.Sqrt(2f);
            }

            if (actualPos.x < previousPos.x)
            {
                offsetPos.x = -0.0025f * Mathf.Sqrt(2f);
                offsetPos.y = 0.0025f * Mathf.Sqrt(2f);
            }

            if (actualPos.x == previousPos.x)
            {
                offsetPos.x = 0f;
                offsetPos.y = 0f;
            }
        }

        GetComponent<ChiselCheckShape>().offsetCheck = offsetCheck;
    }

    public void StartOfGoingTooDeep()
    {
        if (goingTooDeep == false)
        {
            startTimeGoingTooDeep = Time.time;
            goingTooDeep = true;
        }
    }

    void CheckIfTurningTooDeep()
    {
        if (goingTooDeep == true && Time.time >= startTimeGoingTooDeep + 0.1f)
        {
            if(gameObject.name == "Square_Chisel(Clone)" || gameObject.name == "Square_Chisel 30(Clone)" || gameObject.name == "Square_Chisel 45(Clone)" || gameObject.name == "Square_Chisel-30(Clone)" || gameObject.name == "Square_Chisel-45(Clone)")
            {
                GameManager.counterSquare += 0.1f;
            }
            if (gameObject.name == "Rounded_Chisel(Clone)" || gameObject.name == "Rounded_Chisel 30(Clone)" || gameObject.name == "Rounded_Chisel 45(Clone)" || gameObject.name == "Rounded_Chisel-30(Clone)" || gameObject.name == "Rounded_Chisel-45(Clone)")
            {
                GameManager.counterRound += 0.1f;
            }
            if (gameObject.name == "Triangle_Chisel(Clone)" || gameObject.name == "Triangle_Chisel 30(Clone)" || gameObject.name == "Triangle_Chisel 45(Clone)" || gameObject.name == "Triangle_Chisel-30(Clone)" || gameObject.name == "Triangle_Chisel-45(Clone)")
            {
                GameManager.counterTriangle += 0.1f;
            }
            if (gameObject.name == "TrapezLeft_Chisel(Clone)" || gameObject.name == "TrapezLeft_Chisel 30(Clone)" || gameObject.name == "TrapezLeft_Chisel 45(Clone)" || gameObject.name == "TrapezLeft_Chisel-30(Clone)" || gameObject.name == "TrapezLeft_Chisel-45(Clone)")
            {
                GameManager.counterTrapezLeft += 0.1f;
            }
            if (gameObject.name == "TrapezRight_Chisel(Clone)" || gameObject.name == "TrapezRight_Chisel 30(Clone)" || gameObject.name == "TrapezRight_Chisel 45(Clone)" || gameObject.name == "TrapezRight_Chisel-30(Clone)" || gameObject.name == "TrapezRight_Chisel-45(Clone)")
            {
                GameManager.counterTrapezRight += 0.1f;
            }
            goingTooDeep = false;
        }
    }

    void BuildClipPolygonCheck(Vector2 begin, Vector2 end)   //funkcja budująca poligon dłuta potrzebny do wykorzystania biblioteki clipper
    {
        Vector2 delta = end - begin;
        int a = 0, b = 0;
        float max_eval = float.MinValue;
        float min_eval = float.MaxValue;
        Vector2 p;
        for (int i = 0; i < basePolygonCheck.Length; i++)
        {
            p = basePolygonCheck[i];
            float eval = -delta.y * p.x + delta.x * p.y;
            if (eval < min_eval)
            {
                min_eval = eval;
                a = i;
            }

            if (eval > max_eval)
            {
                max_eval = eval;
                b = i;
            }
        }

        clipPointsCheck.Clear();

        boundCheck.pointA.x = float.MaxValue;
        boundCheck.pointA.y = float.MaxValue;
        boundCheck.pointB.x = float.MinValue;
        boundCheck.pointB.y = float.MinValue;

        int k = a;
        while (k != b)
        {
            p = basePolygonCheck[k] + end + offsetCheck + offsetPos;
            clipPointsCheck.Add(p);

            k = (k + 1) % basePolygonCheck.Length;

            boundCheck.pointA.x = Mathf.Min(boundCheck.pointA.x, p.x);
            boundCheck.pointA.y = Mathf.Min(boundCheck.pointA.y, p.y);
            boundCheck.pointB.x = Mathf.Max(boundCheck.pointB.x, p.x);
            boundCheck.pointB.y = Mathf.Max(boundCheck.pointB.y, p.y);

        }

        k = b;
        while (k != a)
        {
            p = basePolygonCheck[k] + begin + offsetCheck + offsetPos;
            clipPointsCheck.Add(p);

            k = (k + 1) % basePolygonCheck.Length;

            boundCheck.pointA.x = Mathf.Min(boundCheck.pointA.x, p.x);
            boundCheck.pointA.y = Mathf.Min(boundCheck.pointA.y, p.y);
            boundCheck.pointB.x = Mathf.Max(boundCheck.pointB.x, p.x);
            boundCheck.pointB.y = Mathf.Max(boundCheck.pointB.y, p.y);
        }

        for (int i = 0; i <= clipPointsCheck.Count - 2; i++)
        {
            Debug.DrawLine(clipPointsCheck[i], clipPointsCheck[i + 1], Color.red);
        }
        Debug.DrawLine(clipPointsCheck[clipPointsCheck.Count - 1], clipPointsCheck[0], Color.red);
    }
}
