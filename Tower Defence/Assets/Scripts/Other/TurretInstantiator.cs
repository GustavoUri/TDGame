using Other;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretInstantiator : MonoBehaviour
{
    private GameObject _instantiationPrefab;

    private CameraMovement _camScript;
    public Vector3 place;
    public GameObject ordinaryTurret;
    public GameObject freezingTurret;
    public GameObject artilleryTurret;
    [SerializeField] private int mouseRaycastLayer;
    public GameObject sniperTurret;

    private GameObject _instantiationModel;
    // Start is called before the first frame update
    private RaycastHit _hit;
    public GameObject ordinaryTurretModel;
    public GameObject freezingTurretModel;
    public GameObject sniperTurretModel;
    public GameObject artilleryTurretModel;
    private GameObject _instantiatedModel;
    void Start()
    {
        mouseRaycastLayer = 1 << mouseRaycastLayer;
        _camScript = Camera.main.gameObject.GetComponent<CameraMovement>();
        _instantiationPrefab = ordinaryTurret;
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosition();

        if (_hit.collider.CompareTag("Terrain"))
        {
            _instantiatedModel.SetActive(true);
            _instantiatedModel.transform.position = _hit.point;
        }
        
        if (Input.GetMouseButtonUp(0) && _camScript.cameraState == CameraViewState.ShopView &&
            !EventSystem.current.IsPointerOverGameObject())
        {
            
            // if (!hit.transform.gameObject.CompareTag("Road"))
            // {
            //     Instantiate(_instantiationPrefab, place, new Quaternion());
            // }
        }
    }

    private void GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, mouseRaycastLayer))
        {
        }
    }

    public void SelectOrdinaryTurret()
    {
        _instantiationPrefab = ordinaryTurret;
        _instantiationModel = ordinaryTurretModel;
        _instantiatedModel = Instantiate(_instantiationModel, Vector3.zero, new Quaternion());
        _instantiatedModel.SetActive(false);
    }

    public void SelectSniperTurret()
    {
        _instantiationPrefab = sniperTurret;
        _instantiationModel = ordinaryTurretModel;
        Instantiate(_instantiationModel, Vector3.zero, new Quaternion());
    }

    public void SelectArtilleryTurret()
    {
        _instantiationPrefab = artilleryTurret;
        _instantiationModel = ordinaryTurretModel;
        Instantiate(_instantiationModel, Vector3.zero, new Quaternion());
    }

    public void SelectFreezingTurret()
    {
        _instantiationPrefab = freezingTurret;
        _instantiationModel = ordinaryTurretModel;
        Instantiate(_instantiationModel, Vector3.zero, new Quaternion());
    }
}