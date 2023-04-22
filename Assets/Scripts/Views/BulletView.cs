using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Game.SpaceShooter.Models;
using Game.SpaceShooter.Managers;

namespace Game.SpaceShooter.Views
{
    public class BulletView : MonoBehaviour
    {
        #region --------------- Private Variables --------------
        #region ----- SerializeField -----
        [SerializeField] private BulletModel bulletModelObj;
        [SerializeField] private Rigidbody2D rb;
        #endregion

        #region ----- Non-SerializeField -----
        private Text score;
        private int sc = 0;
        #endregion

        #endregion

        #region --------------- Private Methods ---------------
        private void Start()
        {
            StartCoroutine(Move());
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            switch (col.tag)
            {
                case "Enemy":
                    if (bulletModelObj.isPlayerBullet)
                    {
                        if (rb.position.y < (bulletModelObj.gameHeight / 2) - 0.5)
                        {
                            Destroy(rb.gameObject);
                            sc = Int16.Parse(score.text);
                            sc += 1;
                            score.text = sc.ToString();
                        }
                    }
                    break;

                case "Player":
                    if (!bulletModelObj.isPlayerBullet)
                        Destroy(rb.gameObject);
                    break;

                case "EnemyBullet":
                    if (bulletModelObj.isPlayerBullet)
                    {
                        Destroy(rb.gameObject);
                        AudioManager.Instance.BulletCollision();
                    }
                    break;

                case "PlayerBullet":
                    if (!bulletModelObj.isPlayerBullet)
                        Destroy(rb.gameObject);
                    break;

                default:
                    break;
            }
        }

        private void CheckBoundary()
        {
            if (rb.position.y > bulletModelObj.gameHeight / 2 || rb.position.y < -(bulletModelObj.gameHeight / 2))
            {
                Destroy(rb.gameObject);
            }
        }
        #endregion

        #region --------------- Public Methods ---------------
        #endregion

        #region --------------- Coroutine ---------------
        IEnumerator Move()
        {
            rb.velocity = new Vector2(0f, bulletModelObj.bulletSpeed);
            while (rb.gameObject)
            {
                if(GameObject.Find("Score"))
                {
                    score = GameObject.Find("Score").GetComponent<Text>();
                }
                CheckBoundary();
                yield return null;
            }
        }
        #endregion
    }
}
