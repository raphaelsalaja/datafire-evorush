using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum AxisType
{
    X,
    Y,
    Z,
}
public class rotateWithMove : MonoBehaviour
{
    public float _max = 360f;
    public float _min = 0f;
    public float _speed;
    [SerializeField]private float _normalizedValue;
    private float _lerpValue;
    public bool _moveWithSight;

    public AxisType _axis;

    private RectTransform _thisTransform;
    void Start()
    {
        _thisTransform = GetComponent<RectTransform>();
        _normalizedValue = Mathf.Abs(Mathf.DeltaAngle(_min, _max));
        _lerpValue = _thisTransform.eulerAngles.z;
        
    }

    void Update()
    {
        if (_moveWithSight)
        {
            float _ratio = 360 / _normalizedValue;
            // float _value = Mathf.Clamp(Mathf.DeltaAngle(0, Mathf.Abs(Camera.main.transform.eulerAngles.y)) / _ratio, _min, _max);
            float _value = Mathf.Clamp(Camera.main.transform.eulerAngles.y, _min, _max);
            _lerpValue = Mathf.MoveTowards(_lerpValue, _value, _speed * Time.deltaTime);
            _thisTransform.localRotation = Quaternion.Euler(0,0,_lerpValue/_ratio);
        }
    }

    public void RotateSprite(float f)
    {
        _thisTransform.localRotation = Quaternion.Euler(0,0,f);
    }
}
