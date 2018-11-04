using UnityEngine;

[CreateAssetMenu(fileName = "CubePropery", menuName = "Cube/Property")]
public class CubeProperties : ScriptableObject
{
    public float size;

    #region Physical Properties
    public float angularDrag;
    public float mass;
    #endregion

    #region Visual Properties
    public Color emissionColor;
    public AnimationCurve feedbackScaleCurve;
    public AnimationCurve feedbackEmissionCurve;
    public Material material;
    public GameObject particles;
    #endregion

}
