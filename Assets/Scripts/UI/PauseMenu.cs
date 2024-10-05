using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public Animator levelTransition;
    public Animator pauseMenuAnimator;

    private bool active = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (active) {
                pauseMenuAnimator.SetTrigger("End");
            }
            else {
                pauseMenuAnimator.SetTrigger("Start");
            }

            active = !active;
        }
    }

    public void QuitToMenu() {
        StartCoroutine(LoadLevel(0));
    }
    
    public void QuitGame() {
        Application.Quit();
    }
    
    IEnumerator LoadLevel(int sceneIndex) {
        levelTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }
}
