using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {

	[Header("Input Fields")]
	public InputField inputName;
	[Header("Panels")]
	public GameObject movePanel;
	public GameObject namingPanel;
	[Header("Text Objects")]
	public Text nameText;


	// Use this for initialization
	void Start () {
		inputName.onEndEdit.AddListener (renameOutput);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void triggerNaming() {
		namingPanel.SetActive (!namingPanel.activeInHierarchy);
	}

	public void triggerColors() {
		movePanel.SetActive (!movePanel.activeInHierarchy);
	}

	void renameOutput(string name) {
		nameText.text = name;
		triggerNaming ();
	}
}
