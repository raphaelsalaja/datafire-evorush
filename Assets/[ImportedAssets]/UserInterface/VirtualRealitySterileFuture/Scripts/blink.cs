using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blink : MonoBehaviour
{

    public bool _blinking;
    private Image _image;
	private Color _color;
	public float _speed;

    // Use this for initialization
    void Start()
    {
        _image = GetComponent<Image>();
		_color = _image.color;
    }

    // Update is called once per frame
    void Update()
    {
		if(_blinking){
			float _blinkerAlpha = Mathf.PingPong(_speed * Time.time, 1);
			_color = new Color(_color.r, _color.g, _color.b, _blinkerAlpha);
			_image.color = _color;
		}
    }
}
