using UnityEngine;
using System.Collections;
using Game.SpaceShooter.Models;
using Game.SpaceShooter.Controllers;
using Game.SpaceShooter.Managers;
using UnityEngine.UI;

namespace Game.SpaceShooter.Views
{
    public class PlayerView : MonoBehaviour
    {
        #region --------------- Private Variables ---------------
        #region ----- SerializeField -----
        [SerializeField] private PlayerModel playerModelObj;
        [SerializeField] private PlayerController playerControllerObj;
        [SerializeField] private Rigidbody2D rb;

        [SerializeField] private Text scoreObj;
        [SerializeField] private Text finalScore;
        [SerializeField] private GameObject playerBullet;
        [SerializeField] private Transform attackPoint;

        [SerializeField] private GameObject gameOverCanvas;
        [SerializeField] private Text message;
        [SerializeField] private GameObject restartButton;
        [SerializeField] private GameObject gameWorld;
        [SerializeField] private GameObject gameCanvases;

        [SerializeField] private Animator animatorObj;
        [SerializeField] private Healthbar Healthbar;

        #endregion

        #region ----- Non-SerializeField -----
        private Vector3 mousePosition;
        private float attackDelay;
        private int currentLevel = 0;
        #endregion

        #endregion

        #region --------------- Private Methods ---------------
        private void Start()
        {
            playerModelObj.isGameOver = false;
            playerModelObj.damage = playerModelObj.barFillAmount / playerModelObj.health;
            currentLevel = playerControllerObj.ReturnCurrentLevel();
            attackDelay = playerModelObj.attackDelay[currentLevel];
            StartCoroutine(Move());
            StartCoroutine(Attack());
        }

        private void CheckForInput()
        {
            if (Input.GetMouseButton(0))
            {
                mousePosition = Input.mousePosition;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = Camera.main.nearClipPlane;
                rb.position = new Vector2(worldPosition.x, rb.position.y);
            }

            if (Input.GetKey("d"))
            {
                playerModelObj.moveDirection = 1;
            }
            if (Input.GetKey("a"))
            {
                playerModelObj.moveDirection = -1;
            }

           
        }

        public void CheckBoundary()
        {
            if (rb.position.x > playerModelObj.gameWidth / 2)
            {
                rb.position = new Vector2(playerModelObj.gameWidth / 2, rb.position.y);
            }
            if (rb.position.x < -(playerModelObj.gameWidth / 2))
            {
                rb.position = new Vector2(-(playerModelObj.gameWidth / 2), rb.position.y);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Enemy" || col.tag == "EnemyBullet" )
            {
                DamagePlayerHealthbar();
                if (playerModelObj.health <= 0)
                {
                    GameOver();
                }
                
            }
        }

        private void SpawnBullet()
        {
            Instantiate(playerBullet, attackPoint.position, Quaternion.identity);
            AudioManager.Instance.PlayerShooting();
        }

        private void MoveShip()
        {
            rb.position = rb.position + new Vector2(playerModelObj.moveDirection * playerModelObj.moveSpeed, 0f);
            playerModelObj.moveDirection = 0f;
        }

        private void GameOver()
        {
            Debug.Log("Game Over");
            finalScore.text = scoreObj.text;
            message.text = "GAME OVER";
            gameCanvases.SetActive(false);
            gameOverCanvas.SetActive(true);
            restartButton.SetActive(true);
            playerModelObj.isGameOver = true;
            AudioManager.Instance.PlayerDestroyed();
            animatorObj.SetBool("playerDestroyed", true);
            StartCoroutine(PlayerExplosion());
        }

        #endregion

        #region --------------- Public Methods ---------------
        #endregion

        #region --------------- Coroutines ---------------
        IEnumerator Move()
        {
            while (!playerModelObj.isGameOver)
            {
                CheckForInput();
                MoveShip();
                CheckBoundary();
                yield return null;
            }
        }

        IEnumerator Attack()
        {
            while (!playerModelObj.isGameOver)
            {
                SpawnBullet();
                yield return new WaitForSeconds(attackDelay);
            }
        }

        IEnumerator PlayerExplosion()
        {
            yield return new WaitForSeconds(0.5f);
            rb.gameObject.SetActive(false);
        }

        void DamagePlayerHealthbar()
        {
            if (playerModelObj.health > 0)
            {
                playerModelObj.health -= 1;
                playerModelObj.barFillAmount = playerModelObj.barFillAmount - playerModelObj.damage;
                Healthbar.SetAmount(playerModelObj.barFillAmount);
            }
        }
        #endregion
    }
}
