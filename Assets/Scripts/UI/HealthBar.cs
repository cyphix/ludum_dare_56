using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    
    [SerializeField]
    private int playerMaxHealth = 8;
    [SerializeField]
    private int playerHealth = 6;
    
    public Transform player;
    public Image inlineBar;

    private void OnEnable() {
        RecalculateHealthbar();
    }

    private void Update() {
        transform.position = player.position + Vector3.up + Vector3.forward;
    }
    
    public int GetHealth() {
        return playerHealth;
    }
    

    public int GetMaxHealth() {
        return playerMaxHealth;
    }
    
    public void SetHealth(int health) {
        this.playerHealth = health;
        RecalculateHealthbar();
    }
    
    public void SetMaxHealth(int maxHealth) {
        this.playerMaxHealth = maxHealth;
        RecalculateHealthbar();
    }

    private void RecalculateHealthbar() {
        if (playerHealth >= 0 && playerMaxHealth > 1) {
            if (playerHealth <= playerMaxHealth / 3f) {
                inlineBar.color = Color.red;
            } else if (playerHealth <= playerMaxHealth * 2 / 3f) {
                inlineBar.color = Color.yellow;
            }
            else {
                inlineBar.color = Color.green;
            }

            inlineBar.transform.localScale = new Vector3(
                playerHealth * 1f / playerMaxHealth,
                1,
                1
            );
        }
    }
}
