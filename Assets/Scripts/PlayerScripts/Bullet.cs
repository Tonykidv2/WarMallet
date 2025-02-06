using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;
    public float BulletTimeToLive = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        BulletTimeToLive -= Time.deltaTime;
        if (BulletTimeToLive < 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
