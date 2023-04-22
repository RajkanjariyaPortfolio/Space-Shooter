using UnityEngine;

namespace Game.SpaceShooter.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region --------------- Private Variables ---------------

        #region ----- Non-SerializeFields -----
        private static LevelManager _instance;
        private static int currentLevel = 0;
        private static bool isEndlessMode;
        private static bool isGameComplete;
        #endregion

        #endregion

        #region --------------- Public Variables ---------------

        [HideInInspector] public static LevelManager Instance { get { return _instance; } }

        #endregion

        #region --------------- Private Methods ---------------

        private void Awake()
        {
            SetEndlessMode();
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        #endregion

        #region --------------- Public Methods ---------------

        public void UpdateCurrentLevel()
        {
            if (!isEndlessMode)
            {
                if (currentLevel < 3)
                {
                    currentLevel += 1;
                }
            }
        }

        public void ResetCurrentLevel()
        {
            currentLevel = 0;
        }

        public void SetEndlessMode()
        {
            isEndlessMode = MenuManager.Instance.ReturnEndlessMode();
        }

        public int ReturnCurrentLevel()
        {
            return currentLevel;
        }

        public bool ReturnEndlessMode()
        {
            Debug.Log(isEndlessMode);
            return isEndlessMode;
        }

        #endregion

        #region --------------- Coroutines ---------------

        #endregion
    }
}
