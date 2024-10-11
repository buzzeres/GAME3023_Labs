using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleManager playerUnit; // Reference to the player's battle manager
    [SerializeField] private BattleHud playerHud;      // Reference to the player's HUD
    [SerializeField] private BattleManager enemyUnit;   // Reference to the enemy's battle manager
    [SerializeField] private BattleHud enemyHud;       // Reference to the enemy's HUD

    private void Start()
    {
        SetupBattle();
    }

    private void SetupBattle()
    {
        // Check if player unit is assigned
        if (playerUnit == null)
        {
            Debug.LogError("PlayerUnit is not assigned in BattleSystem!");
            return;
        }

        // Check if enemy unit is assigned
        if (enemyUnit == null)
        {
            Debug.LogError("EnemyUnit is not assigned in BattleSystem!");
            return;
        }

        // Check if player HUD is assigned
        if (playerHud == null)
        {
            Debug.LogError("PlayerHud is not assigned in BattleSystem!");
            return;
        }

        // Check if enemy HUD is assigned
        if (enemyHud == null)
        {
            Debug.LogError("EnemyHud is not assigned in BattleSystem!");
            return;
        }

        // Initialize the player's unit
        playerUnit.SetUp();
        playerHud.SetPokemon(playerUnit.Pokemon); // Update the HUD with the player's Pokémon

        // Initialize the enemy unit
        enemyUnit.SetUp();
        enemyHud.SetPokemon(enemyUnit.Pokemon); // Update the HUD with the enemy's Pokémon
    }

}
