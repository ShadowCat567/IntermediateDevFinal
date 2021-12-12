using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawning : MonoBehaviour
{
    int numEnemiesWave = 0;
    [SerializeField] int totalEnemies = 3;
    public GameObject enemy;
    public GameObject combatZone;

    public List<GameObject> enemyList = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if(combatZone.GetComponent<CombatZone>().combatZoneStart)
        {
            StartCoroutine(EnemyWave());
        }

        if(enemyList.Count == 0 && combatZone.GetComponent<CombatZone>().combatZoneEntered)
        {
            combatZone.GetComponent<CombatZone>().combatZoneStart = false;
            combatZone.GetComponent<CombatZone>().completedCombat = true;
        }

        enemyList.RemoveAll(enemy => enemy == null);
    }

    void SpawnEnemies()
    {
        enemyList.Add(Instantiate(enemy, transform.position, Quaternion.identity));
        numEnemiesWave += 1;
    }

    IEnumerator EnemyWave()
    {
        while(numEnemiesWave < totalEnemies)
        {
            SpawnEnemies();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
