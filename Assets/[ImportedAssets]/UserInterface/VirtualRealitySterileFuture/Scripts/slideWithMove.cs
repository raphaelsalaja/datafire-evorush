using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideWithMove : MonoBehaviour
{

    public float _min;
    public float _max;
    public bool _moveWithSight;
    public float _speed;
    private float _normalizedValue;
    private float _axisFloat;
    [SerializeField] private float _lerpValue;
    public AxisType _axis;
    private RectTransform _thisTransform;
    void Start()
    {
        _thisTransform = GetComponent<RectTransform>();
        _normalizedValue = Mathf.Abs(_max - _min);
        _lerpValue = _thisTransform.localPosition.x;

    }

    void Update()
    {
        if (_moveWithSight)
        {
            float _ratio = _normalizedValue / 360;
            if (_axis == AxisType.X)
            {
                _axisFloat = Camera.main.transform.eulerAngles.y;
            }
            if (_axis == AxisType.Y)
            {
                _axisFloat = Camera.main.transform.eulerAngles.x;

            }
            float _value = Mathf.Clamp(Mathf.DeltaAngle(0, _axisFloat) * _ratio, _min, _max);

            _lerpValue = Mathf.MoveTowards(_lerpValue, _value, _speed * Time.deltaTime);


            _thisTransform.localPosition = new Vector3(_lerpValue, 0, 0);


        }
    }

    public void SlideSprite(float f){

                    _thisTransform.localPosition = new Vector3(f, 0, 0);

    }
}
