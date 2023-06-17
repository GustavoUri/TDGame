using System;
using System.Collections.Generic;
using System.Linq;
using Other;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretShop : MonoBehaviour
{
    private GameObject _instantiationPrefab;

    private CameraMovement _camScript;
    private Vector3 _mousePosition;
    [SerializeField] private GameObject ordinaryTurret;
    [SerializeField] private GameObject freezingTurret;
    [SerializeField] private GameObject artilleryTurret;
    [SerializeField] private GameObject sniperTurret;
    [SerializeField] private GameObject turretModelCircle;
    private RaycastHit _hit;
    private GameObject _instantiatedModel;
    private Renderer _modelRenderer;
    [SerializeField] private Color instantiatedModelOkColor;
    [SerializeField] private Color instantiatedModelBadColor;
    private List<GameObject> _obstructiveObjects;
    private GameObject _instantiatedCircle;
    private Renderer _circleRenderer;
    private BaseTurret _instantiatedModelScript;
    private List<Button> _turretButtons;
    [SerializeField] private int raycastLayerForInstantiating;
    [SerializeField] private int raycastLayerForEditing;
    private RaycastHit _turretHit;
    private Button _deleteButton;
    private bool _isAbleToDeleteTurrets;
    private KeyboardButtonsToShopButtons _keyboardButtons;
    private MainTower _towerScript;
    private int _healthDamageForMainTower;

    private void Awake()
    {
        raycastLayerForInstantiating = 1 << raycastLayerForInstantiating;
        raycastLayerForEditing = 1 << raycastLayerForEditing;
        _turretButtons = GetComponentsInChildren<Button>().ToList();
        _deleteButton = _turretButtons.Find(button => button.CompareTag("TurretDeleteButton"));
        _deleteButton.onClick.AddListener(TaskOnClickForDeleteButton);
        foreach (var button in _turretButtons.Where(button => !button.CompareTag("TurretDeleteButton")))
        {
            button.onClick.AddListener(delegate { TaskOnClick(button); });
        }
    }

    private void TaskOnClickForDeleteButton()
    {
        RemoveInstantiatedModel();
        var image = _deleteButton.GetComponent<Image>();
        _isAbleToDeleteTurrets = image.color == _deleteButton.colors.normalColor;
        SetButtonColor(_deleteButton, image);
    }

    private void TaskOnClick(Button button)
    {
        var image = button.GetComponent<Image>();
        RemoveInstantiatedModel();
        if (!(image.color == button.colors.pressedColor))
        {
            switch (button.tag)
            {
                case "OrdinaryTurretButton":
                    SelectOrdinaryTurret();
                    break;
                case "SniperTurretButton":
                    SelectSniperTurret();
                    break;
                case "ArtilleryTurretButton":
                    SelectArtilleryTurret();
                    break;
                case "FreezingTurretButton":
                    SelectFreezingTurret();
                    break;
            }

            InitializeInstantiatingModels();
        }

        SetButtonColor(button, image);
    }

    private void SetButtonColor(Button button, Image btnImage)
    {
        btnImage.color = btnImage.color != button.colors.pressedColor
            ? button.colors.pressedColor
            : button.colors.normalColor;
        foreach (var turretButton in _turretButtons.Where(x => !x.CompareTag(button.tag)))
        {
            var image = turretButton.GetComponent<Image>();
            image.color = turretButton.colors.normalColor;
        }
    }

    void Start()
    {
        _towerScript = GameObject.FindWithTag("MainTower").GetComponent<MainTower>();
        _obstructiveObjects = FindObjectsOfType<GameObject>().Where(gameObject => !gameObject.CompareTag("Terrain"))
            .ToList();
        _camScript = Camera.main.gameObject.GetComponent<CameraMovement>();
    }

    void RemoveInstantiatedModel()
    {
        _instantiationPrefab = null;
        Destroy(_instantiatedModel);
        Destroy(_instantiatedCircle);
    }

    private void CheckKeyboardNumbersPressed()
    {
        var arr = (int[]) Enum.GetValues(typeof(KeyboardButtonsToShopButtons));
        if (!Input.inputString.Any())
            return;
        var number = int.Parse(Input.inputString);
        if (arr.Contains(number))
        {
            var tag = Enum.GetName(typeof(KeyboardButtonsToShopButtons), number);
            switch (tag)
            {
                case "TurretDeleteButton":
                    TaskOnClickForDeleteButton();
                    break;
                default:
                    TaskOnClick(_turretButtons.Find(btn => btn.CompareTag(tag)));
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeyboardNumbersPressed();
        if (_isAbleToDeleteTurrets && CheckIfTurretOnPosition() && Input.GetMouseButtonUp(0))
        {
            _obstructiveObjects.Remove(_turretHit.collider.gameObject);
            Destroy(_turretHit.collider.gameObject);
        }

        if (_camScript.cameraState == CameraViewState.ShootingView)
        {
            RemoveInstantiatedModel();
            return;
        }

        GetMousePosition();


        if (_instantiatedModel != null)
        {
            MoveInstantiatedModel();
        }


        if (Input.GetMouseButtonUp(0) &&
            !EventSystem.current.IsPointerOverGameObject() &&
            _instantiationPrefab != null)
        {
            InstantiatePrefab();
        }
    }

    private void InstantiatePrefab()
    {
        if (_towerScript.Health - _healthDamageForMainTower < 0) return;
        var turret = Instantiate(_instantiationPrefab, _mousePosition, new Quaternion());
        _towerScript.GetDamage(_healthDamageForMainTower, turret.tag);
        _obstructiveObjects.Add(turret);
    }

    private void MoveInstantiatedModel()
    {
        _instantiatedModel.transform.position = _mousePosition;
        _instantiatedCircle.transform.position = _mousePosition;
        if (CheckPossibilityToInstantiate())
        {
            _modelRenderer.material.color = instantiatedModelOkColor;
            _circleRenderer.material.color = instantiatedModelOkColor;
        }
        else
        {
            _modelRenderer.material.color = instantiatedModelBadColor;
            _circleRenderer.material.color = instantiatedModelBadColor;
        }
    }


    private void InitializeInstantiatingModels()
    {
        _instantiatedModel = Instantiate(_instantiationPrefab, _mousePosition, new Quaternion());
        var modelScript = _instantiatedModel.GetComponent<BaseTurret>();
        _healthDamageForMainTower = modelScript.Price;
        modelScript.enabled = false;
        _modelRenderer = _instantiatedModel.GetComponent<Renderer>();
        _instantiatedCircle = Instantiate(turretModelCircle, _mousePosition, new Quaternion());
        _instantiatedCircle.transform.localScale = new Vector3(5, 5, 5);
        _instantiatedCircle.transform.rotation = Quaternion.Euler(90, 0, 0);
        _circleRenderer = _instantiatedCircle.GetComponent<Renderer>();
    }

    private bool CheckPossibilityToInstantiate()
    {
        var isEnoughSpace = !_obstructiveObjects.Any(gameObject =>
            Vector3.Distance(gameObject.transform.position, _instantiatedModel.transform.position) <= 5);
        return isEnoughSpace && _hit.collider.CompareTag("Terrain");
    }

    private bool CheckIfTurretOnPosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out _turretHit, Mathf.Infinity, raycastLayerForEditing);
        if (_turretHit.transform == null)
            return false;
        return _turretHit.transform.CompareTag("Turret") && _turretHit.transform.gameObject != _instantiatedModel;
    }

    private void GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, raycastLayerForInstantiating))
        {
            _mousePosition = _hit.point;
        }
    }

    public void SelectOrdinaryTurret()
    {
        _instantiationPrefab = ordinaryTurret;
    }

    public void SelectSniperTurret()
    {
        _instantiationPrefab = sniperTurret;
    }

    public void SelectArtilleryTurret()
    {
        _instantiationPrefab = artilleryTurret;
    }

    public void SelectFreezingTurret()
    {
        _instantiationPrefab = freezingTurret;
    }

    public void RemoveTurret(GameObject turret)
    {
        _obstructiveObjects.Remove(turret);
        Destroy(turret);
    }
}