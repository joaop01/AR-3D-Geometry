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

    private Slider heightSlider;
    public float heightMinValue;
    public float heightMaxValue;

    private Slider sidesSlider;
    private int sidesMinValue = 3;
    private int sidesMaxValue = 20;

    private GameObject prefab;
    Button hide;
    Button buttonCustom;
    Button buttonTransform;
    private GameObject panel;
    private TMP_Dropdown drop;
	private float lastRotateX;
	private float lastRotateZ;

    TMP_Text textRotateX;
    TMP_Text textRotateZ;
    TMP_Text textHeight;
    TMP_Text textSides;

    void Start()
    {
        panel = GameObject.Find("XR Origin (AR Rig)").GetComponent<PlaceObject>().panel;

        prefab = GameObject.Find("XR Origin (AR Rig)").GetComponent<PlaceObject>().spawnedObject;

        buttonCustom = GameObject.Find("ButtonCustom").GetComponent<Button>();
        buttonCustom.onClick.AddListener(ButtonCustomUpdate);

        buttonTransform = GameObject.Find("ButtonTransform").GetComponent<Button>();
        buttonTransform.onClick.AddListener(ButtonTransformUpdate);
    }

    void PanelUpdate()
    {
        if (panel.gameObject.activeSelf)
        {
			if (panel.transform.GetChild(0).gameObject.activeSelf)
			{
				rotateXSlider = GameObject.Find("RotateXSlider").GetComponent<Slider>();
				rotateXSlider.minValue = rotMinValue;
				rotateXSlider.maxValue = rotMaxValue;

				rotateXSlider.onValueChanged.AddListener(RotateXSliderUpdate);

				rotateZSlider = GameObject.Find("RotateZSlider").GetComponent<Slider>();
				rotateZSlider.minValue = rotMinValue;
				rotateZSlider.maxValue = rotMaxValue;

				rotateZSlider.onValueChanged.AddListener(RotateZSliderUpdate);

				textRotateX = GameObject.Find("TextRotateXValue").GetComponent<TextMeshProUGUI>();
				textRotateX.text = (string)rotateXSlider.value.ToString()+" º";

				textRotateZ = GameObject.Find("TextRotateZValue").GetComponent<TextMeshProUGUI>();
				textRotateZ.text = (string)rotateZSlider.value.ToString()+" º";

			}

			if (panel.transform.GetChild(1).gameObject.activeSelf)
			{
				heightSlider = GameObject.Find("HeightSlider").GetComponent<Slider>();
				heightSlider.value = prefab.GetComponent<MeshMaker>().height;
				heightSlider.minValue = heightMinValue;
				heightSlider.maxValue = heightMaxValue;

				heightSlider.onValueChanged.AddListener(HeightSliderUpdate);

				sidesSlider = GameObject.Find("SidesSlider").GetComponent<Slider>();
				sidesSlider.value = prefab.GetComponent<MeshMaker>().nSides;
				sidesSlider.minValue = sidesMinValue;
				sidesSlider.maxValue = sidesMaxValue;

				sidesSlider.onValueChanged.AddListener(SidesSliderUpdate);

				textHeight = GameObject.Find("TextHeightValue").GetComponent<TextMeshProUGUI>();
				int intHeight = (int)(100*prefab.GetComponent<MeshMaker>().height);
				textHeight.text = (string)intHeight.ToString()+" cm";

				textSides = GameObject.Find("TextSidesValue").GetComponent<TextMeshProUGUI>();
				textSides.text = (string)prefab.GetComponent<MeshMaker>().nSides.ToString();

				drop = GameObject.Find("PolyDropdown").GetComponent<TMP_Dropdown>();
				drop.value = prefab.GetComponent<MeshMaker>().polygon;
				drop.onValueChanged.AddListener(delegate { DropdownUpdate(drop); });
			}

            hide = GameObject.Find("ButtonHide").GetComponent<Button>();
            hide.onClick.AddListener(HideUpdate);
        }
    }

    void RotateXSliderUpdate(float value)
    {
		float rotateValue = value - lastRotateX;
        Vector3 position = prefab.GetComponent<Renderer>().bounds.center;
        prefab.transform.RotateAround(position, new Vector3(1,0,0), rotateValue);
        lastRotateX = value;
		textRotateX.text = (string)value.ToString()+" º";
    }

    void RotateZSliderUpdate(float value)
    {
		float rotateValue = value - lastRotateZ;
        Vector3 position = prefab.GetComponent<Renderer>().bounds.center;
        prefab.transform.RotateAround(position, new Vector3(0,0,1), rotateValue);
        lastRotateZ = value;
		textRotateZ.text = (string)value.ToString()+" º";
    }

    void SidesSliderUpdate(float value)
    {
        prefab.GetComponent<MeshMaker>().nSides = (int) value;
        prefab.GetComponent<MeshMaker>().Start();
        textSides.text = prefab.GetComponent<MeshMaker>().nSides.ToString();
    }

    void HeightSliderUpdate(float value)
	{
        prefab.GetComponent<MeshMaker>().height = value;
        prefab.GetComponent<MeshMaker>().Start();
        int intHeight = (int)(100*prefab.GetComponent<MeshMaker>().height);
        textHeight.text = (string)intHeight.ToString()+" cm";
    }

    void HideUpdate()
    {
        panel.gameObject.SetActive(false);
        panel.transform.GetChild(0).gameObject.SetActive(false);
        panel.transform.GetChild(1).gameObject.SetActive(false);
        buttonCustom.gameObject.SetActive(true);
        buttonTransform.gameObject.SetActive(true);
    }

    void ButtonCustomUpdate()
    {
        panel.gameObject.SetActive(true);
        panel.transform.GetChild(1).gameObject.SetActive(true);
        PanelUpdate();
        buttonCustom.gameObject.SetActive(false);
        buttonTransform.gameObject.SetActive(false);
    }

    void ButtonTransformUpdate()
    {
        panel.gameObject.SetActive(true);
        panel.transform.GetChild(0).gameObject.SetActive(true);
        PanelUpdate();
        buttonCustom.gameObject.SetActive(false);
        buttonTransform.gameObject.SetActive(false);
    }

    void DropdownUpdate(TMP_Dropdown change)
    {
        prefab.GetComponent<MeshMaker>().polygon = change.value;
        prefab.GetComponent<MeshMaker>().Start();
    }
}
