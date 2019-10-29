using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _MoveSpeed = 3;
    [SerializeField]
    private int _powerUpID;
    // PowerUp IDs
    // 0 = Triple Shot
    // 1 = Speed
    // 2 = Shield

    void Update()
    {
        transform.Translate(Vector3.down * _MoveSpeed * Time.deltaTime);    
        if(transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player == null)
            {
                Debug.LogError("Player script not found!");
            }
            else
            {
                player.ActivatePowerUp(_powerUpID);
                Destroy(this.gameObject);
            }
        }
    }
}
