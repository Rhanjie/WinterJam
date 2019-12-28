using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSorter : MonoBehaviour {
    private void FixedUpdate() {
        var objects = GameObject.FindGameObjectsWithTag("Sortable");

        var i = objects.Length;
        foreach(var sortingObject in objects.OrderBy(sortingObject => sortingObject.transform.position.y)) {
            sortingObject.GetComponentInParent<SpriteRenderer>().sortingOrder = i--;
        }
    }
}
