using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Firebase;
using Firebase.Database;

public class UIPanelCustomization : MonoBehaviour
{
	//[Serializable]
	//public class Forma{
		//public float altura;
		//public int forma;
		//public int lado;
		//public float largura;
	//}
	private float lastRotationX;
	private float lastRotationZ;

    private Slider rotationXSlider;
    private Slider rotationYSlider;
    private Slider rotationZSlider;
	private Slider translationYSlider;
    public float rotMinValue;
    public float rotMaxValue;
    public float translationMinValue;
    public float translationMaxValue;
    private TMP_Text textRotationX;
    private TMP_Text textRotationY;
    private TMP_Text textRotationZ;
    private TMP_Text textTranslationY;

    private Slider sidesSlider;
    private Slider heightSlider;
    private Slider widthSlider;
    private Slider radiusSlider;
    private TMP_Dropdown drop;
    public int sidesMinValue;
    public int sidesMaxValue;
    public float heightMinValue;
    public float heightMaxValue;
    public float radiusMinValue;
    public float radiusMaxValue;
    private TMP_Text textSides;
    private TMP_Text textHeight;
    private TMP_Text textWidth;
    private TMP_Text textRadius;

    private Slider transparencySlider;
    private Slider rSlider;
	private Slider gSlider;
	private Slider bSlider;
    public float transparencyMinValue;
    public float transparencyMaxValue;
    public float rMinValue;
    public float rMaxValue;
    private TMP_Text textTransparency;
    private TMP_Text textR;
    private TMP_Text textG;
    private TMP_Text textB;

	private Toggle diagonalToggle;

    private Button hide;
    private Button buttonTransform;
    private Button buttonCustom;
	private Button buttonColor;
	private Button buttonLine;
	private Button buttonAtt;

    private GameObject prefab;
    private GameObject panel;
    public GameObject prefabLine;
    public GameObject spawnedLine;

	private Renderer rend;
	public Material opaque;
	public Material transparent;

    void Start()
    {
        panel = GameObject.Find("XR Origin (AR Rig)").GetComponent<PlaceObject>().panel;

        prefab = GameObject.Find("XR Origin (AR Rig)").GetComponent<PlaceObject>().spawnedObject;

		rend = prefab.GetComponent<Renderer>();

        buttonTransform = GameObject.Find("ButtonTransform").GetComponent<Button>();
        buttonTransform.onClick.AddListener(ButtonTransformUpdate);

        buttonCustom = GameObject.Find("ButtonCustom").GetComponent<Button>();
        buttonCustom.onClick.AddListener(ButtonCustomUpdate);

        buttonColor = GameObject.Find("ButtonColor").GetComponent<Button>();
        buttonColor.onClick.AddListener(ButtonColorUpdate);

        buttonLine = GameObject.Find("ButtonLine").GetComponent<Button>();
        buttonLine.onClick.AddListener(ButtonLineUpdate);
        
        buttonAtt = GameObject.Find("ButtonRefresh").GetComponent<Button>();
        buttonAtt.onClick.AddListener(ButtonRefreshUpdate);
    }

    void PanelUpdate()
    {
		int poly = prefab.GetComponent<MeshMaker>().polygon;

        if (panel.gameObject.activeSelf)
        {
			if (panel.transform.GetChild(0).gameObject.activeSelf)
			{
				rotationXSlider = GameObject.Find("RotationXSlider").GetComponent<Slider>();
				rotationXSlider.minValue = rotMinValue;
				rotationXSlider.maxValue = rotMaxValue;
				rotationXSlider.onValueChanged.AddListener(RotationXSliderUpdate);
				textRotationX = GameObject.Find("TextRotationXValue").GetComponent<TextMeshProUGUI>();
				textRotationX.text = (string)rotationXSlider.value.ToString()+" º";

				rotationYSlider = GameObject.Find("RotationYSlider").GetComponent<Slider>();
				rotationYSlider.minValue = rotMinValue;
				rotationYSlider.maxValue = rotMaxValue;
				rotationYSlider.onValueChanged.AddListener(RotationYSliderUpdate);
				textRotationY = GameObject.Find("TextRotationYValue").GetComponent<TextMeshProUGUI>();
				textRotationY.text = (string)rotationYSlider.value.ToString()+" º";

				rotationZSlider = GameObject.Find("RotationZSlider").GetComponent<Slider>();
				rotationZSlider.minValue = rotMinValue;
				rotationZSlider.maxValue = rotMaxValue;
				rotationZSlider.onValueChanged.AddListener(RotationZSliderUpdate);
				textRotationZ = GameObject.Find("TextRotationZValue").GetComponent<TextMeshProUGUI>();
				textRotationZ.text = (string)rotationZSlider.value.ToString()+" º";

				translationYSlider = GameObject.Find("TranslationYSlider").GetComponent<Slider>();
				translationYSlider.minValue = translationMinValue;
				translationYSlider.maxValue = translationMaxValue;
				translationYSlider.onValueChanged.AddListener(TranslationYSliderUpdate);
				textTranslationY = GameObject.Find("TextTranslationYValue").GetComponent<TextMeshProUGUI>();
				int intHeight = (int)(100*translationYSlider.value);
				textTranslationY.text = (string)intHeight.ToString("n2")+" cm";

			}

			if (panel.transform.GetChild(1).gameObject.activeSelf)
			{
				if(poly == 5 || poly == 6)
				{
					sidesSlider = GameObject.Find("SidesSlider").GetComponent<Slider>();
					sidesSlider.value = prefab.GetComponent<MeshMaker>().nSides;
					sidesSlider.minValue = sidesMinValue;
					sidesSlider.maxValue = sidesMaxValue;
					sidesSlider.onValueChanged.AddListener(SidesSliderUpdate);
					textSides = GameObject.Find("TextSidesValue").GetComponent<TextMeshProUGUI>();
					textSides.text = (string)prefab.GetComponent<MeshMaker>().nSides.ToString();
				}

				if(poly != 0 && poly != 2)
				{
					heightSlider = GameObject.Find("HeightSlider").GetComponent<Slider>();
					heightSlider.value = prefab.GetComponent<MeshMaker>().height;
					heightSlider.minValue = heightMinValue;
					heightSlider.maxValue = heightMaxValue;
					heightSlider.onValueChanged.AddListener(HeightSliderUpdate);
					textHeight = GameObject.Find("TextHeightValue").GetComponent<TextMeshProUGUI>();
					int intHeight = (int)(100*prefab.GetComponent<MeshMaker>().height);
					textHeight.text = (string)intHeight.ToString()+" cm";
				}

				radiusSlider = GameObject.Find("RadiusSlider").GetComponent<Slider>();
				radiusSlider.value = prefab.GetComponent<MeshMaker>().radius;
				radiusSlider.minValue = radiusMinValue;
				radiusSlider.maxValue = radiusMaxValue;
				radiusSlider.onValueChanged.AddListener(RadiusSliderUpdate);
				textRadius = GameObject.Find("TextRadiusValue").GetComponent<TextMeshProUGUI>();
				int intRadius = (int)(100*prefab.GetComponent<MeshMaker>().radius);
				textRadius.text = (string)intRadius.ToString()+" cm";

				drop = GameObject.Find("PolyDropdown").GetComponent<TMP_Dropdown>();
				drop.value = prefab.GetComponent<MeshMaker>().polygon;
				drop.onValueChanged.AddListener(delegate { DropdownUpdate(drop); });
			}

			if (panel.transform.GetChild(2).gameObject.activeSelf)
			{
				Color cor = rend.material.color;

				transparencySlider = GameObject.Find("TransparencySlider").GetComponent<Slider>();
				transparencySlider.value = cor.a;
				transparencySlider.minValue = transparencyMinValue;
				transparencySlider.maxValue = transparencyMaxValue;
				transparencySlider.onValueChanged.AddListener(TransparencySliderUpdate);
				textTransparency = GameObject.Find("TextTransparencyValue").GetComponent<TextMeshProUGUI>();
				textTransparency.text = (string)transparencySlider.value.ToString("n2");

				rSlider = GameObject.Find("RSlider").GetComponent<Slider>();
				rSlider.value = cor.r;
				rSlider.minValue = rMinValue;
				rSlider.maxValue = rMaxValue;
				rSlider.onValueChanged.AddListener(RSliderUpdate);
				textR = GameObject.Find("TextRValue").GetComponent<TextMeshProUGUI>();
				textR.text = (string)rSlider.value.ToString("n2");

				gSlider = GameObject.Find("GSlider").GetComponent<Slider>();
				gSlider.value = cor.g;
				gSlider.minValue = rMinValue;
				gSlider.maxValue = rMaxValue;
				gSlider.onValueChanged.AddListener(GSliderUpdate);
				textG = GameObject.Find("TextGValue").GetComponent<TextMeshProUGUI>();
				textG.text = (string)gSlider.value.ToString("n2");

				bSlider = GameObject.Find("BSlider").GetComponent<Slider>();
				bSlider.value = cor.b;
				bSlider.minValue = rMinValue;
				bSlider.maxValue = rMaxValue;
				bSlider.onValueChanged.AddListener(BSliderUpdate);
				textB = GameObject.Find("TextBValue").GetComponent<TextMeshProUGUI>();
				textB.text = (string)bSlider.value.ToString("n2");
			}

			if (panel.transform.GetChild(3).gameObject.activeSelf)
			{
				diagonalToggle = GameObject.Find("DiagonalToggle").GetComponent<Toggle>();
				diagonalToggle.onValueChanged.AddListener(DiagonalToggleUpdate);
			}

            hide = GameObject.Find("ButtonHide").GetComponent<Button>();
            hide.onClick.AddListener(HideUpdate);
        }
    }

    void RotationXSliderUpdate(float value)
    {
		float rotationValue = value - lastRotationX;
        Vector3 position = prefab.GetComponent<Renderer>().bounds.center;
        prefab.transform.RotateAround(position, new Vector3(1,0,0), rotationValue);
        lastRotationX = value;
		textRotationX.text = (string)value.ToString()+" º";
		if (diagonalToggle == null) DiagonalToggleUpdate(false);
		else DiagonalToggleUpdate(diagonalToggle.isOn);
    }

    void RotationYSliderUpdate(float value)
	{
		transform.localEulerAngles = new Vector3(transform.rotation.x, value, transform.rotation.z);
		textRotationY.text = (string)value.ToString()+" º";
		if (diagonalToggle == null) DiagonalToggleUpdate(false);
		else DiagonalToggleUpdate(diagonalToggle.isOn);
	}

    void RotationZSliderUpdate(float value)
    {
		float rotationValue = value - lastRotationZ;
        Vector3 position = prefab.GetComponent<Renderer>().bounds.center;
        prefab.transform.RotateAround(position, new Vector3(0,0,1), rotationValue);
        lastRotationZ = value;
		textRotationZ.text = (string)value.ToString()+" º";
		if (diagonalToggle == null) DiagonalToggleUpdate(false);
		else DiagonalToggleUpdate(diagonalToggle.isOn);
    }

	void TranslationYSliderUpdate(float value)
	{
        prefab.transform.position = new Vector3(prefab.transform.position.x, value, prefab.transform.position.z);
		int intHeight = (int)(100*value);
		textTranslationY.text = (string)intHeight.ToString("n2")+" cm";
		if (diagonalToggle == null) DiagonalToggleUpdate(false);
		else DiagonalToggleUpdate(diagonalToggle.isOn);
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

	void RadiusSliderUpdate(float value)
	{
		int poly = prefab.GetComponent<MeshMaker>().polygon;

		if(poly == 2)
		{
			prefab.transform.localScale = new Vector3(value, value, value);
		}

        prefab.GetComponent<MeshMaker>().radius = value;
        prefab.GetComponent<MeshMaker>().Start();
        int intRadius = (int)(100*prefab.GetComponent<MeshMaker>().radius);
        textRadius.text = (string)intRadius.ToString()+" cm";
	}

	void TransparencySliderUpdate(float value)
	{
		Color cor = rend.material.color;

		if(rend.material.color.a > 0.92f) rend.material = opaque;

		else rend.material = transparent;

		rend.material.color = new Color(cor.r, cor.g, cor.b, value);

		textTransparency.text = (string)transparencySlider.value.ToString("n2");
	}

	void RSliderUpdate(float value)
	{
		Color cor = rend.material.color;

		rend.material.color = new Color(value, cor.g, cor.b, cor.a);

		textR.text = (string)rSlider.value.ToString("n2");
	}

	void GSliderUpdate(float value)
	{
		Color cor = rend.material.color;

		rend.material.color = new Color(cor.r, value, cor.b, cor.a);

		textG.text = (string)gSlider.value.ToString("n2");
	}

	void BSliderUpdate(float value)
	{
		Color cor = rend.material.color;

		rend.material.color = new Color(cor.r, cor.g, value, cor.a);

		textB.text = (string)bSlider.value.ToString("n2");
	}

	void DiagonalToggleUpdate(bool value)
	{
		int poly = prefab.GetComponent<MeshMaker>().polygon;

		if(value && ((poly == 0 || poly == 1 || poly == 3 || poly == 5)))
		{
			if(spawnedLine == null) spawnedLine = Instantiate(prefabLine, prefab.transform.position, prefab.transform.rotation);
			else
			{
				spawnedLine.transform.position = prefab.transform.position;
				spawnedLine.transform.rotation = prefab.transform.rotation;
			}
		}

		else Destroy(spawnedLine);
	}

    void HideUpdate()
    {
        panel.gameObject.SetActive(false);
        panel.transform.GetChild(0).gameObject.SetActive(false);
        panel.transform.GetChild(1).gameObject.SetActive(false);
        panel.transform.GetChild(2).gameObject.SetActive(false);
        panel.transform.GetChild(3).gameObject.SetActive(false);
        buttonCustom.gameObject.SetActive(true);
        buttonTransform.gameObject.SetActive(true);
        buttonColor.gameObject.SetActive(true);
        buttonLine.gameObject.SetActive(true);
    }

    void ButtonTransformUpdate()
    {
        panel.gameObject.SetActive(true);
        panel.transform.GetChild(0).gameObject.SetActive(true);
        PanelUpdate();
        buttonTransform.gameObject.SetActive(false);
        buttonCustom.gameObject.SetActive(false);
        buttonColor.gameObject.SetActive(false);
        buttonLine.gameObject.SetActive(false);
    }

    void ButtonCustomUpdate()
    {
		int poly = prefab.GetComponent<MeshMaker>().polygon;

        panel.gameObject.SetActive(true);
        panel.transform.GetChild(1).gameObject.SetActive(true);

		GameObject custom = panel.transform.GetChild(1).gameObject;

		custom.transform.GetChild(0).gameObject.SetActive(true);
		custom.transform.GetChild(1).gameObject.SetActive(true);
		custom.transform.GetChild(2).gameObject.SetActive(true);

		GameObject.Find("TextRadius").GetComponent<TextMeshProUGUI>().text = "Base";

		if(poly != 5 && poly != 6)
		{
			custom.transform.GetChild(0).gameObject.SetActive(false);
		}

		if(poly == 0 || poly == 2)
		{
			custom.transform.GetChild(0).gameObject.SetActive(false);
			custom.transform.GetChild(1).gameObject.SetActive(false);
		}

		if(poly == 2)
		{
			GameObject.Find("TextRadius").GetComponent<TextMeshProUGUI>().text = "Raio";
		}

        PanelUpdate();
        buttonTransform.gameObject.SetActive(false);
        buttonCustom.gameObject.SetActive(false);
        buttonColor.gameObject.SetActive(false);
        buttonLine.gameObject.SetActive(false);
    }

	void ButtonColorUpdate()
	{
        panel.gameObject.SetActive(true);
        panel.transform.GetChild(2).gameObject.SetActive(true);
        PanelUpdate();
        buttonTransform.gameObject.SetActive(false);
        buttonCustom.gameObject.SetActive(false);
        buttonColor.gameObject.SetActive(false);
        buttonLine.gameObject.SetActive(false);
	}

	void ButtonLineUpdate()
	{
        panel.gameObject.SetActive(true);
        panel.transform.GetChild(3).gameObject.SetActive(true);
        PanelUpdate();
        buttonTransform.gameObject.SetActive(false);
        buttonCustom.gameObject.SetActive(false);
        buttonColor.gameObject.SetActive(false);
        buttonLine.gameObject.SetActive(false);
	}

    void DropdownUpdate(TMP_Dropdown change)
    {
        prefab.GetComponent<MeshMaker>().polygon = change.value;
        prefab.GetComponent<MeshMaker>().Start();

		int poly = prefab.GetComponent<MeshMaker>().polygon;

		Destroy(spawnedLine);
		if (diagonalToggle != null) diagonalToggle.isOn = false;

		if(poly == 2)
		{
			float radius = prefab.GetComponent<MeshMaker>().radius;
			prefab.transform.localScale = new Vector3(radius, radius, radius);
		}

		else prefab.transform.localScale = new Vector3(1f, 1f, 1f);

		ButtonCustomUpdate();
        PanelUpdate();
    }
    
    void ButtonRefreshUpdate()
    {
	    Debug.Log("Btn Refresh clicado");
	    DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
	    StartCoroutine(LoadDataEnum());
	    
    }
    
    IEnumerator LoadDataEnum()
    {
	    //Busca o valor na referencia. Substituir por 'chaves/'+palavra_chave_inserida_na_UI quando ter um campo para coletar
	    var data = FirebaseDatabase.DefaultInstance
		    .GetReference("chaves/ar3d_palavra_chave")
		    .GetValueAsync();

	    yield return new WaitUntil(predicate: () => data.IsCompleted);
	    DataSnapshot snapshot = data.Result;
	    Debug.Log(snapshot.Child("altura").Value.ToString());
		Debug.Log(snapshot.ToString());
	    
	    prefab.GetComponent<MeshMaker>().height = (float) Convert.ToDouble(snapshot.Child("altura").Value);
	    prefab.GetComponent<MeshMaker>().polygon = Convert.ToInt32(snapshot.Child("forma").Value);
		Debug.Log("forma: " + Convert.ToInt32(snapshot.Child("forma").Value));
	    prefab.GetComponent<MeshMaker>().nSides = Convert.ToInt32(snapshot.Child("lado").Value);
	    prefab.GetComponent<MeshMaker>().radius = (float) Convert.ToDouble(snapshot.Child("largura").Value);
	    prefab.GetComponent<MeshMaker>().Start();
    }
    
}
