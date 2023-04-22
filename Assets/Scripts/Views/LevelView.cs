using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Game.SpaceShooter.Controllers;

namespace Game.SpaceShooter.Views
{
    public class LevelView : MonoBehaviour
    {
        #region --------------- Private Variables ---------------

        #region ----- SerializeFields -----
        [SerializeField] private GameObject pausedText;
        [SerializeField] private GameObject pauseGame;
        [SerializeField] private GameObject playGame;

        [SerializeField] private GameObject restartGame;
        [SerializeField] private GameObject quitGame;
        [SerializeField] private GameObject nextLevel;

        [SerializeField] private GameObject player;
        [SerializeField] private GameObject gameCanvas;
        [SerializeField] private GameObject levelCompleteCanvas;
        [SerializeField] private GameObject nextLevelButton;
        [SerializeField] private GameObject restartButton;

        [SerializeField] private Text message;
        [SerializeField] private Text scoreObj;
        [SerializeField] private Text levelCompleteScoreObj;

        [SerializeField] private LevelController levelControllerObj;
        #endregion

        #region ----- Non-SerializeFields -----
        private int currentLevel;
        private int[] levelScores = { 10, 20, 50, 100 };
        private bool isEndlessMode;
        private int score;
        #endregion

        #endregion

        #region --------------- Private methods ---------------

        private void Start()
        {
            Button pauseBtn = pauseGame.GetComponent<Button>();
            pauseBtn.onClick.AddListener(PauseGame);
            Button playBtn = playGame.GetComponent<Button>();
            playBtn.onClick.AddListener(PlayGame);
            Button restartBtn = restartGame.GetComponent<Button>();
            restartBtn.onClick.AddListener(RestartGame);
            Button quitBtn = quitGame.GetComponent<Button>();
            quitBtn.onClick.AddListener(GoToMenu);
            Button nextLevelBtn = nextLevel.GetComponent<Button>();
            nextLevelBtn.onClick.AddListener(NextLevel);

            currentLevel = levelControllerObj.ReturnCurrentLevel();
            isEndlessMode = levelControllerObj.ReturnEndlessMode();
            Debug.Log(currentLevel);
            
            if (!isEndlessMode)
            {
                StartCoroutine(UpdateScore());
            }
        }

        private void PauseGame()
        {
            levelControllerObj.PauseGame();
            pauseGame.SetActive(false);
            playGame.SetActive(true);
            pausedText.SetActive(true);
        }

        private void PlayGame()
        {
            levelControllerObj.PlayGame();
            pauseGame.SetActive(true);
            playGame.SetActive(false);
            pausedText.SetActive(false);
        }

        private void RestartGame()
        {
            levelControllerObj.ResetCurrentLevel();
            levelControllerObj.ReloadScene();
        }

        private void GoToMenu()
        {
            levelControllerObj.GoToMenu();
        }

        private void NextLevel()
        {
            levelControllerObj.NextLevel();
        }

        private void LevelComplete()
        {
            levelCompleteScoreObj.text = score.ToString();
            levelCompleteCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            player.SetActive(false);

            if (levelControllerObj.ReturnCurrentLevel() == levelScores.Length - 1)
            {
                Debug.Log("Game Complete");
                message.text = "GAME COMPLETE";
                restartButton.SetActive(true);
            }
            else
            {
                Debug.Log("Level Complete");
                message.text = "LEVEL COMPLETE";
                nextLevelButton.SetActive(true);
            }
        }

        #endregion

        #region --------------- Coroutines ---------------

        IEnumerator UpdateScore()
        {
            while (1 < 2)
            {
                score = Int16.Parse(scoreObj.text);
                if (score == levelScores[currentLevel])
                {
                    LevelComplete();
                    break;
                }
                yield return null;
            }
        }

        #endregion
    }
}
