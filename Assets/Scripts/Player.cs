﻿using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Player move speed.")]
    private float _MoveSpeed = 30;
    [SerializeField]
    [Tooltip("Select laser prefab.")]
    private GameObject _laserPrefab;
    [SerializeField]
    [Tooltip("Distance the laser is created from the player.")]
    private float _laserPositionOffset = 0.8f;
    [SerializeField]
    [Tooltip("Faster when closer to zero.")]
    private float _FireRate = 0.15f;
    private float _canFire = -1f;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    
    void Update()
    {
        CalculateMoviment();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMoviment() 
    {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(axisX, axisY, 0);

        transform.Translate(direction * _MoveSpeed * Time.deltaTime);

        //Melhor maneira, utilizando Mathf.Clamp para setar um minimo e maximo valor.

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.3f, 8.3f),
                                            Mathf.Clamp(transform.position.y, -3.3f, 0f),
                                                0);

        //Maneira que pensei
        //if (transform.position.x <= -8.3f) transform.position = new Vector3(-8.3f, transform.position.y, 0);
        //if (transform.position.x >= 8.3f) transform.position = new Vector3(8.3f, transform.position.y, 0);
        //if (transform.position.y <= -3.3f) transform.position = new Vector3(transform.position.x, -3.3f, 0);
        //if (transform.position.y >= 5.4f) transform.position = new Vector3(transform.position.x, 5.4f, 0);
    }
    void FireLaser() 
    {
        // Time.time pega o tempo em que o game está rodando
            _canFire = Time.time + _FireRate;
            Vector3 laserSpawnPosition = transform.position + new Vector3(0, _laserPositionOffset, 0);
            Instantiate(_laserPrefab, laserSpawnPosition, Quaternion.identity);
     }
}

