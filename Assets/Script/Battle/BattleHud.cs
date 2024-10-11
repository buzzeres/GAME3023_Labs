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

    // Method to update the HUD with Pokémon's details
    private void UpdateHud()
    {
        nameText.text = currentPokemon.Base.Name; // Access Name from Base
        levelText.text = "Level: " + currentPokemon.Level; // Current Level
        healthBar.SetHealth((float)currentPokemon.CurrentHealth / currentPokemon.Base.MaxHealth); // Access MaxHealth from Base
    }

    // Optional: Update the health when damage is taken
    public void UpdateHealth(int damage)
    {
        currentPokemon.TakeDamage(damage);
        healthBar.SetHealth((float)currentPokemon.CurrentHealth / currentPokemon.Base.MaxHealth); // Access MaxHealth from Base
    }
}
