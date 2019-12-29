using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 5.0f;
    public float attackRange = 1.0f;
    public LayerMask enemyLayer; 
    public Transform attackPoint;

    [HideInInspector] public Inventory inventory;

    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    private void Start() {
        inventory = GetComponent<Inventory>();
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 position = transform.position;
        
        position += new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime);

        if (position.y > -1f)
            position.y = -1f;

        if (position.y < -4.3f)
            position.y = -4.3f;

        transform.position = position;

        //GetComponent<SpriteRenderer>().flipX = horizontal < 0;

        if (Math.Abs(horizontal) > 0.1) {
            transform.rotation = Quaternion.Euler(0, (horizontal < 0 ? 1 : 0) * 180, 0);
        }

        _animator.SetFloat(Speed, Mathf.Abs(horizontal));

        if (Input.GetKeyDown(KeyCode.Space)) {
            Attack();
        }
    }

    private void Attack() {
        _animator.SetTrigger(AttackTrigger);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies) {
            Debug.Log("Hit " + enemy.name);
            
            enemy.GetComponent<Enemy>().Hit(10);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
