using UnityEngine;

public class Scene : MonoBehaviour
{
    [SerializeField] private Transform _cubeCenter;
    [SerializeField] private CubeProperties _cubeProps;
    [SerializeField] private InputManager _input;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private OrbitMovement _cameraMovement;

    private Cube _cube;

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

        go.name = "The Cube";

        var mr = go.GetComponent<MeshRenderer>();
        mr.sharedMaterial = properties.Material;

        var rigidbody = go.AddComponent<Rigidbody>();

        rigidbody.mass = properties.Mass;
        rigidbody.angularDrag = properties.AngularDrag;
        rigidbody.constraints = RigidbodyConstraints.FreezePosition;

        var cube = go.AddComponent<Cube>();

        return cube;
    }

    private void OnZoom(float value)
    {
        _cameraMovement.UpdatePosition(Vector2.zero, value);
    }

    private void OnDrag(Vector3 dragVector)
    {
        _cameraMovement.UpdatePosition(new Vector2(dragVector.y, -dragVector.x), 0);
        if (Input.GetMouseButton(2))
        {
        }
        else
        {
            //TODO: Rotate cube
        }
    }

    private void OnClick(Vector3 clickPos)
    {
        Ray r = _mainCamera.ScreenPointToRay(clickPos);
    }

}
