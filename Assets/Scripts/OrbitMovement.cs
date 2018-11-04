using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    private float _radius;
    private Vector2 _polarCoords; // x is the angle with xz plane, y is the rotation around y axis.
    private Vector3 _center;

    public void SetPosition(Vector2 center, Vector2 coords, float radius)
    {
        _center = center;
        _polarCoords = coords;
        _radius = radius;

        transform.position = CalculatePos();
        LookAtCenter();
    }

    public void UpdatePosition(Vector2 coordsDelta, float radiusDelta)
    {
        _polarCoords.x += coordsDelta.x;
        _polarCoords.y += coordsDelta.y;

        _radius += radiusDelta;

        transform.position = CalculatePos();
        LookAtCenter();
    }

    private Vector3 CalculatePos()
    {
        var p = Quaternion.AngleAxis(_polarCoords.x, Vector3.forward) * (Vector3.right * _radius);
        p = Quaternion.AngleAxis(_polarCoords.y, Vector3.up) * p;

        p.x += _center.x;
        p.y += _center.y;
        p.z += _center.z;

        return p;
    }

    private void LookAtCenter()
    {
        transform.rotation = Quaternion.LookRotation(_center - transform.position, Vector2.up);
    }
}
