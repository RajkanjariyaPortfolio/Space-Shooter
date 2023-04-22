using System;

namespace Game.SpaceShooter.Models
{
    [Serializable]
    public class PlayerModel
    {
        #region --------------- Public Variables ---------------
        public float moveDirection;
        public float moveSpeed;
        public float[] attackDelay = {};
        public bool isGameOver;
        public float gameWidth;

        public float health = 20f;
        public float barFillAmount = 1f;
        public float damage = 0;
        #endregion
    }
}
