using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MathX : MonoBehaviour {

	public static float Y_LOC;

	public Vector2 pixelRes;

	public GameObject defaultMove;

	public GameObject parentImage;
	public GameObject panel;
	public GameObject fullPanel;

	public Text textureName;
	public Toggle flexToggle;

	Texture2D theTexture;
	
	List<int> rMoves;
	List<int> gMoves;
	List<int> bMoves;

	int currentColor;

	// Use this for initialization
	void Start () {
		
		rMoves = new List<int> ();
		gMoves = new List<int> ();
		bMoves = new List<int> ();
		
		theTexture = new Texture2D ((int)pixelRes.x, (int)pixelRes.y, TextureFormat.ARGB32, false);

	}
	
	// Update is called once per frame
	void Update () {
	}
	
	float applyMoves(int[] moves, int x, int y) {
		
		float colorValue = 1;
		
		for (int i = 0; i < moves.Length; i++) {
			switch(moves[i]) {
			case(0): //Addition X
			default:
				colorValue += x;
				break;
			case(1): //Addition Y
				colorValue += y;
				break;
			case(2)://Subtraction X
				colorValue -= x;
				break;
			case(3)://Subtraction Y
				colorValue -= y;
				break;
			case(4)://Multiplcation X
				colorValue *= x;
				break;
			case(5)://Multiplcation Y
				colorValue *= y;
				break;
			case(6)://Division X
				if(colorValue == 0)
					colorValue ++;
				colorValue /= x;
				break;
			case(7)://Multiplcation Y
				if(colorValue == 0)
					colorValue ++;
				colorValue /= y;
				break;
			case(8)://Modulus X
				if(x == 0)
					colorValue = 0;
				else 
					colorValue = Mathf.Abs(colorValue) % x;
				break;
			case(9)://Modulus Y
				if(y == 0)
					colorValue = 0;
				else 
					colorValue = Mathf.Abs(colorValue) % y;
				break;
			case(10)://Power X
				colorValue = (int)Mathf.Pow(colorValue, x);
				break;
			case(11)://Power Y
				colorValue = (int)Mathf.Pow (colorValue, y);
				break;
			case(12)://Pi
				colorValue *= Mathf.PI;
				break;
			case(13)://Pi FLEX
				colorValue *= Mathf.Pow(Mathf.PI, 2);
				break;
			case(14):// Square Root
				colorValue = Mathf.Sqrt(colorValue);
				break;
			case(15):// Square Root FLEX
				colorValue = Mathf.Sqrt(colorValue * x * y);
				break;
			case(16):
				colorValue = Mathf.Sin(colorValue);
				break;
			case(17):
				colorValue = Mathf.Asin(colorValue);
				break;
			case(18):
				colorValue = Mathf.Cos (colorValue);
				break;
			case(19):
				colorValue = Mathf.Acos(colorValue);
				break;
			case(20):
				colorValue = Mathf.Tan(colorValue);
				break;
			case(21):
				colorValue = Mathf.Atan(colorValue);
				break;
			case(22):
				colorValue = Mathf.Log(colorValue);
				break;
			case(23):
				colorValue = Mathf.Exp(colorValue);
				break;
			}
		}
		
		colorValue %= 256;
		
		float finalValue = (float)colorValue / 256;
		
		return finalValue;
	}

	public void saveImage() {
		byte[] bytes = theTexture.EncodeToPNG();
#if UNITY_STANDALONE
		if (!Directory.Exists(Application.dataPath + "/output/")) {
			Directory.CreateDirectory(Application.dataPath + "/output/");
		}
		File.WriteAllBytes(Application.dataPath + "/output/" + textureName.text + ".png", bytes);
#elif UNITY_ANDROID
		File.WriteAllBytes("/sdcard/DCIM/" + textureName.text + ".png", bytes);
#endif
	}

	public void renderImage() {
		for (int i = 0; i < pixelRes.x; i++) {
			for(int id = 0; id < pixelRes.y; id++) {
				theTexture.SetPixel(i, id, new Color(applyMoves(rMoves.ToArray(), i, id), 
				                                     applyMoves(gMoves.ToArray(), i, id), 
				                                     applyMoves(bMoves.ToArray(), i, id)));
			}
		}
		
		theTexture.Apply ();
		
		gameObject.GetComponent<Image> ().sprite = Sprite.Create (theTexture, 
		                                                          new Rect (0, 0, pixelRes.x, pixelRes.y), 
		                                                          new Vector2 (0.5f, 0.5f));
	}

	public void setColor(int c) {
		currentColor = c;
	}

	public void addMove(int m) {

		if (flexToggle.isOn)
			m++;

		if (currentColor == 0)
			rMoves.Add (m);
		else if (currentColor == 1)
			gMoves.Add (m);
		else
			bMoves.Add (m);
	}

	string getMoveName(int i) {
		switch (i) {
		case(0): //Addition X
		default:
			return "Addition";
		case(1): //Addition Y
			return "Addition (FLEX)";
		case(2)://Subtraction X
			return "Subtraction";
		case(3)://Subtraction Y
			return "Subtraction (FLEX)";
		case(4)://Multiplcation X
			return "Multiplication";
		case(5)://Multiplcation Y
			return "Multiplaction (FLEX)";
		case(6)://Division X
			return "Division";
		case(7)://Multiplcation Y
			return "Division (FLEX)";
		case(8)://Modulus X
			return "Modulus";
		case(9)://Modulus Y
			return "Modulus (FLEX)";
		case(10)://Power X
			return "Power";
		case(11)://Power Y
			return "Power (FLEX)";
		case(12)://Pi
			return "Pi";
		case(13)://Pi FLEX
			return "Pi (FLEX)";
		case(14):// Square Root
			return "Square Root";
		case(15):// Square Root FLEX
			return "Square Root (FLEX)";
		case(16):
			return "Sine";
		case(17):
			return "Sine (FLEX)";
		case(18):
			return "Cosine";
		case(19):
			return "Cosine (FLEX)";
		case(20):
			return "Tangent";
		case(21):
			return "Tangent (FLEX)";
		case(22):
			return "Log";
		case(23):
			return "Log (FLEX)";
		}
		
	}
	
	public void renderMoveList(int a) {
		List<int> theList;
		if (a == 0)
			theList = rMoves;
		else if (a == 1)
			theList = gMoves;
		else
			theList = bMoves;

		for(int i = 0; i < theList.Count; i++) {
			GameObject g = Instantiate(defaultMove) as GameObject;
			g.transform.SetParent(parentImage.transform, false);
			Vector3 v = g.GetComponent<RectTransform>().transform.localPosition;
			v.y -= (60 * i);
			g.GetComponent<RectTransform>().transform.localPosition = v;

			g.transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + getMoveName(theList[i]);

			g.GetComponent<Button>().onClick.AddListener(whenClicked);
		}

		panel.SetActive(true);
		currentColor = a;
	}

	void whenClicked() {

		float y = MathX.Y_LOC;
		Debug.Log (y);
		y -= 221.5f;
		y /= -60;

		int m = (int)y;

		if (currentColor == 0)
			rMoves.RemoveAt (m);
		else if (currentColor == 1)
			gMoves.RemoveAt (m);
		else
			bMoves.RemoveAt (m);

		GameObject[] go = GameObject.FindGameObjectsWithTag("Temp Moves");

		foreach (GameObject g in go) {
			Destroy(g);
		}

		panel.SetActive(false);
	}
}
