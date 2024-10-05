using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Animator transitionAnimation;
    public Button playButton;

    public void PlayGame() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
    public void QuitGame() {
        Application.Quit();
    }

    IEnumerator LoadLevel(int sceneIndex) {
        transitionAnimation.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }
}
