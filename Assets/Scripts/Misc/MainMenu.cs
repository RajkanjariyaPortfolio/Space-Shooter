using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Game.SpaceShooter.Managers;

namespace Game.SpaceShooter.Misc
{
    public class MainMenu : MonoBehaviour
    {
        #region --------------- Private Variables ---------------

        #region ----- SerializeFields -----
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject logo;
        [SerializeField] private GameObject settingsMenu;
        [SerializeField] private Slider volumeSlider;
        #endregion

        #region ----- Non-SerializeFields -----
        #endregion

        #endregion

        #region --------------- Public methods ---------------

        public void PlayGame()
        {
            MenuManager.Instance.SetEndlessMode(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void GoToSettings()
        {
            mainMenu.SetActive(false);
            logo.SetActive(false);
            settingsMenu.SetActive(true);
        }

        public void GoToMainMenu()
        {
            mainMenu.SetActive(true);
            logo.SetActive(true);
            settingsMenu.SetActive(false);
        }

        public void HandleVolume()
        {
            AudioListener.volume = volumeSlider.value;
        }

        public void EndlessMode()
        {
            MenuManager.Instance.SetEndlessMode(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        #endregion
    }
}
