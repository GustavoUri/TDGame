using UnityEngine;
using Unity.UI;
public class Instantiator : MonoBehaviour
{
    private GameObject _instantiationPrefab;

    private CameraMovement _camScript;
    public Vector3 place;

    public GameObject ordinaryTurret;
    public GameObject freezingTurret;
    public GameObject artilleryTurret;
    public GameObject sniperTurret;
    // Start is called before the first frame update
    void Start()
    {
        _camScript = Camera.main.gameObject.GetComponent<CameraMovement>();
        _instantiationPrefab = ordinaryTurret;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && _camScript.isOnAbove)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // if (_plane.Raycast(ray, out var distance))
            // {
            //     place = ray.GetPoint(distance);
            // }

            if (Physics.Raycast(ray, out var hit))
            {
                place = hit.point;
                place.y = hit.transform.position.y + 2;
            }

            if (!hit.transform.gameObject.CompareTag("Road"))
            {
                Instantiate(_instantiationPrefab, place, new Quaternion());
            }
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
}