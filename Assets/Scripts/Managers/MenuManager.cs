using UnityEngine;

namespace Game.SpaceShooter.Managers
{
    public class MenuManager : MonoBehaviour
    {
        private static MenuManager _instance;
        private static bool isEndlessMode = false;

        [HideInInspector] public static MenuManager Instance { get { return _instance; } }

        private void Awake()
        {
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

        public void SetEndlessMode(bool endless)
        {
            isEndlessMode = endless;
        }
        
        public bool ReturnEndlessMode()
        {
            return isEndlessMode;
        }
    }
}
