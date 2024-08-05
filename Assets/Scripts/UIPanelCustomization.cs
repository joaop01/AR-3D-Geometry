using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPanelCustomization : MonoBehaviour
{
    private Slider rotateSlider;
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
            rotateSlider = GameObject.Find("RotateSlider").GetComponent<Slider>();
            rotateSlider.minValue = rotMinValue;
            rotateSlider.maxValue = rotMaxValue;

            rotateSlider.onValueChanged.AddListener(RotateSliderUpdate);

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

    void RotateSliderUpdate(float value)
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, value, transform.rotation.z);
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
