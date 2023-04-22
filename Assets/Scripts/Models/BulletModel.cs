using System;

namespace Game.SpaceShooter.Models
{
    [Serializable]
    public class BulletModel
    {
        #region --------------- Public Variables ---------------
        public bool isPlayerBullet;
        public float bulletSpeed;
        public float gameHeight = 10f;
        #endregion
    }
}
