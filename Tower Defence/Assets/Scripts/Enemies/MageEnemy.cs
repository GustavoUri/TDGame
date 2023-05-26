using System.Collections;
using UnityEngine;
public class MageEnemy : BaseEnemy
{
    private float _distanceToTower;
    [SerializeField] private int hpStealedForSecond;
    // Start is called before the first frame update
    private void Start()
    {
        Tower = GameObject.FindWithTag("MainTower");
        TowerScript = Tower.GetComponent<MainTower>();
        FollowPosition = Tower.transform.position;
        var position = transform.position;
        EndPosition = new Vector3(position.x,position.y,position.z);
        StartCoroutine(StealHealth());
    }

    private IEnumerator StealHealth(){
        while (TowerScript != null)
        {
            StealedHealth +=hpStealedForSecond;
            TowerScript.GetDamage(Damage, gameObject.tag);
            yield return new WaitForSeconds(1);
        }
    }
}
