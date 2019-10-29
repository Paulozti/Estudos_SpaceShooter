using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPrefab;
    [SerializeField]
    private GameObject _TripleShotPowerUp, _SpeedPowerUp,_ShieldPowerUp;
    [SerializeField]
    private GameObject _EnemyContainer;
    [SerializeField]
    private GameObject _PowerUpContainer;
    [SerializeField]
    private float _EnemySpawnTime = 5.0f;
    [SerializeField]
    private float _powerUpSpawnTimeLow = 5f, _powerUpSpawnTimeHigh = 8f;

    private bool _StopSpawning = false;
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
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
        while(_StopSpawning == false)
        {
            GameObject newPowerUp = null;
            yield return new WaitForSeconds(Random.Range(_powerUpSpawnTimeLow,_powerUpSpawnTimeHigh));
            Vector3 position = new Vector3(Random.Range(-5.5f, 8.5f), 7.5f, 0);
            int powerUp = Random.Range(0, 3);
            Debug.Log(powerUp);
            if (powerUp == 0)
            {
                 newPowerUp = Instantiate(_TripleShotPowerUp, position, Quaternion.identity);
            }
            else if(powerUp == 1)
            {
                newPowerUp = Instantiate(_SpeedPowerUp, position, Quaternion.identity);
            }
            else if(powerUp == 2)
            {
                newPowerUp = Instantiate(_ShieldPowerUp, position, Quaternion.identity);
            }
            if(newPowerUp != null)
            {
                newPowerUp.transform.parent = _PowerUpContainer.transform;
            }
            else
            {
                Debug.LogError("Random fail!");
            }
            
        }
    }

    public void onPlayerDeath()
    {
        _StopSpawning = true;
    }
}
