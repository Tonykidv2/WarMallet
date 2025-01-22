using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public string EnemyName = string.Empty;

    private Transform PlayerCharacter;
    public float Speed = 3;
    public bool hasHurtEnemy = false;
    public float pushBack = .25f;
    public float pushBackDelta;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCharacter = GameObject.FindGameObjectWithTag("Player").transform;
        pushBackDelta = pushBack;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHurtEnemy)
        {
            pushBackDelta -= Time.deltaTime;
            if (pushBackDelta < 0)
            {
                pushBackDelta = pushBack;
                hasHurtEnemy = false;
            }
        }

        // get direction you want to point at
        Vector2 direction = ((Vector2)PlayerCharacter.position - (Vector2)transform.position).normalized;

        // set vector of transform directly
        transform.up = direction;

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(PlayerCharacter.transform.position.x, PlayerCharacter.transform.position.y), (hasHurtEnemy ?  -Speed * Time.deltaTime : (Speed * Time.deltaTime)));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            hasHurtEnemy = true;
            PlayerCharacter.gameObject.GetComponent<PlayerScript>().TakenDamage(EnemyName);
        }

        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
