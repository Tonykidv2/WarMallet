using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BarrierObject : MonoBehaviour
{
    public List<AudioClip> audioClips;
    private AudioSource AudioSource;

    //I was having trouble keeping the player within the screen
    public GameObject scoreText;
    public GameObject barrierPrefab;
    public GameObject enemyPrefab;
    private float SpawnRate = 3;
    private float spawnRateDelta;

    private GameObject topBarrier;
    private GameObject bottomBarrier;
    private GameObject leftBarrier;
    private GameObject rightBarrier;

    private Vector3 screenBounds;

    private int Score;
    private int enemyDefeated = 0;

    private Difficulty currDifficulty;

    enum Barrier
    {
        Top = 1,
        Bottom, 
        Left, 
        Right
    }

    enum Difficulty
    {
        Easy = 1,
        Medium,
        Hard,
        Ultra
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //Top Barrier
        topBarrier = Instantiate(barrierPrefab);
        topBarrier.transform.position = new Vector3(0, screenBounds.y + (topBarrier.transform.localScale.x/*/.75f*/), 0);
        topBarrier.transform.localScale = new Vector3(screenBounds.x * 3, 1, 1);
        //Bottom Barrier
        bottomBarrier = Instantiate(barrierPrefab);
        bottomBarrier.transform.position = new Vector3(0, -(screenBounds.y + (bottomBarrier.transform.localScale.x/*/.75f*/)), 0);
        bottomBarrier.transform.localScale = new Vector3(screenBounds.x * 3, 1, 1);
        //Left Barrier
        leftBarrier = Instantiate(barrierPrefab);
        leftBarrier.transform.position = new Vector3(-(screenBounds.x + (leftBarrier.transform.localScale.y/*/.75f*/)), 0, 0);
        leftBarrier.transform.localScale = new Vector3(1, screenBounds.y * 3, 1);
        //Right Barrier
        rightBarrier = Instantiate(barrierPrefab);
        rightBarrier.transform.position = new Vector3((screenBounds.x + (rightBarrier.transform.localScale.y/*/.75f*/)), 0, 0);
        rightBarrier.transform.localScale = new Vector3(1, screenBounds.y * 3, 1);

        spawnRateDelta = SpawnRate;

        currDifficulty = Difficulty.Easy;
        scoreText = GameObject.FindGameObjectWithTag("Score");
    }

    // Update is called once per frame
    void Update()
    {
        spawnRateDelta -= Time.deltaTime;
        if (spawnRateDelta <= 0)
        {
            spawnRateDelta = SpawnRate;

            SpawnEnemy(currDifficulty);
        }
    }

    public int GetScore()
    {
        var result = Mathf.Clamp(Score, 0, 999999999);
        return result;
    }

    public void EnemyDefeated()
    {
        enemyDefeated++;
        
        Score += 100;

        Score = Mathf.Clamp(Score, 0, 999999999);

        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + Score.ToString().PadLeft(9, '0');

        int randomIndex = Random.Range(0, 8);
        AudioSource.PlayOneShot(audioClips[randomIndex]);

        if (enemyDefeated >= 10)
        {
            enemyDefeated = 0;

            switch (currDifficulty)
            {
                case Difficulty.Easy:
                    currDifficulty = Difficulty.Medium;
                    break;
                case Difficulty.Medium:
                    currDifficulty = Difficulty.Hard;
                    break;
            }
        }
    }

    public void PlayerHit()
    {
        enemyDefeated = 0;

        switch (currDifficulty)
        {
            case Difficulty.Medium:
                currDifficulty = Difficulty.Easy;
                break;
            case Difficulty.Hard:
                currDifficulty = Difficulty.Medium;
                break;
        }
    }

    void SpawnEnemy(Difficulty difficulty)
    {
        switch(difficulty)
        {
            case Difficulty.Easy:
                SpawnEnemy();
                break;
            case Difficulty.Medium:
                Enumerable.Range(0, 3).ToList().ForEach(_ => SpawnEnemy());
                break;
            case Difficulty.Hard:
                Enumerable.Range(0, 5).ToList().ForEach(_ => SpawnEnemy());
                break;
        }
    }

    void SpawnEnemy()
    {
        //number range will be 1-4
        var selection = Random.Range(1, 5);

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
                Instantiate(enemyPrefab, new Vector3(leftBarrier.transform.position.x, randomY, 0), new Quaternion());
                break;
            case Barrier.Right:
                Instantiate(enemyPrefab, new Vector3(rightBarrier.transform.position.x, randomY, 0), new Quaternion());
                break;
        }
    }
}
