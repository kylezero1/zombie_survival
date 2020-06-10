using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieType2 : Enemy
{
    [SerializeField] private HealthBar healthBar;
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    private float roamRadius = 3f;
    public Transform homePosition;
    private ScoreController scoreController;
    public DeathSoundController deathSoundController;
    Vector2 roamPos;

    // Start is called before the first frame update
    void Start()
    {
        health = 1f;
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        scoreController = GameObject.Find("txtScore").GetComponent<ScoreController>();
        deathSoundController = GameObject.Find("DeathSoundPlayer").GetComponent<DeathSoundController>();

        roamPos = transform.position;
        roamPos += Random.insideUnitCircle.normalized * roamRadius;
    }

    void Update()
    {
        if (health <= 0)
        {
            int randomCoin = Random.Range(1, 10);
            Item coinItem = new Item { itemType = Item.ItemType.Coin, amount = randomCoin };
            ItemWorld.DropItem(transform.position, coinItem);

            scoreController.incrementScore();
            deathSoundController.PlayZombieDeath();
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        ChecckDistance();
    }

    void ChecckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            myRigidbody.MovePosition(temp);
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            if (Vector3.Distance(roamPos, transform.position) < 0.5f)
            {
                roamPos = transform.position;
                roamPos += Random.insideUnitCircle.normalized * roamRadius;
            }


            Vector3 temp2 = Vector3.MoveTowards(transform.position, roamPos, moveSpeed * Time.deltaTime);
            myRigidbody.MovePosition(temp2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= .5f;
            healthBar.SetSize(health);
            myRigidbody.velocity -= myRigidbody.velocity; //So the zombie doesn't go flying after getting shot
        }

    }
}
