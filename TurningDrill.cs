using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector2i = ClipperLib.IntPoint;
using Polygon = System.Collections.Generic.List<ClipperLib.IntPoint>;
using System.Diagnostics.Tracing;

public class TurningDrill : MonoBehaviour
{
    [SerializeField] GameObject drillingSpawnTransform;

    [SerializeField] ParticleSystem turningEfect;

    protected TurningController subject;
    public TurningController Subject { set { subject = value; } get { return subject; } }

    protected Vector2[] basePolygon;    //bazowy poligon kwadratowy dłuta składajacy się z 4 pozycji

    protected List<Vector2i> clipPolygon = new List<Vector2i>();

    protected List<Vector2> clipPoints = new List<Vector2>();
    public List<Vector2i> Polygon => clipPolygon;

    private Vector2 currentPosition;

    private Vector2 previousPosition;

    private BoundingBox bound;

    protected bool contacting = false;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        drillingSpawnTransform = GameObject.Find("DrillingSpawnTransform");
        turningEfect.Stop();

        previousPosition = transform.position;

        Vector2 offset = Vector2.zero;
        Vector2[] points = null;

        if (GetComponent<DrillShape>())
        {
            var drillShape = GetComponent<DrillShape>();
            points = drillShape.points;                    //punkty poligonów w przestrzeni reprezentujące kształt
            offset = drillShape.offset;
        }

        float clampOffset = float.MinValue;
        basePolygon = new Vector2[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            basePolygon[i] = points[i] + offset;
            if (clampOffset < basePolygon[i].y) clampOffset = basePolygon[i].y;     //clampOffset to dlugosc połowy wychylenia w osi Y poligonu dłuta
        }

        ContactEnd();   //stopuje efekt cięcia drewna, zmniejsza predkosc dłuta

        StartCoroutine(PreventContactingWhenPositionNotChanged(0.5f)); //po wybranym czasie wióry przestają lecieć gdy dłuto sie nie porusza
    }

    private void Update()
    {
        transform.position = drillingSpawnTransform.transform.position;

        if (subject)
        {

            if (!transform.hasChanged)      //jeśli transformacja się nie zmienia nic nie rób
            {
                return;
            }

            transform.hasChanged = false;

            currentPosition = transform.localPosition;

            BuildClipPolygon(previousPosition, currentPosition);

            if (subject.OverlapChisel(clipPoints, bound)) //jeśli materiał zazębia się z dłutem -> prawda
            {
                if (!contacting)        //w razie bugu ręcznie zmieniamy że dłuto wchodzi w kontakt z materiałem
                {
                    contacting = true;
                    ContactBegin();
                }
                subject.ClipDrill(this);     //funkcja do edycji siatki w czasie          
            }
            else
            {
                if (contacting)     //jeśli się nie zazębiają kończy się kontakt zmienić prędkość
                {
                    contacting = false;
                    ContactEnd();
                }
            }
            previousPosition = currentPosition;
        }
    }

    public void ChangePosition(Vector3 position) //sprawdzamy czy zmieniła się pozycja dłuta
    {
        transform.position = position;

        if (previousPosition != currentPosition)
        {
            transform.hasChanged = true;
        }

        previousPosition = currentPosition = position;
    }

    void ContactBegin()     //kiedy dłuto zaczyna stykać się z obrabianym elementem
    {
        //chiselDrag.OnChiselContactBegin();
        if (!turningEfect.isPlaying) turningEfect.Play();
        audioSource.loop = true;
        if (!audioSource.isPlaying) audioSource.Play();
    }

    void ContactEnd()       //kiedy dłuto cofa się od obrabianego elementu
    {
        //chiselDrag.OnChiselContactEnd();
        if (turningEfect.isPlaying) turningEfect.Stop();
        if (audioSource.isPlaying) audioSource.Stop();
    }

    void BuildClipPolygon(Vector2 begin, Vector2 end)   //funkcja budująca poligon dłuta potrzebny do wykorzystania biblioteki clipper
    {
        Vector2 delta = end - begin;
        int a = 0, b = 0;
        float max_eval = float.MinValue;
        float min_eval = float.MaxValue;
        Vector2 p;
        for (int i = 0; i < basePolygon.Length; i++)
        {
            p = basePolygon[i];
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

        clipPolygon.Clear();
        clipPoints.Clear();

        bound.pointA.x = float.MaxValue;
        bound.pointA.y = float.MaxValue;
        bound.pointB.x = float.MinValue;
        bound.pointB.y = float.MinValue;

        int k = a;
        clipPolygon.Add((basePolygon[k] + begin).ToVector2i());
        while (k != b)
        {
            p = basePolygon[k] + end;
            clipPoints.Add(p);
            clipPolygon.Add(p.ToVector2i());

            k = (k + 1) % basePolygon.Length;

            bound.pointA.x = Mathf.Min(bound.pointA.x, p.x);
            bound.pointA.y = Mathf.Min(bound.pointA.y, p.y);
            bound.pointB.x = Mathf.Max(bound.pointB.x, p.x);
            bound.pointB.y = Mathf.Max(bound.pointB.y, p.y);

        }

        k = b;
        clipPolygon.Add((basePolygon[b] + end).ToVector2i());
        while (k != a)
        {
            p = basePolygon[k] + begin;
            clipPoints.Add(p);
            clipPolygon.Add(p.ToVector2i());

            k = (k + 1) % basePolygon.Length;

            bound.pointA.x = Mathf.Min(bound.pointA.x, p.x);
            bound.pointA.y = Mathf.Min(bound.pointA.y, p.y);
            bound.pointB.x = Mathf.Max(bound.pointB.x, p.x);
            bound.pointB.y = Mathf.Max(bound.pointB.y, p.y);
        }
    }

    IEnumerator PreventContactingWhenPositionNotChanged(float maxDuration)      //po wybranym czasie wióry przestają lecieć gdy dłuto sie nie porusza
    {
        float time = maxDuration;

        while (true)
        {
            if (transform.hasChanged)
            {
                time = maxDuration;
            }

            time -= Time.deltaTime;

            if (time < 0f)
            {
                if (contacting)
                {
                    contacting = false;
                    ContactEnd();
                }
            }

            yield return null;
        }
    }
}
