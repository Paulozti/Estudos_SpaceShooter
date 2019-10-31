using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _MoveSpeed = 5;
    private Player _player;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {
        transform.Translate(Vector3.down * _MoveSpeed * Time.deltaTime);
        if (transform.position.y < -5.6f)
        {
            float newX = Random.Range(-5.5f, 8.5f);
            transform.position = new Vector3(newX, 8f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            if (_player != null)
            {
                _player.Damage();
            }
            Destroy(this.gameObject);
        }
        else if (other.tag == ("Laser"))
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.UpdateScore(10);
            }
            Destroy(this.gameObject);
        }
    }
}
