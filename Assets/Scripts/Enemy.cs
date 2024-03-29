﻿using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _MoveSpeed = 5;
    private Player _player;
    private Animator _destroyAnimation;
    private bool _exploding = false;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null){
            Debug.LogError("Player component is NULL");
        }
        _destroyAnimation = GetComponent<Animator>();
        if (_destroyAnimation == null)
        {
            Debug.LogError("Animator component is NULL");
        }
    }
    void Update()
    {
        transform.Translate(Vector3.down * _MoveSpeed * Time.deltaTime);
        if (transform.position.y < -5.6f && !_exploding)
        {
            float newX = Random.Range(-5.5f, 8.5f);
            transform.position = new Vector3(newX, 8f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player") && !_exploding)
        {
            if (_player != null)
            {
                _player.Damage();
                _destroyAnimation.SetTrigger("OnEnemyDeath");
                _exploding = true;
            }
            StartCoroutine(OnDeath());
        }
        else if (other.tag == ("Laser"))
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.UpdateScore(10);
                _destroyAnimation.SetTrigger("OnEnemyDeath");
                _exploding = true;
            }
            StartCoroutine(OnDeath());
        }
    }

    IEnumerator OnDeath()
    {
        while (_exploding)
        {
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(3f);
            GetComponent<AudioSource>().Stop();
            Destroy(this.gameObject);
        }
        
    }
}
