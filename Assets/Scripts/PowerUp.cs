using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _MoveSpeed = 3;
    private bool _TripleShotIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
                player.ActivateTripleShot();
                _TripleShotIsActive = true;
                Destroy(this.gameObject);
            }
            
        }
    }
}
