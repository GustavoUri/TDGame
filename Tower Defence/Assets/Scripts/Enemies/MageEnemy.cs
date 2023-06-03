using System.Collections;
using UnityEngine;

public class MageEnemy : BaseEnemy
{
    [field: SerializeField] public int DamageWhileRunning { get; protected set; }
    [field: SerializeField] public float DamageRate { get; protected set; }
    // Start is called before the first frame update
    private void Start()
    {
        Tower = GameObject.FindWithTag("MainTower");
        TowerScript = Tower.GetComponent<MainTower>();
        FollowPosition = Tower.transform.position;
        var position = transform.position;
        EndPosition = new Vector3(position.x, position.y, position.z);
        StartCoroutine(StealHealth());
    }

    private IEnumerator StealHealth()
    {
        while (TowerScript != null)
        {
            TowerScript.GetDamage(DamageWhileRunning, gameObject.tag);
            yield return new WaitForSeconds(DamageRate);
        }
    }
}