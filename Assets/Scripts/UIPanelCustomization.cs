using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPanelCustomization : MonoBehaviour
{
    private Slider rotateXSlider;
    private Slider rotateZSlider;
    public float rotMinValue;
    public float rotMaxValue;

    private Slider sidesSlider;
    private int sidesMinValue = 3;
    private int sidesMaxValue = 20;

    private GameObject prefab;
    TMP_Text textSides;
    Button hide;
    Button show;
    private GameObject panel;
    private TMP_Dropdown drop;
	private float lastRotateX;
	private float lastRotateZ;

    void Start()
    {
        panel = GameObject.Find("XR Origin (AR Rig)").GetComponent<PlaceObject>().panel;

        prefab = GameObject.Find("XR Origin (AR Rig)").GetComponent<PlaceObject>().spawnedObject;

        show = GameObject.Find("ButtonShow").GetComponent<Button>();
        show.onClick.AddListener(ShowUpdate);
    }

    void PanelUpdate()
    {
        if (panel.gameObject.activeSelf)
        {
            rotateXSlider = GameObject.Find("RotateXSlider").GetComponent<Slider>();
            rotateXSlider.minValue = rotMinValue;
            rotateXSlider.maxValue = rotMaxValue;

            rotateXSlider.onValueChanged.AddListener(RotateXSliderUpdate);

            rotateZSlider = GameObject.Find("RotateZSlider").GetComponent<Slider>();
            rotateZSlider.minValue = rotMinValue;
            rotateZSlider.maxValue = rotMaxValue;

            rotateZSlider.onValueChanged.AddListener(RotateZSliderUpdate);

            sidesSlider = GameObject.Find("SidesSlider").GetComponent<Slider>();
            sidesSlider.value = prefab.GetComponent<MeshMaker>().nSides;
            sidesSlider.minValue = sidesMinValue;
            sidesSlider.maxValue = sidesMaxValue;

            sidesSlider.onValueChanged.AddListener(SidesSliderUpdate);

            textSides = GameObject.Find("TextSidesValue").GetComponent<TextMeshProUGUI>();
            textSides.text = (string)prefab.GetComponent<MeshMaker>().nSides.ToString();

            hide = GameObject.Find("ButtonHide").GetComponent<Button>();
            hide.onClick.AddListener(HideUpdate);

            //drop = GameObject.Find("PolyDropdown").GetComponent<TMP_Dropdown>();
            //drop.onValueChanged.AddListener(delegate { DropdownUpdate(drop); });
        }
    }

    void RotateXSliderUpdate(float value)
    {
		float rotateValue = value - lastRotateX;
        Vector3 position = prefab.GetComponent<Renderer>().bounds.center;
        prefab.transform.RotateAround(position, new Vector3(1,0,0), rotateValue);
        lastRotateX = value;
    }

    void RotateZSliderUpdate(float value)
    {
		float rotateValue = value - lastRotateZ;
        Vector3 position = prefab.GetComponent<Renderer>().bounds.center;
        prefab.transform.RotateAround(position, new Vector3(0,0,1), rotateValue);
        lastRotateZ = value;
    }

    void SidesSliderUpdate(float value)
    {
        prefab.GetComponent<MeshMaker>().nSides = (int) value;
        prefab.GetComponent<MeshMaker>().Start();
        textSides.text = prefab.GetComponent<MeshMaker>().nSides.ToString();
    }

    void HideUpdate()
    {
        panel.gameObject.SetActive(false);
        show.gameObject.SetActive(true);
    }

    void ShowUpdate()
    {
        panel.gameObject.SetActive(true);
        PanelUpdate();
        show.gameObject.SetActive(false);
    }

    void DropdownUpdate(TMP_Dropdown change)
    {
        //prefab.GetComponent<MeshMaker>().polygon = prefab.GetComponent<MeshMaker>().Polygons[change.value];
        //prefab.GetComponent<MeshMaker>().Start();
    }
}
