using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    public GameObject gameOverPanel;
    public float health;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;
    public Transform target;
    public float attackRadius;

    Vector2 movement;
    Vector2 mousePos;

    public UI_Inventory ui_inventory;

    public DeathSoundController deathSoundController;

    private bool dead = false;

    private float timer = 0.0f;
    private float waitTime = 2.0f;

    void Start()
    {
        health = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (!dead)
            {
                deathSoundController.PlayCharacterDeath(); //Plays character death sound
                dead = true;
            }            
            gameOverPanel.SetActive(true);
            ui_inventory.isGameOver = true; //So you can't open inventory if game over
            ui_inventory.canvasGroup.alpha = 0; //Hides inventory
            Time.timeScale = 0f;
            if (ui_inventory.isGameOver && Input.GetKeyDown("space")) //if it's game over and we press Space, we reload the scene
            {
                Input.ResetInputAxes();
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            health -= .10f;
            healthBar.SetSize(health);
            timer = 0.0f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {        
        if (collision.gameObject.tag == "Zombie")
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                health -= .10f;
                healthBar.SetSize(health);
                timer = 0.0f;
            }
        }
    }
}
