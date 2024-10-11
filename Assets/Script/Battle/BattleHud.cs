using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText; // Reference to NameText in PlayerHUD
    [SerializeField] private TextMeshProUGUI levelText; // Reference to LevelText in PlayerHUD
    [SerializeField] private HealthBar healthBar;       // Reference to HealthBar in PlayerHUD

    private Pokemon currentPokemon; // Reference to the current Pokémon

    // Method to set the Pokémon in the BattleHud
    public void SetPokemon(Pokemon pokemon)
    {
        currentPokemon = pokemon;
        UpdateHud();
    }

    private void UpdateHud()
    {
        if (currentPokemon != null)
        {
            nameText.text = currentPokemon.Base.Name; // Access Name from Base
            levelText.text = "Level: " + currentPokemon.Level; // Current Level

            // Ensure the health value is valid
            if (currentPokemon.CurrentHealth >= 0 && currentPokemon.Base.MaxHealth > 0)
            {
                float healthScale = (float)currentPokemon.CurrentHealth / currentPokemon.Base.MaxHealth;
                healthBar.SetHealth(healthScale); // Set health value to the health bar
            }
            else
            {
                Debug.LogWarning("Invalid health values: CurrentHealth: " + currentPokemon.CurrentHealth + ", MaxHealth: " + currentPokemon.Base.MaxHealth);
            }
        }
    }


    // Optional: Update the health when damage is taken
    public void UpdateHealth(int damage)
    {
        currentPokemon.TakeDamage(damage);
        healthBar.SetHealth((float)currentPokemon.CurrentHealth / currentPokemon.Base.MaxHealth); // Access MaxHealth from Base
    }
}
