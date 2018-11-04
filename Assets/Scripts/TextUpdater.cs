using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    [SerializeField] private AnimationCurve _bounceAnim;
    private Text _text;

    private float _t;
    private float _decayRate = 0.1f;
    private Coroutine _bounceCoroutine;

    private void Awake()
    {
        _text = GetComponent<Text>();
        _t = 0;
    }

    public string Text
    {
        get { return _text.text; }
    }

    public void SetText(string value, bool bounce = true)
    {
        _text.text = value;
        Bounce();
    }

    void Bounce()
    {
        if(_bounceCoroutine != null)
        {
            StopCoroutine(_bounceCoroutine);
        }

        _bounceCoroutine = StartCoroutine(BounceCoroutine(0.1f));
    }

    IEnumerator BounceCoroutine(float duration)
    {
        var timer = 0f;

        while(timer < duration)
        {
            var size = _bounceAnim.Evaluate(timer / duration);
            transform.localScale = Vector3.one * (1f + size);
            yield return null;
            timer += Time.deltaTime;
        }

        transform.localScale = Vector3.one;
    }

}
