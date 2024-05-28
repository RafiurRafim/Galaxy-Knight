using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemycontainer;
    [SerializeField]
    private bool _stopspawn = false;
    [SerializeField]
    private GameObject [] powerupsList;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }


    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    { while (_stopspawn==false)
        {

            Vector3 spawnposition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnposition, Quaternion.identity);
            newEnemy.transform.parent = _enemycontainer.transform;
            yield return new WaitForSeconds(3f);

        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while(_stopspawn == false)
        {
            Vector3 powerUpSpawnPositon = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newPowerup = Instantiate(powerupsList[Random.Range(0,3)], powerUpSpawnPositon, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(6, 10));
        }
    }

    public void OnPlayerDeath()
    {
        _stopspawn = true;
    }
}
