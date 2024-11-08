using System;
using UnityEngine;

public enum GameState { FreeRoam, Battle }

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private Camera mainCamera;

    private GameState state;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        playerMovement.isEncountered += () => StartBattle(CollisionType.Encounter);
        battleSystem.OnEndBattle += EndBattle;
    }

    public void StartBattle(CollisionType type)
    {
        if (type == CollisionType.Encounter)
        {
            state = GameState.Battle;
            battleSystem.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);

            playerMovement.DisableMovement(); // Disable player movement when battle starts
            battleSystem.StartBattle(); // This will reset the battle units and start the battle
        }
    }

    private void EndBattle(bool playerWin)
    {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        playerMovement.EnableMovement(); // Enable player movement when battle ends
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
}
