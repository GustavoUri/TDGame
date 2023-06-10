using System;
using System.Collections;
using Other;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraTurnSpeed;
    private bool _isRotDone;
    [SerializeField] private GameObject shopUI;
    private float _zCameraMovementRatio;
    private float _xCameraMovementRatio;
    [SerializeField] private int raycastLayerForMap;
    [SerializeField] private int raycastLayerForMouse;
    private Vector3 _scopeLastPosition;
    private Vector3 _cameraRightUpPosition;
    private Vector3 _cameraRightDownPosition;
    private Vector3 _cameraRightDownUpCenterPosition;
    private Transform _mainTowerTransform;
    [SerializeField] private float mapZBound;
    [SerializeField] private float mapXBound;
    private Vector3 _shootingCameraPosition;
    private Quaternion _cameraShootingAngle;
    private Scope _scopeScript;
    public Vector3 ScopePosition { get; private set; }
    [SerializeField] private float maxCameraYForShopView;
    public CameraViewState cameraState { get; private set; }
    private Vector3 _cameraShopPosition;

    private void Start()
    {
        _scopeScript = GetComponent<Scope>();
        _mainTowerTransform = GameObject.FindWithTag("MainTower").transform;
        raycastLayerForMap = 1 << raycastLayerForMap;
        raycastLayerForMouse = 1 << raycastLayerForMouse;
        cameraState = CameraViewState.ShootingView;
        GetCameraRightDownPosition();
        GetCameraRightUpPosition();
        GetCameraRightDownUpCenter();
        GetCameraShootingPosition();
        transform.position = _shootingCameraPosition;
        GetCameraRightDownPosition();
        GetCameraRightUpPosition();
        GetRatios();
        GetCameraShopPosition();
        _cameraShootingAngle = transform.rotation;
        StartCoroutine(SetShootingView());
    }

    private void GetCameraShopPosition()
    {
        _cameraShopPosition = new Vector3(_mainTowerTransform.position.x, maxCameraYForShopView,
            _mainTowerTransform.position.z); 
    }

    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            if (_isRotDone)
            {
                cameraState = cameraState == CameraViewState.ShootingView
                    ? CameraViewState.ShopView
                    : CameraViewState.ShootingView;
            }
            
            switch (cameraState)
            {
                case CameraViewState.ShootingView:
                    StartCoroutine(SetShootingView());
                    break;
                case CameraViewState.ShopView:
                    StartCoroutine(SetShopView());
                    break;
            }
            
        }

        GetScopePosition();

        switch (cameraState)
        {
            case CameraViewState.ShootingView:
                if (_isRotDone)
                    FollowTheScope();
                break;
            case CameraViewState.ShopView:
                break;
        }
    }


    IEnumerator SetShootingView()
    {
        _isRotDone = false;
        shopUI.SetActive(false);
        _scopeScript.enabled = true;
        _scopeScript.SetScopePosition(Screen.width / 2, Screen.height / 2);
        _scopeLastPosition = new Vector3(Screen.width / 2, Screen.height / 2);
        Cursor.visible = false;
        while (transform.rotation != _cameraShootingAngle)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _cameraShootingAngle, cameraTurnSpeed);
            transform.position = Vector3.Lerp(transform.position, _shootingCameraPosition, 0.1f);
            yield return null;
        }

        _isRotDone = true;
    }

    IEnumerator SetShopView()
    {
        _isRotDone = false;
        shopUI.SetActive(true);
        _scopeScript.enabled = false;
        Cursor.visible = true;
        while (transform.rotation != Quaternion.Euler(90, 0, 0))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, 0, 0), cameraTurnSpeed);
            transform.position = Vector3.Lerp(transform.position, _cameraShopPosition, 0.1f);
            yield return null;
        }

        _isRotDone = true;
    }

    void FollowTheScope()
    {
        var scopePos = new Vector3(_scopeScript.ScopePositionX, _scopeScript.ScopePositionY, 0);
        var scopePosDiff = scopePos - _scopeLastPosition;
        var cameraUpdatedPos = transform.position;
        cameraUpdatedPos.z -= scopePosDiff.y * _zCameraMovementRatio;
        cameraUpdatedPos.x += scopePosDiff.x * _xCameraMovementRatio;
        transform.position = cameraUpdatedPos;
        _scopeLastPosition = scopePos;
    }

    private void GetRatios()
    {
        var mapZBoundPos = new Vector3(_cameraRightUpPosition.x, _cameraRightUpPosition.y, mapZBound);
        var distFromCamZBoundToMap = Vector3.Distance(_cameraRightUpPosition, mapZBoundPos);
        _zCameraMovementRatio = distFromCamZBoundToMap / (Screen.height / 2);
        var mapXBoundPos = new Vector3(mapXBound, _cameraRightUpPosition.y, _cameraRightUpPosition.z);
        var distFromCamXBoundToMap = Vector3.Distance(_cameraRightUpPosition, mapXBoundPos);
        _xCameraMovementRatio = distFromCamXBoundToMap / (Screen.width / 2);
    }

    private void GetCameraShootingPosition()
    {
        var towerPos = _mainTowerTransform.position;
        var offset = _cameraRightDownUpCenterPosition - towerPos;
        var pos = transform.position;
        _shootingCameraPosition = new Vector3(towerPos.x, pos.y, pos.z - offset.z);
    }

    private void GetCameraRightUpPosition()
    {
        var cameraCenter = new Vector3(Screen.width, Screen.height, 0);
        var ray = Camera.main.ScreenPointToRay(cameraCenter);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, raycastLayerForMap))
        {
            _cameraRightUpPosition = hit.point;
        }
    }

    private void GetCameraRightDownPosition()
    {
        var cameraCenter = new Vector3(Screen.width, 0, 0);
        var ray = Camera.main.ScreenPointToRay(cameraCenter);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, raycastLayerForMap))
        {
            _cameraRightDownPosition = hit.point;
        }
    }

    private void GetCameraRightDownUpCenter()
    {
        _cameraRightDownUpCenterPosition = new Vector3((_cameraRightDownPosition.x + _cameraRightUpPosition.x) / 2,
            (_cameraRightDownPosition.y + _cameraRightUpPosition.y) / 2,
            (_cameraRightDownPosition.z + _cameraRightUpPosition.z) / 2);
    }

    private void GetScopePosition()
    {
        var scopePosOnScreen = new Vector3(_scopeScript.ScopePositionX, Screen.height - _scopeScript.ScopePositionY, 0);
        var ray = Camera.main.ScreenPointToRay(scopePosOnScreen);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, raycastLayerForMouse))
        {
            ScopePosition = hit.point;
        }
    }
}