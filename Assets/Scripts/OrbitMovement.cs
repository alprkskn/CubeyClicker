using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    private Vector3 center;
    private Vector2 polarCoords; // x is the angle with xz plane, y is the rotation around y axis.
    private float radius;

    public void SetPosition(Vector2 center, Vector2 coords, float radius)
    {
        this.center = center;
        this.polarCoords = coords;
        this.radius = radius;

        transform.position = CalculatePos();
        LookAtCenter();
    }

    public void UpdatePosition(Vector2 coordsDelta, float radiusDelta)
    {
        this.polarCoords.x += coordsDelta.x;
        this.polarCoords.y += coordsDelta.y;

        this.radius += radiusDelta;

        transform.position = CalculatePos();
        LookAtCenter();
    }

    private Vector3 CalculatePos()
    {
        var p = Quaternion.AngleAxis(this.polarCoords.x, Vector3.forward) * (Vector3.right * this.radius);
        p = Quaternion.AngleAxis(this.polarCoords.y, Vector3.up) * p;

        p.x += this.center.x;
        p.y += this.center.y;
        p.z += this.center.z;

        return p;
    }

    private void LookAtCenter()
    {
        transform.rotation = Quaternion.LookRotation(this.center - transform.position, Vector2.up);
    }
}
