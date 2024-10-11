using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleManager playerUnit; // Reference to the player's battle manager
    [SerializeField] private BattleHud playerHud;      // Reference to the player's HUD
    [SerializeField] private BattleManager enemyUnit;   // Reference to the enemy's battle manager
    [SerializeField] private BattleHud enemyHud;       // Reference to the enemy's HUD
    [SerializeField] private BattleDialog dialogBox;   // Reference to the dialog box

    private void Start()
    {
        SetupBattle();
    }
    private void SetupBattle()
    {
        if (playerUnit == null)
        {
            Debug.LogError("PlayerUnit is not assigned in BattleSystem!");
            return;
        }

        if (enemyUnit == null)
        {
            Debug.LogError("EnemyUnit is not assigned in BattleSystem!");
            return;
        }

        if (playerHud == null)
        {
            Debug.LogError("PlayerHud is not assigned in BattleSystem!");
            return;
        }

        if (enemyHud == null)
        {
            Debug.LogError("EnemyHud is not assigned in BattleSystem!");
            return;
        }

        if (dialogBox == null)
        {
            Debug.LogError("DialogBox is not assigned in BattleSystem!");
            return;
        }

        // Initialize the player's unit
        playerUnit.SetUp();
        playerHud.SetPokemon(playerUnit.Pokemon); // Update the HUD with the player's Pokémon

        // Initialize the enemy unit
        enemyUnit.SetUp();
        enemyHud.SetPokemon(enemyUnit.Pokemon); // Update the HUD with the enemy's Pokémon

        dialogBox.SetDialog($"A wild {playerUnit.Pokemon.Base.Name} appeared.");
    }


}
