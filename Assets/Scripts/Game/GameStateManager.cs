using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateManager : MonoBehaviour {
    public Animator pauseMenuAnimator;
    public Animator gameOverMenuAnimator;
    public GameObject gameOverMenu;
    
    private ISActions actions;
    private InputAction pauseAction;
    private GameState gameState = GameState.PLAYING;

    private void Awake() {
        actions = new ISActions();
        pauseAction = actions.UI.Pause;
    }

    private void OnEnable() {
        actions.Enable();
    }

    private void OnDisable() {
        actions.Disable();
    }

    private void Update() {
        if (pauseAction.triggered) {
            if (gameState == GameState.PLAYING) {
                Pause();
            } else if (gameState == GameState.PAUSED) {
                Unpause();
            }
        }
    }

    public void Pause() {
        gameState = GameState.PAUSED;
        pauseMenuAnimator.SetTrigger("Start");
        Time.timeScale = 0;
    }

    public void Unpause() {
        gameState = GameState.PLAYING;
        pauseMenuAnimator.SetTrigger("End");
        Time.timeScale = 1;
    }

    public void SetGameOver() {
        gameState = GameState.GAMEOVER;
        Time.timeScale = 0;
        gameOverMenuAnimator.SetTrigger("Start");
    }

    public void CheckPlayerHealth(int health) {
        if (health <= 0) {
            SetGameOver();
        }
    }
}
