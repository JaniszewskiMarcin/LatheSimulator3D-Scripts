using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurningDrillShape : MonoBehaviour
{
    public GameObject activeDrillPrefab;
    public ChiselTable drillTable;
    public PlaySystem playSystem;

    private struct DrillButtonData
    {
        public Image layoutImage;
        public GameObject drillPrefab;
    }

    private List<DrillButtonData> drillButtonDatas = new List<DrillButtonData>();

    private void Awake()
    {

        for (int i = 0; i < 5; i++)
        {
            var child = transform.GetChild(i);

            Image layoutImage = child.GetComponent<Image>();
            Image image = child.GetChild(0).GetComponent<Image>();

            ChiselInfo drillInfo = drillTable.GetChiselInfoAtIndex(i);
            GameObject drillPrefab = drillInfo.prefab;
            Texture2D buttonTexture = drillInfo.buttonTexture;

            image.sprite = Sprite.Create(buttonTexture, new Rect(0, 0, buttonTexture.width, buttonTexture.height), new Vector2(0.5f, 0.5f));

            int k = i;
            child.GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectButton(k);
            });

            drillButtonDatas.Add(new DrillButtonData { layoutImage = layoutImage, drillPrefab = drillPrefab });
        }
    }

    private void Update()
    {
        if(PlaySystem.inTurning == true)
        {
            for (int i = 0; i < drillButtonDatas.Count; i++)
            {
                drillButtonDatas[i].layoutImage.color = new Color(0.35f, 0.35f, 0.35f, 0.6f);
            }
        }
    }

    void SelectButton(int idx)
    {
        if(PlaySystem.inDrilling == true && ((CylinderRotatingLeaver.isLeftRotatingLeaverOn == false || CylinderRotatingLeaver.isRightRotatingLeaverOn == false) || TurnOnBut.isTurnOnButtonOn == false))
        {
            for (int i = 0; i < drillButtonDatas.Count; i++)
            {
                drillButtonDatas[i].layoutImage.color = new Color(0.35f, 0.35f, 0.35f, 0.6f);
            }

            var drillButtonData = drillButtonDatas[idx];
            drillButtonData.layoutImage.color = Color.white;

            SetChisel(drillButtonData.drillPrefab);
        }
    }

    private void SetChisel(GameObject drillPrefab)
    {
        playSystem.ReplaceDrill(drillPrefab);
    }
}
