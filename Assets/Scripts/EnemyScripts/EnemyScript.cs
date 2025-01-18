using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private Transform PlayerCharacter;
    public float Speed = 5;
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

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(PlayerCharacter.transform.position.x, PlayerCharacter.transform.position.y), Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
