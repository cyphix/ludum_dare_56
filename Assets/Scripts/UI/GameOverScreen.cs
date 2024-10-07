using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour {
    public Animator gameOverScreenAnimator;
    public TextMeshProUGUI gameOverText;
    
    public void SetGameOver(string reason) {
        gameOverText.SetText("You died...\nCause of death: " + reason);
        gameOverScreenAnimator.SetTrigger("Start");
    }
}
