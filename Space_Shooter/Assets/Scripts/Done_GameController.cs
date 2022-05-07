﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
    public List<GameObject> hazards;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float EnemySpawnWait1;
    public float EnemySpawnWait2;
    public float EnemySpawnWait3;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public bool gameOver;

    private bool restart;
    private bool e1, e2, e3;
    private int score;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        e1 = false;
        e2 = false;
        e3 = false;
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetButton("Submit"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (!e1 && Time.time > EnemySpawnWait1) {
                e1 = true;
                hazards.Add(Enemy1);
            }

            if (!e2 && Time.time > EnemySpawnWait2)
            {
                e2 = true;
                hazards.Add(Enemy2);
            }

            if (!e3 && Time.time > EnemySpawnWait3)
            {
                e3 = true;
                hazards.Add(Enemy3);
            }

            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Count)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press Restart Button for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}