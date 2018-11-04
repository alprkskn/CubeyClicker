using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action<Vector3> Click;
    public event Action<Vector3> Drag;
    public event Action<float> Zoom;

    private const string MouseWheelAxis = "Mouse ScrollWheel";
    private const float DragStartThreshold = 100;
    private const float ClickDistanceThresholdSqr = 100;
    private const float ZoomChangeThreshold = 0f;

    private bool _ptrDown;
    private bool _dragStarted;
    private Vector3 _ptrDownPos;
    private Vector3 _prevMousePos;
    private float _prevZoomValue;
    private DateTime _ptrDownTime;

    void Update()
    {
        var mousePos = Input.mousePosition;

        if (_ptrDown)
        {
            var delta = mousePos - _ptrDownPos;

            if (Input.GetMouseButtonUp(0))
            {
                if(delta.sqrMagnitude < ClickDistanceThresholdSqr)
                {
                    if (Click != null) Click(mousePos);
                }

                _ptrDown = false;
                _dragStarted = false;
            }
            else
            {
                if(!_dragStarted && delta.sqrMagnitude >= DragStartThreshold)
                {
                    _dragStarted = true;
                }

                if(_dragStarted)
                {
                    if (Drag != null) Drag(_prevMousePos - mousePos);
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ptrDown = true;
                _ptrDownPos = mousePos;
            }
        }

        var zoom = Input.GetAxis(MouseWheelAxis);

        if (Mathf.Abs(zoom) > 0)
        {
            if (Zoom != null) Zoom(zoom);
        }

        _prevZoomValue = zoom;
        _prevMousePos = mousePos;
    }
}
