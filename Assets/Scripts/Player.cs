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
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 position = transform.position;
        
        position += new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime);

        if (position.y > -1f)
            position.y = -1f;

        if (position.y < -4.3f)
            position.y = -4.3f;

        transform.position = position;
        
        GetComponent<SpriteRenderer>().flipX = horizontal < 0;
    }
}
