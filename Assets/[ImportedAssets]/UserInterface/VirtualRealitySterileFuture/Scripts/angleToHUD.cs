using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class angleToHUD : MonoBehaviour {

private Text _text;
public AxisType _axis;
	// Use this for initialization
	void Start () {
		_text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_axis == AxisType.X){
			_text.text = ((int)Camera.main.transform.eulerAngles.y).ToString();
		}

		if(_axis == AxisType.Y){
			_text.text = ((int)Camera.main.transform.eulerAngles.x).ToString();
		}

	}
}
