using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {
    public List<TextMeshProUGUI> texts;

    private int _i;

    private void Start() {
        texts[_i].gameObject.SetActive(true);
    }
    
    // Update is called once per frame
    private void Update() {
        if (Input.anyKeyDown) {
            if (_i + 1 >= texts.Count) {
                SceneManager.LoadScene("GameplayScene");

                return;
            }
            
            texts[_i++].gameObject.SetActive(false);
            texts[_i].gameObject.SetActive(true);
        }
    }
}
