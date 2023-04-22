using UnityEngine;
using UnityEngine.SceneManagement;
using Game.SpaceShooter.Managers;

namespace Game.SpaceShooter.Controllers
{
    public class LevelController : MonoBehaviour
    {
        #region --------------- Private Variables ---------------

        #region ----- SerializeFields -----
        #endregion

        #region ----- Non-SerializeFields -----
        #endregion

        #endregion

        #region --------------- Private Methods ---------------

        #endregion

        #region --------------- Public Methods ---------------
        
        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void PlayGame()
        {
            Time.timeScale = 1;
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GoToMenu()
        {
            LevelManager.Instance.ResetCurrentLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            AudioManager.Instance.DontPlayAudio();
        }

        public void NextLevel()
        {
            LevelManager.Instance.UpdateCurrentLevel();
            ReloadScene();
        }

        public void ResetCurrentLevel()
        {
            LevelManager.Instance.ResetCurrentLevel();
        }

        public int ReturnCurrentLevel()
        {
            return LevelManager.Instance.ReturnCurrentLevel();
        }

        public bool ReturnEndlessMode()
        {
            return MenuManager.Instance.ReturnEndlessMode();
        }

        #endregion
    }
}
