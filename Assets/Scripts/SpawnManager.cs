using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPrefab;
    [SerializeField]
    private GameObject _PowerUp;
    [SerializeField]
    private GameObject _EnemyContainer;
    [SerializeField]
    private GameObject _PowerUpContainer;
    [SerializeField]
    private float _SpawnTime = 5.0f;

    private bool _StopSpawning = false;
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
    {
        while (_StopSpawning == false)
        {
            Vector3 position = new Vector3(Random.Range(-5.5f, 8.5f), 7.5f, 0);
            GameObject newEnemy = Instantiate(_EnemyPrefab, position, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(_SpawnTime);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(5f,8f));
            Vector3 position = new Vector3(Random.Range(-5.5f, 8.5f), 7.5f, 0);
            GameObject newPowerUp = Instantiate(_PowerUp, position, Quaternion.identity);
            newPowerUp.transform.parent = _PowerUpContainer.transform;
        }
    }

    public void onPlayerDeath()
    {
        _StopSpawning = true;
    }
}
