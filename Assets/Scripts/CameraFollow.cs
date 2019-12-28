using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject target;

    public float smoothTimeX;

    private Vector2 _velocity;
    
    private void FixedUpdate() {
        Vector3 position = transform.position;
        float targetPosX = target.transform.position.x;
        
        float newPositionX = Mathf.SmoothDamp(position.x, targetPosX, ref _velocity.x, smoothTimeX);
        transform.position = new Vector3(newPositionX, position.y, position.z);
    }
}
