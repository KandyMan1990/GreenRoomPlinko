using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    int direction = 1;
    [SerializeField]
    float speed = 2f;

    void Update()
    {
        transform.position = new Vector3(transform.position.x + (direction * speed * Time.deltaTime), transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            direction *= -1;
    }
}