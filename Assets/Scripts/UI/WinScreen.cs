using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public Animator winScreenAnimator;
    public TextMeshProUGUI winText;
    
    public void SetWinScreen(string reason) {
        winText.SetText("You win!! Because " + reason);
        winScreenAnimator.SetTrigger("Start");
    }
}
