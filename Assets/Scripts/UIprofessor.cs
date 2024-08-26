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

	public static float heightMinValue;
	public static float heightMaxValue;

	public static float widthMinValue;
	public static float widthMaxValue;

	// Objetos para input
	private Slider heightSlider;
	private Slider widthSlider;
	private Slider sideSlider;

	private TMP_Dropdown typeSelector;

	private Button sendButton;

	// objetos para output
	private TMP_Text heightText;
	private TMP_Text widthText;
	private TMP_Text sideText;

    // Start is called before the first frame update
    void Start()
    {
        
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

		sendButton = GameObject.Find("ButtonSend").GetComponent<Button>();
        sendButton.onClick.AddListener(sendData);

		//output objetos de output
		heightText = GameObject.Find("textHeight").GetComponent<TextMeshProUGUI>();

		widthText = GameObject.Find("textWidth").GetComponent<TextMeshProUGUI>();

		sideText = GameObject.Find("textSide").GetComponent<TextMeshProUGUI>();
        
    }

	void HeightSliderUpdate(float value){

		height = value;
		int tempVar = (int)(height*100)
		heightText.text = tempVar.ToString();

	}

	void widthSliderUpdate(float value){

		width = value;
		int tempVar = (int)(width*100);
		widthText.text = tempVar.ToString();

	}

	void sideSliderUpdate(float value){

		sides = (int)value;
		sideText.text = sides.ToString();

	}

	void typeUpdate(TMP_Dropdown change){

		polygon = change.value;
	
	}

	void sendData(){

	}
}
