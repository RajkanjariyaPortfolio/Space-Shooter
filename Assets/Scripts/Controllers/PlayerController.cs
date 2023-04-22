using UnityEngine;
using Game.SpaceShooter.Views;
using Game.SpaceShooter.Managers;

namespace Game.SpaceShooter.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        #region --------------- Private Variables ---------------
        #region ----- SerializeField -----
        [SerializeField] private PlayerView playerViewObj;
        #endregion
        #endregion

        #region --------------- Public Methods ---------------
        
        public int ReturnCurrentLevel()
        {
            return LevelManager.Instance.ReturnCurrentLevel();
        }

        #endregion
    }
}
