using System.Collections;
using UnityEngine;

public enum GameState { FreeRoam, Battle }

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private BattleSystem battleSystem;

    private GameState state;

    private void Start()
    {
        SetGameState(GameState.FreeRoam); // Start with FreeRoam state
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerMovement.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
    }

    // Public method to change the game state
    public void SetGameState(GameState newState)
    {
        state = newState;

        if (state == GameState.FreeRoam)
        {
            EnterFreeRoam();
        }
        else if (state == GameState.Battle)
        {
            EnterBattle();
        }
    }

    private void EnterFreeRoam()
    {
        playerMovement.enabled = true; // Enable player controls
        battleSystem.gameObject.SetActive(false); // Disable battle system
    }

    private void EnterBattle()
    {
        playerMovement.enabled = false; // Disable player controls
        battleSystem.gameObject.SetActive(true); // Enable battle system
        battleSystem.StartBattle(); // Start battle logic
    }
}
