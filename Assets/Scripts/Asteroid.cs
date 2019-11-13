using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3;
    [SerializeField]
    private float _moveSpeed = 1;

    [SerializeField]
    private GameObject _explosion;
    private GameObject _createdExplosion;
    private bool _notDestroyed = true;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotateSpeed) * Time.deltaTime);
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            _createdExplosion = Instantiate(_explosion, transform.position, transform.rotation);
            _moveSpeed = 0;
            Destroy(collision.gameObject);
            StartCoroutine(WaitAndDestroy());
        }
    }

    IEnumerator WaitAndDestroy()
    {
        while (_notDestroyed)
        {
            yield return new WaitForSeconds(3f);
            Destroy(this.gameObject);
       }
    }
}
