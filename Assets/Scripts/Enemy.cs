using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField]
    Transform exit; // исчезновение персонажа
    [SerializeField]
    Transform[] wayPoints;
    [SerializeField]
    float navigation;
    [SerializeField]
    int health;

    int target = 0; // счетчик целей
    Transform enemy;
    Collider2D enemyCollider;
    Animator anim;
    float navigationTime = 0; // положение противника
    bool isDead = false;

    public bool IsDead
    {
        get 
        { 
            return isDead; 
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Transform>();
        Manager.Instance.RegisterEnemy(this);
        enemyCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wayPoints != null && isDead == false) 
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigation)
            {
                if (target < wayPoints.Length)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
                }
                else
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, exit.position, navigationTime);
                }
                navigationTime = 0;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "moviengPoint")
        {
            target += 1;
        }
        else if (collision.tag == "Finish")
        {
            Manager.Instance.UnRegisterEnemy(this);
        }
        else if (collision.tag == "Projectile")
        {
            Projectile newP = collision.GetComponent<Projectile>();
            EnemyHit(newP.AttackDamage);
            Destroy(collision.gameObject);
        }
    }

    public void EnemyHit(int hitPoints)
    {
        if (health - hitPoints > 0)
        {
           health -= hitPoints; // получение урона
        }
        else 
        {
            Die();
            anim.SetTrigger("didDie");
        }
    }

    public void Die()
    {
        isDead = true;
        enemyCollider.enabled = false;
    }
}
