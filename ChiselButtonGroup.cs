using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class ChiselButtonGroup : MonoBehaviour
{
    public GameObject activeChiselPrefab;
    public ChiselTable chiselTable;
    public PlaySystem playSystem;
    int x = 2;

    private struct ChiselButtonData
    {
        public Image layoutImage;
        public GameObject chiselPrefab;
    }

    private List<ChiselButtonData> chiselButtonDatas = new List<ChiselButtonData>();

    private void Awake()
    {
        //int count = Mathf.Min(transform.childCount, chiselTable.Count);

        for (int i = 0; i < 5; i++)
        {
            var child = transform.GetChild(i);

            Image layoutImage = child.GetComponent<Image>();
            Image image = child.GetChild(0).GetComponent<Image>();

            ChiselInfo chiselInfo = chiselTable.GetChiselInfoAtIndex(x);
            x = x + 5;
            GameObject chiselPrefab = chiselInfo.prefab;
            Texture2D buttonTexture = chiselInfo.buttonTexture;

            image.sprite = Sprite.Create(buttonTexture, new Rect(0, 0, buttonTexture.width, buttonTexture.height), new Vector2(0.5f, 0.5f));

            int k = i;
            child.GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectButton(k);
            });

            chiselButtonDatas.Add(new ChiselButtonData { layoutImage = layoutImage, chiselPrefab = chiselPrefab });
        }
    }

    private void Update()
    {
        if(PlaySystem.inDrilling == true)
        {
            for (int i = 0; i < chiselButtonDatas.Count; i++)
            {
                chiselButtonDatas[i].layoutImage.color = new Color(0.35f, 0.35f, 0.35f, 0.6f);
            }
        }
    }

    void SelectButton(int idx)
    {
        if(PlaySystem.inTurning == true)
        {
            for (int i = 0; i < chiselButtonDatas.Count; i++)
            {
                chiselButtonDatas[i].layoutImage.color = new Color(0.35f, 0.35f, 0.35f, 0.6f);
            }

            var chiselButtonData = chiselButtonDatas[idx];
            chiselButtonData.layoutImage.color = Color.white;

            SetChisel(chiselButtonData.chiselPrefab);
        }
    }

    private void SetChisel(GameObject chiselPrefab)
    {
        activeChiselPrefab = FindObjectOfType<TurningChisel>().gameObject;

        if (SupportLeaver.isSupportLeaverOnPlace == false && (activeChiselPrefab.name == "Square_Chisel(Clone)" || activeChiselPrefab.name == "Rounded_Chisel(Clone)" || activeChiselPrefab.name == "Triangle_Chisel(Clone)" || activeChiselPrefab.name == "TrapezRight_Chisel(Clone)" || activeChiselPrefab.name == "TrapezLeft_Chisel(Clone)"))
        {
            playSystem.ReplaceChisel(chiselPrefab);
        }
        else
        {
            Debug.Log("Nie zmieniam");
        }
    }
}
