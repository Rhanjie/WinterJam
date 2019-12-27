using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 5.0f;

    [HideInInspector] public Inventory inventory;

    private void Start() {
        inventory = GetComponent<Inventory>();
    }

    private void Update() {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        
        transform.position += new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime);

        GetComponent<SpriteRenderer>().flipX = horizontal < 0;
    }
}
