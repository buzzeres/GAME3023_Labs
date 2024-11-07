using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum BattleState { START, PLAYERACTION, PLAYERMOVE, ENEMYMOVE, BUSY }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleManager playerUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleManager enemyUnit;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialog dialogBox;

    BattleState state;
    int currentAction;
    int currentMove;

    private void Start()
    {
        StartCoroutine(SetupBattle());
    }

    // Start is called before the first frame update
    private IEnumerator SetupBattle()
    {
        playerUnit.SetUp();
        playerHud.SetData(playerUnit.Pokemon);

        enemyUnit.SetUp();
        enemyHud.SetData(enemyUnit.Pokemon);

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

    void PlayerMove()
    {
        state = BattleState.PLAYERMOVE;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.BUSY;

        var move = playerUnit.Pokemon.Moves[currentMove];
        Debug.Log($"{playerUnit.Pokemon.Base.Name} used {move.Base.Name}");
        yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.Name} used {move.Base.Name}.");

        playerUnit.PlayerAttackMove();
        yield return new WaitForSeconds(1f);

        enemyUnit.PlayerHitSequence();
        var damageDetails = enemyUnit.Pokemon.TakeDamage(move, playerUnit.Pokemon);
        enemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);


        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Pokemon.Base.Name} fainted.");
            enemyUnit.PlayerFaintAnimation();
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.ENEMYMOVE;

        var move = enemyUnit.Pokemon.GetRandomMove();
        Debug.Log($"{enemyUnit.Pokemon.Base.Name} used {move.Base.Name}");
        yield return dialogBox.TypeDialog($"{enemyUnit.Pokemon.Base.Name} used {move.Base.Name}.");

        enemyUnit.PlayerAttackMove();
        yield return new WaitForSeconds(1f);

        playerUnit.PlayerHitSequence();
        var damageDetails = playerUnit.Pokemon.TakeDamage(move, enemyUnit.Pokemon);
        playerHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.Name} fainted.");
            playerUnit.PlayerFaintAnimation();
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerable ShowDamageDetails(DamageDetails damageDetails)
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

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 1)
            {
                ++currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
            {
                --currentAction;
            }
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
                // Run logic if needed
            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.Pokemon.Moves.Count - 1)
            {
                currentMove++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 0)
            {
                currentMove--;
            }
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
