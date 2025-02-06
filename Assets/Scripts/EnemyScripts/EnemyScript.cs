using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public BarrierObject BarrierSpawner;

    public string EnemyName = string.Empty;

    private Transform PlayerCharacter;
    private NavMeshAgent Agent;
    public float Speed = 3;
    public bool hasHurtEnemy = false;
    public float pushBack = .25f;
    public float pushBackDelta;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BarrierSpawner = GameObject.FindGameObjectWithTag("Barrier").GetComponent<BarrierObject>();
        PlayerCharacter = GameObject.FindGameObjectWithTag("Player").transform;
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        

        pushBackDelta = pushBack;
    }

    // Update is called once per frame
    void Update()
    {
        // get direction you want to point at
        Vector2 direction = ((Vector2)PlayerCharacter.position - (Vector2)transform.position).normalized;

        if (hasHurtEnemy)
        {
            pushBackDelta -= Time.deltaTime;
            if (pushBackDelta < 0)
            {
                pushBackDelta = pushBack;
                hasHurtEnemy = false;
            }

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(PlayerCharacter.transform.position.x, PlayerCharacter.transform.position.y), (hasHurtEnemy ? -Speed * Time.deltaTime : (Speed * Time.deltaTime)));
        }
        else
        {
            Agent.SetDestination(PlayerCharacter.position);
        }

        transform.up = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            hasHurtEnemy = true;
            PlayerCharacter.gameObject.GetComponent<PlayerScript>().TakenDamage(EnemyName);
            BarrierSpawner.PlayerHit();
        }

        if(collision.gameObject.tag == "Bullet")
        {
            BarrierSpawner.EnemyDefeated();
            Destroy(gameObject);
        }
    }
}
