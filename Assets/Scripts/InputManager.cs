using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action<Vector3> Click;
    public event Action<Vector3> Drag;
    public event Action<float> Zoom;

    private const float ClickDistanceThresholdSqr = 100;
    private const float DragStartThreshold = 100;
    private const string MouseWheelAxis = "Mouse ScrollWheel";
    private const float ZoomChangeThreshold = 0f;

    private bool dragStarted;
    private Vector3 prevMousePos;
    private float prevZoomValue;
    private bool ptrDown;
    private Vector3 ptrDownPos;
    private DateTime ptrDownTime;

    void Update()
    {
        var mousePos = Input.mousePosition;

        if (this.ptrDown)
        {
            var delta = mousePos - this.ptrDownPos;

            if (Input.GetMouseButtonUp(0))
            {
                if(delta.sqrMagnitude < ClickDistanceThresholdSqr)
                {
                    if (Click != null) Click(mousePos);
                }

                this.ptrDown = false;
                this.dragStarted = false;
            }
            else
            {
                if(!this.dragStarted && delta.sqrMagnitude >= DragStartThreshold)
                {
                    this.dragStarted = true;
                }

                if(this.dragStarted)
                {
                    if (Drag != null) Drag(this.prevMousePos - mousePos);
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.ptrDown = true;
                this.ptrDownPos = mousePos;
            }
        }

        var zoom = Input.GetAxis(MouseWheelAxis);

        if (Mathf.Abs(zoom) > 0)
        {
            if (Zoom != null) Zoom(zoom);
        }

        this.prevZoomValue = zoom;
        this.prevMousePos = mousePos;
    }
}
