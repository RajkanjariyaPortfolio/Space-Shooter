using UnityEngine;
using System.Collections;
using Game.SpaceShooter.Models;
using Game.SpaceShooter.Managers;

namespace Game.SpaceShooter.Views
{
    public class EnemyView : MonoBehaviour
    {
        #region --------------- Private Variables ---------------
        #region ----- SerializeField -----
        [SerializeField] private EnemyModel enemyModelObj;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private GameObject enemyBullet;
        [SerializeField] private GameObject enemyExplosion;
        #endregion

        #region ----- Non-SerializeField -----
        private int i;  // Bullet Spawn Delay
        private int j;  // Enemy Spawn Point
        private int currentLevel;
        #endregion
        #endregion

        #region --------------- Private Methods ---------------
        private void Start()
        {
            currentLevel = LevelManager.Instance.ReturnCurrentLevel();
            StartCoroutine(Move());
            StartCoroutine(Attack());
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                rb.gameObject.SetActive(false);
            }

            if (col.tag == "PlayerBullet")
            {
                if (rb.position.y < (enemyModelObj.gameHeight / 2) - 0.5)
                {
                    GameObject EnemyExplosion = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
                    Destroy(EnemyExplosion, 0.4f);
                    Destroy(rb.gameObject);
                    AudioManager.Instance.EnemyDestroyed();
                }
            }
        }

        private void SpawnBullet()
        {
            Vector2 position = rb.position + new Vector2(0f, -0.2f);
            Instantiate(enemyBullet, position, Quaternion.identity);
            AudioManager.Instance.EnemyShooting();
        }

        private void CheckBoundary()
        {
            if (rb.position.y < -(enemyModelObj.gameHeight / 2))
            {
                int j = Random.Range(0, 5);
                rb.position = new Vector2(enemyModelObj.enemySpawnPoints[j], 5);
                StartCoroutine(Attack());
            }
        }
        #endregion

        #region --------------- Public Methods ---------------

        #endregion

        #region --------------- Coroutine ---------------
        IEnumerator Move()
        {
            rb.velocity = new Vector2(0f, -enemyModelObj.moveSpeed[currentLevel]);
            while (rb.gameObject)
            {
                CheckBoundary();
                yield return null;
            }
        }

        IEnumerator Attack()
        {
            i = Random.Range(0, 3);
            yield return new WaitForSeconds(i);
            SpawnBullet();
        }
        #endregion
    }
}
