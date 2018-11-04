using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Coroutine feedbackCoroutine;
    private Material material;
    private CubeProperties properties;
    private new Rigidbody rigidbody;

    public void SetProperties(CubeProperties props)
    {
        this.properties = props;
        this.material = this.properties.material;
    }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    public void Hit(Vector3 position, Vector3 normal, Vector3 direction, float magnitude)
    {
        // Apply torque
        this.rigidbody.AddForceAtPosition(direction * magnitude, position, ForceMode.Impulse);

        var particles = Instantiate(this.properties.particles);
        particles.transform.position = position;
        particles.transform.forward = normal;
        var particleSystem = particles.GetComponent<ParticleSystem>();

        particleSystem.Play();

        Destroy(particleSystem.gameObject, particleSystem.main.duration);

        if(this.feedbackCoroutine != null)
        {
            StopCoroutine(this.feedbackCoroutine);
        }

        this.feedbackCoroutine = StartCoroutine(HitFeedback(0.25f));
    }

    private IEnumerator HitFeedback(float duration)
    {
        var timer = 0f;
        this.material.EnableKeyword("_EMISSION");
        while(timer < duration)
        {
            var t = timer / duration;
            var emission = this.properties.feedbackEmissionCurve.Evaluate(t);
            var scale = this.properties.feedbackScaleCurve.Evaluate(t);

            transform.localScale = this.properties.size * (1 - scale) * Vector3.one;
            SetEmissionColor(emission);
            yield return null;
            timer += Time.deltaTime;
        }

        SetEmissionColor(0f);
        transform.localScale = this.properties.size * Vector3.one;
    }

    private void SetEmissionColor(float emission)
    {
        Color baseColor = this.properties.emissionColor;
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        this.material.SetColor("_EmissionColor", finalColor);
    }
}
