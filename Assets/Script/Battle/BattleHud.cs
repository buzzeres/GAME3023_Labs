using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText; // Reference to NameText in PlayerHUD
    [SerializeField] TextMeshProUGUI levelText; // Reference to LevelText in PlayerHUD
    [SerializeField] HealthBar healthBar;       // Reference to HealthBar in PlayerHUD

    private Pokemon currentPokemon;

    public void SetData(Pokemon pokemon)
    {
        currentPokemon = pokemon;
        nameText.text = pokemon.Base.Name;
        levelText.text = "Level: " + pokemon.Level;
        healthBar.SetHealth((float)pokemon.CurrentHealth / pokemon.MaxHp);
    }

    public void UpdateHP()
    {
       healthBar.SetHealth((float)currentPokemon.CurrentHealth / currentPokemon.MaxHp); // Update health instantly
    }
}
