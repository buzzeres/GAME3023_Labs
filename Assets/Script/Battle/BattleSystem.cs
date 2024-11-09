using System;
using System.Collections;
using TMPro;
using UnityEngine;

public enum BattleState { START, PLAYERACTION, PLAYERMOVE, ENEMYMOVE, BUSY }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleManager playerUnit;
    [SerializeField] private BattleHud playerHud;
    [SerializeField] private BattleManager enemyUnit;
    [SerializeField] private BattleHud enemyHud;
    [SerializeField] private BattleDialog dialogBox;

    public event Action<bool> OnEndBattle;

    private BattleState state;
    private int currentAction;
    private int currentMove;

    public void StartBattle()
    {
        // Reset player and enemy units
        playerUnit.SetUp();
        playerHud.SetData(playerUnit.Pokemon);

        enemyUnit.SetUp();
        enemyHud.SetData(enemyUnit.Pokemon);

        // Start the battle setup coroutine
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        dialogBox.SetMoveName(playerUnit.Pokemon.Moves);

        yield return dialogBox.TypeDialog($"A wild {enemyUnit.Pokemon.Base.Name} appeared.");
        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PLAYERACTION;
        StartCoroutine(dialogBox.TypeDialog("Choose an action."));
        dialogBox.EnableActionSelector(true);
    }

    public void PlayerMove()
    {
        state = BattleState.PLAYERMOVE;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    private IEnumerator PerformPlayerMove()
    {
        state = BattleState.BUSY;
        var move = playerUnit.Pokemon.Moves[currentMove];

        yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.Name} used {move.Base.Name}.");
        playerUnit.PlayerAttackMove();

        yield return new WaitForSeconds(1f);
        enemyUnit.PlayerHitSequence();

        move.PP -= 1; 

        var damageDetails = enemyUnit.Pokemon.TakeDamage(move, playerUnit.Pokemon);
        enemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Pokemon.Base.Name} fainted.");
            enemyUnit.PlayerFaintAnimation();

            yield return new WaitForSeconds(2f);
            OnEndBattle?.Invoke(true);
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    private IEnumerator EnemyMove()
    {
        state = BattleState.ENEMYMOVE;
        var move = enemyUnit.Pokemon.GetRandomMove();

        yield return dialogBox.TypeDialog($"{enemyUnit.Pokemon.Base.Name} used {move.Base.Name}.");
        enemyUnit.PlayerAttackMove();

        yield return new WaitForSeconds(1f);
        playerUnit.PlayerHitSequence();

        move.PP -= 1;

        var damageDetails = playerUnit.Pokemon.TakeDamage(move, enemyUnit.Pokemon);
        playerHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.Name} fainted.");
            playerUnit.PlayerFaintAnimation();

            yield return new WaitForSeconds(2f);
            OnEndBattle?.Invoke(false);
        }
        else
        {
            PlayerMove(); // Direct method call instead of StartCoroutine
        }
    }

    private IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Type > 1f)
        {
            yield return dialogBox.TypeDialog("It's super effective!");
        }
        else if (damageDetails.Type < 1f)
        {
            yield return dialogBox.TypeDialog("It's not very effective.");
        }

        if (damageDetails.Critical > 1f)
        {
            yield return dialogBox.TypeDialog("A critical hit!");
        }
    }

    public void HandleUpdate()
    {
        if (state == BattleState.PLAYERACTION)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PLAYERMOVE)
        {
            HandleMoveSelection();
        }
    }

    private void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentAction < 1)
        {
            currentAction++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && currentAction > 0)
        {
            currentAction--;
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentAction == 0)
            {
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                // Add run logic if needed
            }
        }
    }

    private void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentMove < playerUnit.Pokemon.Moves.Count - 1)
        {
            currentMove++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && currentMove > 0)
        {
            currentMove--;
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Pokemon.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
}
