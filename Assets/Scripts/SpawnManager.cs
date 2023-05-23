using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField] private GameObject[] powerups;
    public void StartSpawning()
    {
        StartCoroutine(EnemySpawnRoutine(5.0f));
        StartCoroutine(PowerupSpawnRoutine());
    }
    IEnumerator EnemySpawnRoutine(float waitingTime)
    {
        yield return new WaitForSeconds(3);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9, 8),7,0);
            GameObject newEnemy =  Instantiate(_enemyPrefab,posToSpawn,Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(waitingTime);

        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        yield return new WaitForSeconds(3);
        while (_stopSpawning==false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9, 8), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    private void RemoveAllTheRemainingEnemyOnTheScreen()
    {

    }
}
