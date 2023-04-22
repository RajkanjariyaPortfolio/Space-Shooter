using System;

namespace Game.SpaceShooter.Models
{
    [Serializable]
    public class GameModel
    {
        #region --------------- Public Variables ---------------
        public float width = 3.4f;
        public float height = 10;
        public float[] enemySpawnDelay = {0.8f, 0.7f, 0.6f, 0.5f};
        public float difficultyFactor = 0.03f;
        public float[] enemySpawnPoints = { -1.6f, -0.8f, 0f, 0.8f, 1.6f };
        #endregion
    }
}
