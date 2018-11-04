using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    [SerializeField] private AnimationCurve bounceAnim;

    private Coroutine bounceCoroutine;
    private float decayRate = 0.1f;
    private float t; // Current tween state. Between 0.0 and 1.0
    private Text text;

    private void Awake()
    {
        this.text = GetComponent<Text>();
        this.t = 0;
    }

    public string Text
    {
        get { return this.text.text; }
    }

    public void SetText(string value, bool bounce = true)
    {
        this.text.text = value;
        Bounce();
    }

    void Bounce()
    {
        if(this.bounceCoroutine != null)
        {
            StopCoroutine(this.bounceCoroutine);
        }

        this.bounceCoroutine = StartCoroutine(BounceCoroutine(0.1f));
    }

    IEnumerator BounceCoroutine(float duration)
    {
        var timer = 0f;

        while(timer < duration)
        {
            var size = this.bounceAnim.Evaluate(timer / duration);
            transform.localScale = Vector3.one * (1f + size);
            yield return null;
            timer += Time.deltaTime;
        }

        transform.localScale = Vector3.one;
    }

}
