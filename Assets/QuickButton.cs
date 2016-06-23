using UnityEngine;
using System.Collections;

public class QuickButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void quickSet() {
		MathX.Y_LOC = gameObject.GetComponent<RectTransform>().transform.localPosition.y;
	}
}
