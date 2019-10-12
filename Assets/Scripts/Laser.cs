using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _LaserSpeed = 8;
    void Update()
    {
        transform.Translate(Vector3.up * _LaserSpeed * Time.deltaTime);

        if (transform.position.y > 7.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
