using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitable {
    public int hp = 100;
    public int givenExp = 125;
    public int damage = 20;
    public float attackTime = 5;
    public float speed = 2f;

    public Transform sortPoint;

    private Animator _animator;
    private Transform _player;
    private float _timer = 0;
    private bool _isAttacking = false;
    
    private static readonly int Hit1 = Animator.StringToHash("Hit");
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Start() {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform.Find("SortingPoint");
    }

    private void Update() {
        if (_isAttacking) {
            _timer += Time.deltaTime;

            if (_timer >= attackTime) {
                _isAttacking = false;
                _timer = 0;
            }
        }

        var distance = Vector2.Distance(sortPoint.position, _player.transform.position);

        GoToPlayer();
    }

    private void GoToPlayer() {
        var distance = Vector2.Distance(sortPoint.position, _player.transform.position);

        if (distance > 10)
            return;
        
        var position = sortPoint.position;
        var playerPosition = _player.transform.position;

        //TODO: No time to refactor code
        if (!_isAttacking && distance > 1) {
            if (position.x > playerPosition.x) {
                transform.position -= Vector3.right * (speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            else {
                transform.position += Vector3.right * (speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);

            }
        }
        
        else {
            if (position.x > playerPosition.x) {
                transform.position += Vector3.right * (speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            else {
                transform.position -= Vector3.right * (speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);

            }
        }
        
        if (position.y > playerPosition.y) {
            transform.position -= Vector3.up * (speed * Time.deltaTime);
        }

        else transform.position += Vector3.up * (speed * Time.deltaTime);
    }

    public bool Hit(int damage) {
        hp -= damage;

        if (hp <= 0) {
            _animator.SetBool(IsDead, true);
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this);

            return true;
        }
        
        _animator.SetTrigger(Hit1);
        return false;
    }
    
    public int GetExp() {
        return givenExp;
    }
    
    public int GetHp() {
        return 0;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (_isAttacking)
            return;
        
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        _isAttacking = true;
        _animator.SetTrigger(Attack);
        player.DealDamage(damage);
        
    }
}
