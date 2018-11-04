using UnityEngine;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    [SerializeField] private Transform cubeCenter;
    [SerializeField] private CubeProperties cubeProps;
    [SerializeField] private OrbitMovement cameraMovement;
    [SerializeField] private TextUpdater counterText;
    [SerializeField] private float hitMagnitude;
    [SerializeField] private InputManager input;
    [SerializeField] private Camera mainCamera;

    private Cube cube;
    private int hitCount;

    private void Awake()
    {
        this.cube = CreateCube(this.cubeProps);

        this.input.Click += OnClick;
        this.input.Drag += OnDrag;
        this.input.Zoom += OnZoom;
    }

    private void Start()
    {
        this.cameraMovement.SetPosition(Vector2.zero, new Vector2(45, 45), 6f);
        this.counterText.SetText(this.hitCount.ToString(), false);
    }

    private void OnDestroy()
    {
        this.input.Click -= OnClick;
        this.input.Drag -= OnDrag;
        this.input.Zoom -= OnZoom;
    }

    private Cube CreateCube(CubeProperties properties)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        go.transform.localScale = Vector3.one * properties.size;

        go.name = "The Cube";

        var mr = go.GetComponent<MeshRenderer>();
        mr.sharedMaterial = properties.material;

        var rigidbody = go.AddComponent<Rigidbody>();

        rigidbody.mass = properties.mass;
        rigidbody.angularDrag = properties.angularDrag;
        rigidbody.constraints = RigidbodyConstraints.FreezePosition;

        var cube = go.AddComponent<Cube>();
        cube.SetProperties(properties);

        return cube;
    }

    private void OnZoom(float value)
    {
        this.cameraMovement.UpdatePosition(Vector2.zero, value);
    }

    private void OnDrag(Vector3 dragVector)
    {
        if (Input.GetMouseButton(2))
        {
            this.cameraMovement.UpdatePosition(new Vector2(dragVector.y, -dragVector.x), 0);
        }
    }

    private void OnClick(Vector3 clickPos)
    {
        Ray r = this.mainCamera.ScreenPointToRay(clickPos);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit))
        {
            var cube = hit.collider.gameObject.GetComponent<Cube>();

            if (cube)
            {
                cube.Hit(hit.point, hit.normal, r.direction, this.hitMagnitude);
                this.hitCount++;
                this.counterText.SetText(this.hitCount.ToString());
            }
        }
    }

}
