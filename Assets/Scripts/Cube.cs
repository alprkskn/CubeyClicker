using System.Collections;
using UnityEngine;

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

    public void Hit(Vector3 position, Vector3 normal, Vector3 direction, float magnitude)
    {
        // Apply torque
        _rigidbody.AddForceAtPosition(direction * magnitude, position, ForceMode.Impulse);

        var particles = Instantiate(_properties.Particles);
        particles.transform.position = position;
        particles.transform.forward = normal;
        var particleSystem = particles.GetComponent<ParticleSystem>();

        particleSystem.Play();

        Destroy(particleSystem.gameObject, particleSystem.main.duration);
    }

    private IEnumerator HitFeedback()
    {

        yield return null;
    }
}
