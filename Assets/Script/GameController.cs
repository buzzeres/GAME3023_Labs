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

            battleSystem.StartBattle();
        }
    }

    private void EndBattle(bool playerWin)
    {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
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
