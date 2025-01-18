using UnityEngine;

public class BarrierObject : MonoBehaviour
{
    //I was having trouble keeping the player within the screen
    public GameObject barrierPrefab;
    public GameObject enemyPrefab;
    public float SpawnRate = 3;
    private float spawnRateDelta;

    private GameObject topBarrier;
    private GameObject bottomBarrier;
    private GameObject leftBarrier;
    private GameObject rightBarrier;

    private Vector3 screenBounds;
    enum Barrier
    {
        Top = 1,
        Bottom, 
        Left, 
        Right
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //Top Barrier
        topBarrier = Instantiate(barrierPrefab);
        topBarrier.transform.position = new Vector3(0, screenBounds.y + (topBarrier.transform.localScale.x/.5f), 0);
        topBarrier.transform.localScale = new Vector3(screenBounds.x * 3, 1, 1);
        //Bottom Barrier
        bottomBarrier = Instantiate(barrierPrefab);
        bottomBarrier.transform.position = new Vector3(0, -(screenBounds.y + (bottomBarrier.transform.localScale.x/.5f)), 0);
        bottomBarrier.transform.localScale = new Vector3(screenBounds.x * 3, 1, 1);
        //Left Barrier
        leftBarrier = Instantiate(barrierPrefab);
        leftBarrier.transform.position = new Vector3(-(screenBounds.x + (leftBarrier.transform.localScale.y/.5f)), 0, 0);
        leftBarrier.transform.localScale = new Vector3(1, screenBounds.y * 3, 1);
        //Right Barrier
        rightBarrier = Instantiate(barrierPrefab);
        rightBarrier.transform.position = new Vector3((screenBounds.x + (rightBarrier.transform.localScale.y/.5f)), 0, 0);
        rightBarrier.transform.localScale = new Vector3(1, screenBounds.y * 3, 1);

        spawnRateDelta = SpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        spawnRateDelta -= Time.deltaTime;
        if (spawnRateDelta <= 0)
        {
            spawnRateDelta = SpawnRate;
            //number range will be 1-4
            var selection = Random.Range(1, 5);
            Debug.Log(selection);
            var randomX = Random.Range(-screenBounds.x, screenBounds.x);
            var randomY = Random.Range(-screenBounds.y, screenBounds.y);
            switch ((Barrier)selection)
            {
                case Barrier.Top:
                    Instantiate(enemyPrefab, new Vector3(randomX, topBarrier.transform.position.y, 0), new Quaternion());
                    break;
                case Barrier.Bottom:
                    Instantiate(enemyPrefab, new Vector3(randomX, bottomBarrier.transform.position.y, 0), new Quaternion());
                    break;
                case Barrier.Left:
                    Instantiate(enemyPrefab, new Vector3(leftBarrier.transform.position.x, randomY , 0), new Quaternion());
                    break;
                case Barrier.Right:
                    Instantiate(enemyPrefab, new Vector3(rightBarrier.transform.position.x , randomY , 0), new Quaternion());
                    break;
            }
        }
    }
}
