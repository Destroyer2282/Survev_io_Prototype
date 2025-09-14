using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private float speed = 10f;
    private float range = 20f;
    private Vector2 direction;
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Двигаем пулю в направлении, куда она повернута
        transform.position += transform.right * speed * Time.deltaTime;

        // Проверяем дистанцию
        if (Vector2.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 newDirection, float newSpeed)
    {
        direction = newDirection;
        speed = newSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destructible"))
        {
            collision.GetComponent<BoxDestruction>()?.takeDamage();
            Destroy(gameObject);
        }
    }
}