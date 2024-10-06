using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public Animator levelTransition;

    public void QuitToMenu() {
        StartCoroutine(LoadLevel(0));
    }
    
    public void QuitGame() {
        Application.Quit();
    }
    
    IEnumerator LoadLevel(int sceneIndex) {
        levelTransition.SetTrigger("Start");
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }
}
