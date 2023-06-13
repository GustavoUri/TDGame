using System.Collections.Generic;
using System.Linq;
using Other;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretInstantiator : MonoBehaviour
{
    private GameObject _instantiationPrefab;

    private CameraMovement _camScript;
    private Vector3 _mousePosition;
    [SerializeField] private GameObject ordinaryTurret;
    [SerializeField] private GameObject freezingTurret;
    [SerializeField] private GameObject artilleryTurret;
    [SerializeField] private GameObject sniperTurret;
    private GameObject _ordinaryTurretInstantiationModel;
    private GameObject _freezingTurretInstantiationModel;
    private GameObject _artilleryTurretInstantiationModel;
    private GameObject _sniperTurretInstantiationModel;
    private GameObject _currentInstantiationModel;
    [SerializeField] private GameObject turretModelCircle;
    private RaycastHit _hit;
    private GameObject _instantiatedModel;
    private Renderer[] _modelRenderers;
    [SerializeField] private Color instantiatedModelOkColor;
    [SerializeField] private Color instantiatedModelBadColor;
    private bool _isAbleToInstantiate;
    private List<GameObject> _obstructiveObjects;
    private GameObject _instantiatedCircle;
    private Renderer _circleRenderer;
    private BaseTurret _instantiatedModelScript;

    void Start()
    {
        _obstructiveObjects = FindObjectsOfType<GameObject>().Where(gameObject => !gameObject.CompareTag("Terrain"))
            .ToList();
        _camScript = Camera.main.gameObject.GetComponent<CameraMovement>();
        InitializeInstantiationModels();
    }

    private void InitializeInstantiationModels()
    {
        _ordinaryTurretInstantiationModel = ordinaryTurret;
        _freezingTurretInstantiationModel = freezingTurret;
        _sniperTurretInstantiationModel = sniperTurret;
        _artilleryTurretInstantiationModel = artilleryTurret;
    }

    // Update is called once per frame
    void Update()
    {
        if (_camScript.cameraState == CameraViewState.ShootingView)
        {
            Destroy(_instantiatedModel);
            Destroy(_instantiatedCircle);
            return;
        }

        GetMousePosition();

        if (_instantiatedModel == null && _currentInstantiationModel != null)
            InitializeModels();


        if (_instantiatedModel != null)
        {
            CheckPossibilityToInstantiate();
            _instantiatedModel.transform.position = _mousePosition;
            _instantiatedCircle.transform.position = _mousePosition;
            if (_isAbleToInstantiate)
            {
                foreach (var renderer in _modelRenderers)
                {
                    renderer.material.color = instantiatedModelOkColor;
                }

                _circleRenderer.material.color = instantiatedModelOkColor;
            }
            else
            {
                foreach (var renderer in _modelRenderers)
                {
                    renderer.material.color = instantiatedModelBadColor;
                }

                _circleRenderer.material.color = instantiatedModelBadColor;
            }
        }


        if (Input.GetMouseButtonUp(0) &&
            !EventSystem.current.IsPointerOverGameObject() && _isAbleToInstantiate)
        {
            var turret = Instantiate(_instantiationPrefab, _mousePosition, new Quaternion());
            _obstructiveObjects.Add(turret);
        }
    }

    private void InitializeModels()
    {
        _instantiatedModel = Instantiate(_currentInstantiationModel, _mousePosition, new Quaternion());
        _instantiatedModelScript = _instantiatedModel.GetComponent<BaseTurret>();
        _instantiatedModelScript.enabled = false;
        _modelRenderers = _instantiatedModel.GetComponentsInChildren<Renderer>();
        _instantiatedCircle = Instantiate(turretModelCircle, _mousePosition, new Quaternion());
        _instantiatedCircle.transform.localScale = new Vector3(5, 5, 5);
        _instantiatedCircle.transform.rotation = Quaternion.Euler(90, 0, 0);
        _circleRenderer = _instantiatedCircle.GetComponent<Renderer>();
    }

    private void CheckPossibilityToInstantiate()
    {
        var isEnoughSpace = !_obstructiveObjects.Any(gameObject =>
            Vector3.Distance(gameObject.transform.position, _instantiatedModel.transform.position) <= 5);

        if (isEnoughSpace && _hit.collider.CompareTag("Terrain"))
            _isAbleToInstantiate = true;
        else
            _isAbleToInstantiate = false;
    }

    private void GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit))
        {
            _mousePosition = _hit.point;
        }
    }

    public void SelectOrdinaryTurret()
    {
        _instantiationPrefab = ordinaryTurret;
        _currentInstantiationModel = _ordinaryTurretInstantiationModel;
        Destroy(_instantiatedModel);
        Destroy(_instantiatedCircle);
    }

    public void SelectSniperTurret()
    {
        _instantiationPrefab = sniperTurret;
        _currentInstantiationModel = _sniperTurretInstantiationModel;
        Destroy(_instantiatedModel);
        Destroy(_instantiatedCircle);
    }

    public void SelectArtilleryTurret()
    {
        _instantiationPrefab = artilleryTurret;
        _currentInstantiationModel = _artilleryTurretInstantiationModel;
        Destroy(_instantiatedModel);
        Destroy(_instantiatedCircle);
    }

    public void SelectFreezingTurret()
    {
        _instantiationPrefab = freezingTurret;
        _currentInstantiationModel = _freezingTurretInstantiationModel;
        Destroy(_instantiatedModel);
        Destroy(_instantiatedCircle);
    }
}