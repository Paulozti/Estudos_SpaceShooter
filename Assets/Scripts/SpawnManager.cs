using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPrefab;
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private GameObject _EnemyContainer;
    [SerializeField]
    private GameObject _PowerUpContainer;
    [SerializeField]
    private float _EnemySpawnTime = 5.0f;
    [SerializeField]
    private float _powerUpSpawnTimeLow = 5f, _powerUpSpawnTimeHigh = 8f;

    private bool _StopSpawning = false;
    public void StartSpawnming()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_StopSpawning == false)
        {
            Vector3 position = new Vector3(Random.Range(-5.5f, 8.5f), 7.5f, 0);
            GameObject newEnemy = Instantiate(_EnemyPrefab, position, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(_EnemySpawnTime);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_StopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(_powerUpSpawnTimeLow,_powerUpSpawnTimeHigh));
            Vector3 position = new Vector3(Random.Range(-5.5f, 8.5f), 7.5f, 0);
            int randomPowerUp = Random.Range(0, _powerUps.Length);
            GameObject newPowerUp = Instantiate(_powerUps[randomPowerUp], position, Quaternion.identity);
            newPowerUp.transform.parent = _PowerUpContainer.transform;
        }
    }

    public void onPlayerDeath()
    {
        _StopSpawning = true;
    }
}
