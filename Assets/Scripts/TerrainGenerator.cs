using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    public GameObject terrain;
    
    public GameObject tree;
    public GameObject tree1;
    public GameObject monster;
    public GameObject snow;
    public GameObject chest;
    public GameObject win;

    private void Start() {
        for (int i = 0; i < 50; i++) {
            var position = new Vector2(i * 25, -2.5f);
            
            Instantiate(terrain, position, Quaternion.identity);
            
            var nextPosition = new Vector2(position.x + Random.Range(0, 12), Random.Range(-0.7f, 2.5f));
            var monsterObject = Instantiate(monster, nextPosition, Quaternion.identity);

            monsterObject.transform.localScale += Vector3.one * Random.Range(-2f, 2f);

            var monsterScript = monsterObject.GetComponent<Enemy>();
            monsterScript.hp = Random.Range(100, 200);
            monsterScript.damage = Random.Range(10, 25);
            monsterScript.givenExp = Random.Range(50, 200);
            monsterScript.speed = Random.Range(2f, 4f);

            for (int j = 0; j < 10; j++) {
                int percent = Random.Range(0, 100);

                if (percent < 10) {
                    nextPosition = new Vector2(position.x + Random.Range(0, 12), Random.Range(-4f, -1f));
                    var newObject = Instantiate(chest, nextPosition, Quaternion.identity);

                    newObject.transform.localScale += Vector3.one * Random.Range(-2f, 2f);

                    var objectScript = newObject.GetComponent<Object>();
                    objectScript.hp = Random.Range(5, 10);
                    objectScript.givenHp = Random.Range(0, 25);
                    objectScript.givenExp = Random.Range(5, 20);
                }
                
                else if (percent >= 10 && percent < 20) {
                    nextPosition = new Vector2(position.x + Random.Range(0, 12), Random.Range(-4f, -1f));
                    var newObject = Instantiate(snow, nextPosition, Quaternion.identity);

                    newObject.transform.localScale += Vector3.one * Random.Range(-2f, 2f);

                    var objectScript = newObject.GetComponent<Object>();
                    objectScript.hp = Random.Range(5, 10);
                    objectScript.givenExp = 5;
                }
                
                else if (percent >= 40 && percent < 50) {
                    nextPosition = new Vector2(position.x + Random.Range(0, 12), Random.Range(-0.7f, 2.5f));
                    var newObject = Instantiate(tree, nextPosition, Quaternion.identity);

                    newObject.transform.localScale += Vector3.one * Random.Range(-2f, 2f);
                }
                
                else if (percent >= 70 && percent < 80) {
                    nextPosition = new Vector2(position.x + Random.Range(0, 12), Random.Range(-0.7f, 2.5f));
                    var newObject = Instantiate(tree1, nextPosition, Quaternion.identity);

                    newObject.transform.localScale += Vector3.one * Random.Range(-2f, 2f);
                }
            }
        }
        
        var newPos = new Vector2(1150, -2.5f);
        Instantiate(win, newPos, Quaternion.identity);
    }
}
