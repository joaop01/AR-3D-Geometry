using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIprofessor : MonoBehaviour
{

    public enum Polygons {Cone, Cylinder};
	public int polygon;

    public double height;
	public double width;
    public int sides;
    
	private static int sidesMinValue = 3;
	private static int sidesMaxValue = 20;

	private static float heightMinValue;
	private static float heightMaxValue;

	private static float widthMinValue;
	private static float widthMaxValue;

	// Objetos para input
	private Slider heightSlider;
	private Slider widthSlider;
	private Slider sideSlider;

	private TMP_Dropdown typeSelector;

	// objetos para output
	private TMP_Text heightText;
	private TMP_Text widthText;
	private TMP_Text sideText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		int tempVar;
        
		// objetos de input
        heightSlider = GameObject.Find("SliderHeight").GetComponent<Slider>();
        heightSlider.minValue = heightMinValue;
        heightSlider.maxValue = heightMaxValue;

        heightSlider.onValueChanged.AddListener(HeightSliderUpdate);

        widthSlider = GameObject.Find("SliderWidth").GetComponent<Slider>();
        widthSlider.minValue = widthMinValue;
        widthSlider.maxValue = widthMaxValue;

        widthSlider.onValueChanged.AddListener(widthSliderUpdate);

        sideSlider = GameObject.Find("SliderSide").GetComponent<Slider>();
        sideSlider.minValue = sidesMinValue;
        sideSlider.maxValue = sidesMaxValue;

        sideSlider.onValueChanged.AddListener(sideSliderUpdate);

		typeSelector = GameObject.Find("PolyDropdown").GetComponent<TMP_Dropdown>();
		typeSelector.value = polygon;

		typeSelector.onValueChanged.AddListener(delegate {typeUpdate(typeSelector);});

		//output objetos de output
		heightText = GameObject.Find("textHeight").GetComponent<TextMeshProUGUI>();
		tempVar = (int)(height*100);
		heightText.text = tempVar.ToString();

		widthText = GameObject.Find("textWidth").GetComponent<TextMeshProUGUI>();
		tempVar = (int)(width*100);
		widthText.text = tempVar.ToString();

		sideText = GameObject.Find("textSide").GetComponent<TextMeshProUGUI>();
		sideText.text = sides.ToString();
    }

	void HeightSliderUpdate(float value){

	}

	void widthSliderUpdate(float value){

	}

	void sideSliderUpdate(int value){

	}

	void typeSelector(TMP_Dropdown change){
	
	}
}
