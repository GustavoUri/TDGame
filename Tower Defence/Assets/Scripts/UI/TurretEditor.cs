using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretEditor : MonoBehaviour
{
    private GameObject _turret;
    private Transform _cameraTransform;
    private Button _deleteTurretButton;
    private TurretShop _turretShop;
    void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _deleteTurretButton = GetComponentInChildren<Button>();
        _deleteTurretButton.onClick.AddListener(OnDeleteButtonClick);
    }

    private void OnDeleteButtonClick()
    {
        _turretShop.RemoveTurret(_turret);
        Destroy(gameObject);
    }

    public void SetTurret(GameObject turret)
    {
        _turret = turret;
    }
    
    public void SetTurretUI(GameObject turretUI)
    {
        _turretShop = turretUI.GetComponent<TurretShop>();
    }

// Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.forward, _cameraTransform.rotation * Vector3.up);
    }
}
