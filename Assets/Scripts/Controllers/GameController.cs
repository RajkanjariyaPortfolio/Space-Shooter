using UnityEngine;
using Game.SpaceShooter.Views;
using Game.SpaceShooter.Managers;

namespace Game.SpaceShooter.Controllers
{
    public class GameController : MonoBehaviour
    {
        #region --------------- Private Variables ---------------
        #region ----- SerializeField -----
        [SerializeField] private GameView gameViewObj;
        #endregion
        #endregion

        #region --------------- Public methods ---------------

        public int ReturnCurrentLevel()
        {
            return LevelManager.Instance.ReturnCurrentLevel();
        }

        public bool ReturnEndlessMode()
        {
            return LevelManager.Instance.ReturnEndlessMode();
        }

        #endregion
    }
}
