using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIprofessot : MonoBehaviour
{

    public enum Polygons {Cone, Cylinder};
	public int polygon;

    public float height;
	public float width;
    public int sides;
    
	static int sidesMinValue = 3;
	static int sidesMaxValue = 20;

	static float heightMinValue = 0.05;
	static float heightMaxValue = 0.3;

	static float widthMinValue = 0.05;
	static float widthMaxValue = 0.3;

	// Objetos para input
	private GameObject eightSlider;
	private GameObject widthSlider;
	private GameObject sideSlider;

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
        sideSlider.maxValue = sidestMaxValue;

        sideSlider.onValueChanged.AddListener(sideSliderUpdate);

		typeSelector = GameObject.Find("PolyDropdown").GetComponent<TMP_Dropdown>();
		typeSelector.value = polygon;

		typeSelector.onValueChanged.AddListener(delegate {typeUpdate(typeSelector);});

		//output objetos de output
		heightText = GameObject.Find("textHeight").GetComponent<TextMeshProUGUI>();
		tempVar = (int)(height*100)
		heightText.text = tempVar.toString() + " cm";

		widthText = GameObject.Find("textWidth").GetComponent<TextMeshProUGUI>();
		tempVar = (int)(width*100)
		widthText.text = tempVar.toString() + " cm";

		sideText = GameObject.Find("textSide").GetComponent<TextMeshProUGUI>();
		sideText.text = sides.toString();
    }
}
