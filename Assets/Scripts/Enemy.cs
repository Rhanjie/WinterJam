using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int hp = 100;

    private Animator _animator;
    private static readonly int Hit1 = Animator.StringToHash("Hit");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    private void Start() {
        _animator = GetComponent<Animator>();
    }

    public void Hit(int damage) {
        hp -= damage;

        if (hp <= 0) {
            _animator.SetBool(IsDead, true);

            return;
        }
        
        _animator.SetTrigger(Hit1);
    }
}
