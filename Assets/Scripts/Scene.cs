using UnityEngine;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    [SerializeField] private Transform _cubeCenter;
    [SerializeField] private CubeProperties _cubeProps;
    [SerializeField] private InputManager _input;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private OrbitMovement _cameraMovement;
    [SerializeField]private TextUpdater _counterText;

    private Cube _cube;


    private int _hitCount;
    private float _hitMagnitude;

    private void Awake()
    {
        _cube = CreateCube(_cubeProps);

        _input.Click += OnClick;
        _input.Drag += OnDrag;
        _input.Zoom += OnZoom;
    }

    private void Start()
    {
        _cameraMovement.SetPosition(Vector2.zero, new Vector2(45, 45), 6f);
        _counterText.SetText(_hitCount.ToString(), false);
    }

    private void OnDestroy()
    {
        _input.Click -= OnClick;
        _input.Drag -= OnDrag;
        _input.Zoom -= OnZoom;
    }

    private Cube CreateCube(CubeProperties properties)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        go.transform.localScale = Vector3.one * properties.Size;

        go.name = "The Cube";

        var mr = go.GetComponent<MeshRenderer>();
        mr.sharedMaterial = properties.Material;

        var rigidbody = go.AddComponent<Rigidbody>();

        rigidbody.mass = properties.Mass;
        rigidbody.angularDrag = properties.AngularDrag;
        rigidbody.constraints = RigidbodyConstraints.FreezePosition;

        var cube = go.AddComponent<Cube>();
        cube.SetProperties(properties);

        return cube;
    }

    private void OnZoom(float value)
    {
        _cameraMovement.UpdatePosition(Vector2.zero, value);
    }

    private void OnDrag(Vector3 dragVector)
    {
        if (Input.GetMouseButton(2))
        {
            _cameraMovement.UpdatePosition(new Vector2(dragVector.y, -dragVector.x), 0);
        }
    }

    private void OnClick(Vector3 clickPos)
    {
        Ray r = _mainCamera.ScreenPointToRay(clickPos);
        RaycastHit hit;

        if(Physics.Raycast(r, out hit))
        {
            var cube = hit.collider.gameObject.GetComponent<Cube>();

            if (cube)
            {
                cube.Hit(hit.point, hit.normal, r.direction, 10f);
                _hitCount++;
                _counterText.SetText(_hitCount.ToString());
            }
        }
    }

}
