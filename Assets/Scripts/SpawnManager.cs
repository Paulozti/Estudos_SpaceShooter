using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPrefab;
    [SerializeField]
    private GameObject _EnemyContainer;
    [SerializeField]
    private float _SpawnTime = 5.0f;

    private bool _StopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (_StopSpawning == false)
        {
            Vector3 position = new Vector3(Random.Range(-5.5f, 8.5f), 7.5f, 0);
            GameObject newEnemy = Instantiate(_EnemyPrefab, position, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(_SpawnTime);
        }
    }

    public void onPlayerDeath()
    {
        _StopSpawning = true;
    }
}
