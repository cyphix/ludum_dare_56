using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour {
    public Transform digestionTimerElement;
    public Image stomachContentsElement;
    public Animator starvationAnimation;
    
    public void SetDigestionTimer(float currentTime, float totalTime) {
        digestionTimerElement.localScale = new Vector3(
            totalTime == 0 ? 0 : currentTime / totalTime,
            1,
            1
        );
    }

    public void SetStomachContents(int contents, int maxContents) {
        if (contents < 1) {
            starvationAnimation.SetBool("Starving", true);
        }
        else {
            starvationAnimation.SetBool("Starving", false);
        }
        
        stomachContentsElement.transform.localScale = new Vector3(
            Mathf.Max(contents, 0) * 1f / maxContents,
            1,
            1
        );
    }
}
