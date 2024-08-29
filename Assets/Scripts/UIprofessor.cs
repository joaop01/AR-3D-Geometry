using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Extensions;
using Firebase.Database;

public class UIprofessor : MonoBehaviour
{
	public enum Polygons {Cubo, Ortoedro, Esfera, Cone, Cilindro, Piramide, Prisma};
	public int polygon;

    public double height;
	public double width;
    public int sides;
    
    public int sidesMinValue = 3;
	public int sidesMaxValue = 20;

	public float heightMinValue;
	public float heightMaxValue;

	public float widthMinValue;
	public float widthMaxValue;

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

	    Debug.Log("uiProfessor iniciada");
        
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
		int tempVar = (int)(height * 100);
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
		Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		Firebase.Auth.FirebaseUser user = auth.CurrentUser;
		
		if (user != null) {
			string name = user.DisplayName;
			string email = user.Email;
			string uid = user.UserId;
			Debug.Log("Professor: " + name);
			DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

			reference.Child("chaves").Child("ar3d_palavra_chave").Child("altura").SetValueAsync(height);
			reference.Child("chaves").Child("ar3d_palavra_chave").Child("largura").SetValueAsync(width);
			reference.Child("chaves").Child("ar3d_palavra_chave").Child("lado").SetValueAsync(sides);
			reference.Child("chaves").Child("ar3d_palavra_chave").Child("forma").SetValueAsync(polygon);
		}
		else
		{
			Debug.Log("User null");
		}

		Debug.Log(user);

	}
}
