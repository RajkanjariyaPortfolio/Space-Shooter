using System;

namespace Game.SpaceShooter.Models
{
    [Serializable]
    public class EnemyModel
    {
        #region --------------- Public Variables ---------------
        public float[] moveSpeed = {};
        public float gameHeight;
        public float[] enemySpawnPoints = {};
        #endregion
    }
}
