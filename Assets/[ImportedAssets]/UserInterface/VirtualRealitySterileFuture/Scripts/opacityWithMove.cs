using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class opacityWithMove : MonoBehaviour
{
    private canvasLayerMove _canvas;
    private Color _defaultColor;
    private SpriteRenderer _sprite;
    public float _speed;
    public bool _moveWithSight;
    [SerializeField] private float _opacity;
    public AxisType _axis;
    public float _opacityChangeRatio;
    // Use this for initialization
    void Start()
    {
        if (GetComponentInParent<canvasLayerMove>() != null)
        {
            _canvas = GetComponentInParent<canvasLayerMove>();
        }
        _sprite = GetComponent<SpriteRenderer>();
        _defaultColor = _sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        float t = _speed * Time.deltaTime;
        if (_moveWithSight)
        {

            if (_axis == AxisType.X)
            {
                if ((int)_canvas._deltaX > 0)
                {
                    _opacity = 1 / (int)_canvas._deltaX;

                }

            }
            if (_axis == AxisType.Y)
            {
                if (_opacity <= 1)
                {
                    float targetOpacity = _opacityChangeRatio * (1 / (_canvas._deltaY * 360));
                    // _opacity = _opacityChangeRatio * (1/(_canvas._deltaY*360));
                    _opacity = Mathf.Lerp(_opacity, targetOpacity, t);
                }
                else
                {
                    _opacity = 1;
                }


            }
            _sprite.color = new Color(_defaultColor.r, _defaultColor.g, _defaultColor.b, _opacity);
        }

    }

    public void OpacitySprite(float f)
    {
        _sprite.color = _sprite.color = new Color(_defaultColor.r, _defaultColor.g, _defaultColor.b, f);
    }
}
