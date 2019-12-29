using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public int missingHp = 0;
    public int maxHp = 100;
    
    public int level = 1;
    public int experience = 0;
    public int experienceToNextLevel = 100;
    public int damage = 10;
    public float speed = 5.0f;
    public float attackRange = 1.0f;
    public float blockTime = 1;

    public LayerMask enemyLayer; 
    public Transform attackPoint;

    [HideInInspector] public Inventory inventory;
    public HudManager hudManager;

    private Animator _animator;
    public float _timer = 0;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    private static readonly int BlockTrigger = Animator.StringToHash("Block");
    private static readonly int Hit = Animator.StringToHash("Hit");

    public bool IsBlocking { get; private set; }

    private void Start() {
        inventory = GetComponent<Inventory>();
        _animator = GetComponent<Animator>();

        hudManager.UpdateLife(missingHp, maxHp);
        hudManager.UpdateExperience(level, experience, experienceToNextLevel);
    }

    private void Update() {
        if (IsBlocking) {
            _timer += Time.deltaTime;

            if (_timer >= blockTime) {
                IsBlocking = false;
                _timer = 0;
            }
        }
        
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 position = transform.position;
        
        position += new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime);

        if (position.y > 0f)
            position.y = 0f;

        if (position.y < -3.1f)
            position.y = -3.1f;

        transform.position = position;

        if (Math.Abs(horizontal) > 0.1) {
            transform.rotation = Quaternion.Euler(0, (horizontal < 0 ? 1 : 0) * 180, 0);
        }

        _animator.SetFloat(Speed, Mathf.Abs(horizontal));
        
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            Block();
        }

        else if (Input.GetKeyDown(KeyCode.Space)) {
            Attack();
        }
        
        else if (Input.GetKeyDown(KeyCode.F1)) {
            Die();
        }
        
        else if (Input.GetKeyDown(KeyCode.F2)) {
            Application.Quit();
        }
    }

    private void Attack() {
        _animator.SetTrigger(AttackTrigger);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies) {
            IHitable enemyScript = enemy.GetComponent<IHitable>();

            if (enemyScript.Hit(damage * level)) {
                AddExp(enemyScript.GetExp());
                AddHp(enemyScript.GetHp());
            }
        }
    }

    private void Block() {
        IsBlocking = true;
        _animator.SetTrigger(BlockTrigger);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void AddExp(int amount) {
        experience += amount;

        if (experience >= experienceToNextLevel) {
            LevelUp();
        }
        
        hudManager.UpdateExperience(level, experience, experienceToNextLevel);
    }
    
    private void AddHp(int amount) {
        missingHp -= amount;

        if (missingHp < 0)
            missingHp = 0;
        
        hudManager.UpdateLife(missingHp, maxHp);
    }

    private void LevelUp() {
        experience -= experienceToNextLevel;
        experienceToNextLevel *= 2;
        
        level += 1;
        maxHp += 10;
        missingHp -= 10;
        attackRange += 0.05f;
        
        if (missingHp < 0)
            missingHp = 0;
        
        hudManager.UpdateLife(missingHp, maxHp);
        //transform.localScale += Vector3.one * 1.1f;
    }

    private void Die() {
        SceneManager.LoadScene("MenuScene");
    }

    public void DealDamage(int amount) {
        if (IsBlocking)
            return;
        
        missingHp += amount;
        hudManager.UpdateLife(missingHp, maxHp);
        _animator.SetTrigger(Hit);
        
        if (missingHp >= maxHp) {
            Die();
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Finish")) {
            Die();
        }
    }
}
