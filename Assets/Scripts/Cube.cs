﻿using UnityEngine;

public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private CubeProperties _properties;

    public void SetProperties(CubeProperties props)
    {
        _properties = props;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Hit(Vector3 position, Vector3 direction, float magnitude)
    {
        // Apply torque
        _rigidbody.AddForceAtPosition(direction * magnitude, position, ForceMode.Impulse);
    }
}
