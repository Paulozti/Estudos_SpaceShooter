﻿using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Player move speed.")]
    private float _MoveSpeed = 30;
    [SerializeField]
    [Tooltip("Select laser prefab.")]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _LaserContainer;
    [SerializeField]
    private GameObject _TripleLaserPrefab;
    [Tooltip("Distance the laser is created from the player.")]
    private float _laserPositionOffset = 1f;
    [SerializeField]
    [Tooltip("Faster when closer to zero.")]
    private float _FireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject _ShieldGameObject;
    private bool _isTripleShotActive = false, _isSpeedActive = false, _isShieldActive = false;
    [SerializeField]
    private float _TripleShotCooldown = 5, _SpeedCooldown = 5;
    [SerializeField]
    private float _SpeedPowerUpMultiplier= 2;
    [SerializeField]
    private AudioSource _deathAudioSource;

    [SerializeField]
    private GameObject _rightDamage;
    [SerializeField]
    private GameObject _leftDamage;
    private int _score = 0;

    private SpawnManager _spawner;
    private UImanager _uiManager;

    [SerializeField]
    private AudioClip _shootLaserSound;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        transform.position = new Vector3(0, 0, 0);
        _spawner = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawner == null)
        {
            Debug.LogError("Spawn manager is NULL.");
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UImanager>();
        
        if(_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL.");
        }

        if(_audioSource == null)
        {
            Debug.LogError("Audio Source on player is NULL.");
        }
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
        if (_isSpeedActive)
        {
            transform.Translate(direction * _MoveSpeed * _SpeedPowerUpMultiplier * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _MoveSpeed * Time.deltaTime);
        }
        

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
        GameObject newLaser;
        if (_isTripleShotActive)
        {
            newLaser = Instantiate(_TripleLaserPrefab, laserSpawnPosition, Quaternion.identity);
        }
        else
        {
            newLaser = Instantiate(_laserPrefab, laserSpawnPosition, Quaternion.identity);
        }
        if (newLaser.transform.parent != null)
        {
            newLaser.transform.parent = _LaserContainer.transform;
        }
        _audioSource.clip = _shootLaserSound;
        _audioSource.Play();
     }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _ShieldGameObject.SetActive(false);
        }
        else
        {
            _lives--;
            _uiManager.UpdateLives(_lives);
            if(_lives == 2)
            {
                _rightDamage.SetActive(true);
            }
            else if(_lives == 1)
            {
                _leftDamage.SetActive(true);
            }
            else if (_lives < 1)
            {
                _spawner.onPlayerDeath();
                _deathAudioSource.Play();
                Destroy(this.gameObject);
            }
        }
        
    }

    public void ActivatePowerUp(int ID)
    {
        if(ID == 0)
        {
            _isTripleShotActive = true;
        }
        else if(ID == 1)
        {
            _isSpeedActive = true;
        }
        else if(ID == 2)
        {
            _isShieldActive = true;
            _ShieldGameObject.SetActive(true);
        }
        StartCoroutine(PowerUpCooldown(ID));
    }

   
    IEnumerator PowerUpCooldown(int ID)
    {
        if(ID == 0)
        {
            yield return new WaitForSeconds(_TripleShotCooldown);
            _isTripleShotActive = false;
        }
        else if(ID == 1)
        {
            yield return new WaitForSeconds(_SpeedCooldown);
            _isSpeedActive = false;
        }
    }

    public void UpdateScore(int points)
    {
        _score += points;
        _uiManager.GetComponent<UImanager>().UpdateScore(_score);
    }
}

