using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Game.SpaceShooter.Models;
using Game.SpaceShooter.Controllers;
using Game.SpaceShooter.Managers;

namespace Game.SpaceShooter.Views
{
    public class GameView : MonoBehaviour
    {
        #region --------------- Private Variables ---------------

        #region ----- SerializeField -----
        [SerializeField] private GameModel gameModelObj;
        [SerializeField] private GameController gameControllerObj;
        [SerializeField] private Rigidbody2D playerRB;
        [SerializeField] private GameObject enemyShip;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject powerUp;
        [SerializeField] private Text scoreText;
        #endregion

        #region ----- Non-SerializeField -----
        #endregion
        private bool spawnEnemy = true;
        private int currentLevel;
        private int i = 0;
        private int previousSpawnPoint = 0;
        private int previousScore = 0;
        private int score;
        private bool isEndlessMode;
        private float enemySpawnDelay;
        #endregion

        #region --------------- Private Methods ---------------
        private void Start()
        {
            currentLevel = gameControllerObj.ReturnCurrentLevel();
            isEndlessMode = gameControllerObj.ReturnEndlessMode();
            enemySpawnDelay = gameModelObj.enemySpawnDelay[currentLevel];
            AudioManager.Instance.PlayMusic();
            StartCoroutine(SpawnEnemy());
            if (isEndlessMode)
            {
                StartCoroutine(UpdateScore());
                StartCoroutine(IncreaseDifficulty());
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Enemy")
            {
                spawnEnemy = false;
            }
        }

        private bool CheckScore()
        {
            if (score % 20 == 0 && score != 0 && score > previousScore)
            {
                return true;
            }
            else
                return false;
        }
        #endregion

        #region --------------- Public Methods ---------------
        #endregion

        #region --------------- Coroutine ---------------
        IEnumerator SpawnEnemy()
        {
            while (enemyShip)
            {
                if (spawnEnemy)
                {
                    do
                    {
                        i = UnityEngine.Random.Range(0, 5);
                    } while (i == previousSpawnPoint);

                    previousSpawnPoint = i;
                    Vector3 position = spawnPoint.position + new Vector3(gameModelObj.enemySpawnPoints[i], 0, 0);
                    Instantiate(enemyShip, position, Quaternion.identity);
                }
                else
                    spawnEnemy = true;
                yield return new WaitForSeconds(enemySpawnDelay);
            }
        }

        IEnumerator UpdateScore()
        {
            while (1 < 2)
            {
                score = Int16.Parse(scoreText.text);
                yield return null;
            }
        }

       

        IEnumerator IncreaseDifficulty()
        {
            while (1 < 2)
            {
                yield return new WaitForSeconds(15);
                if (enemySpawnDelay > 0.5f)
                {
                    enemySpawnDelay -= 0.05f;
                }
            }
        }

        #endregion
    }
}
