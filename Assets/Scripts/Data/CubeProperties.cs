using UnityEngine;

[CreateAssetMenu(fileName = "CubePropery", menuName = "Cube/Property")]
public class CubeProperties : ScriptableObject
{
    public float Size;

    #region Physical Properties
    public float Mass;
    public float AngularDrag;
    #endregion

    #region Visual Properties
    public Material Material;
    public GameObject Particles;

    public AnimationCurve FeedbackScaleCurve;
    public AnimationCurve FeedbackEmissionCurve;
    public Color EmissionColor;
    #endregion

}
