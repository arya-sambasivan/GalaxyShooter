using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField] private GameObject _tripleshotPowerupPrefab;
    // Start is called before the first frame update
    void Start()
    {
       
        StartCoroutine(EnemySpawnRoutine(5.0f));
        StartCoroutine(PowerupSpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawnRoutine(float waitingTime)
    {
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9, 8),7,0);
            GameObject newEnemy =  Instantiate(_enemyPrefab,posToSpawn,Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(waitingTime);

        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while(_stopSpawning==false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9, 8), 7, 0);
            Instantiate(_tripleshotPowerupPrefab, posToSpawn, Quaternion.identity);
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
