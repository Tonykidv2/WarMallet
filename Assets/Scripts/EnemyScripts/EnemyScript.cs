using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private Transform PlayerCharacter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCharacter = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // get direction you want to point at
        Vector2 direction = ((Vector2)PlayerCharacter.position - (Vector2)transform.position).normalized;

        // set vector of transform directly
        transform.up = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
