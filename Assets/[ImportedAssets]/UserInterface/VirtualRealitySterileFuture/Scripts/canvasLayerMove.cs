using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasLayerMove : MonoBehaviour
{

    public Transform _trackedObject;
    public bool _track;
    public float _rotateSpeed;

    [HideInInspector] public float _deltaX;
    [HideInInspector] public float _deltaY;


    // Use this for initialization
    void Start()
    {
        if (_trackedObject == null)
        {
            _trackedObject = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _deltaX = Mathf.Abs(Mathf.DeltaAngle(this.transform.eulerAngles.x, _trackedObject.eulerAngles.x));
        _deltaY = Mathf.Abs(Mathf.DeltaAngle(this.transform.eulerAngles.y, _trackedObject.eulerAngles.y));

        if (_track)
        {

            if (_rotateSpeed > 0)
            {

                // this.transform.rotation = Quaternion.Lerp(transform.rotation, _trackedObject.rotation, _rotateSpeed * Time.smoothDeltaTime);
                this.transform.rotation = Quaternion.Lerp(transform.rotation, _trackedObject.rotation, Mathf.SmoothStep(0.0f, 1.0f, _rotateSpeed));
                this.transform.position = _trackedObject.position;
            }
            else
            {
                if (transform.parent != _trackedObject)
                {

                    this.transform.parent = _trackedObject;
                }
            }
        }

    }
}
