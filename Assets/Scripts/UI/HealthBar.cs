using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Transform player;
    public Image section;
    
    private List<Image> sections = new();
    private int playerMaxHealth = 8;
    private int playerHealth = 6;

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
        for (int i = 0; i < sections.Count; i++) {
            Destroy(sections[i].gameObject);
        }

        sections.Clear();

        if (playerHealth >= 0 && playerMaxHealth > 1) {
            for (int i = 0; i < playerHealth; i++) {
                var newSection = Instantiate(section, transform, false);
                var newScale = (newSection.transform.localScale.x / playerMaxHealth);

                newSection.transform.position +=
                    Vector3.right * newScale * 3 * (i - playerMaxHealth / 2f + 0.5f);

                if (playerHealth <= playerMaxHealth / 3f) {
                    newSection.color = Color.red;
                } else if (playerHealth <= playerMaxHealth * 2 / 3f) {
                    newSection.color = Color.yellow;
                }

                newSection.transform.localScale = new Vector3(
                    newScale,
                    newSection.transform.localScale.y,
                    newSection.transform.localScale.z
                );

                sections.Add(newSection);
            }
        }
    }
}
