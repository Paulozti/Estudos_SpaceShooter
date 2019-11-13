using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 6f;
    
    [SerializeField]
    private GameObject _explosion;
    private GameObject _createdExplosion;
    private bool _notDestroyed = true;
    private SpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("SpanwManager is NULL.");
        }
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotateSpeed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            _createdExplosion = Instantiate(_explosion, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            _spawnManager.StartSpawnming();
            Destroy(this.gameObject, 1f);
        }
    }
}
