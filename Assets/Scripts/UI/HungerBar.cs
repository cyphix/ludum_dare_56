using System;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour {
    public Image digestionTimer;
    public Image stomachContentsIndicator;

    public void SetDigestionTimer(float time) {
        digestionTimer.rectTransform.localScale = new Vector3(
            Mathf.Clamp(time, 0.001f, 30) / 30f,
            1,
            1);
    }

    public void SetStomachContents(int content) {
        if (content <= 2) {
            stomachContentsIndicator.color = Color.red;
        } else if (content <= 6) {
            stomachContentsIndicator.color = Color.yellow;
        }
        else {
            stomachContentsIndicator.color = Color.green;
        }

        stomachContentsIndicator.rectTransform.localScale = new Vector3(
            content == 0 ? 0 : Mathf.Clamp(content, 0, 10f),
            1,
            1
        );
    }
}
