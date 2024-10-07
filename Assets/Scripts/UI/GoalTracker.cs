using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalTracker : MonoBehaviour {
    public TextMeshProUGUI currentDigestedFoodText;
    public Image denominominatorImage;
    public TextMeshProUGUI targetDigestedFoodText;
    
    public void SetTracker(int current, int goal) {
        currentDigestedFoodText.SetText(current.ToString());
        targetDigestedFoodText.SetText(goal.ToString());

        if (current >= goal) {
            currentDigestedFoodText.color = Color.green;
            denominominatorImage.color = Color.green;
            targetDigestedFoodText.color = Color.green;
        }
        else {
            currentDigestedFoodText.color = Color.white;
            denominominatorImage.color = Color.white;
            targetDigestedFoodText.color = Color.white;
        }
    }
}
