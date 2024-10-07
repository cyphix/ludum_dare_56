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

    public void SetStomachContents(int contents) {
        Debug.Log("Contents!!!");
        Debug.Log(contents);
        int max = 10;

        if (contents < 1) {
            starvationAnimation.SetBool("Starving", true);
        }
        else {
            starvationAnimation.SetBool("Starving", false);
        }
        
        stomachContentsElement.transform.localScale = new Vector3(
            contents / max,
            1,
            1
        );
    }
}
