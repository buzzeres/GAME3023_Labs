using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleDialog : MonoBehaviour
{
    [SerializeField] private int lettersPerSecond = 30;
    public TextMeshProUGUI DialogText;

    [SerializeField] public GameObject actionSelector;
    [SerializeField] public GameObject moveSelector;
    [SerializeField] public GameObject moveDetails;

    [SerializeField] public List<TextMeshProUGUI> actionTexts;
    [SerializeField] public List<TextMeshProUGUI> moveTexts;

    [SerializeField] public TextMeshProUGUI ppText;
    [SerializeField] public TextMeshProUGUI typeText;

    [SerializeField] Color LightUpWord;

    public void SetDialog(string text)
    {
        DialogText.text = text;
    }

    public IEnumerator TypeDialog(string text)
    {
        DialogText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            DialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    public void EnableDialogText(bool enable)
    {
        DialogText.enabled = enable;
    }

    public void EnableActionSelector(bool enable)
    {
        actionSelector.SetActive(enable);
    }

    public void EnableMoveSelector(bool enable)
    {
        moveSelector.SetActive(enable);
        moveDetails.SetActive(enable);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; i++)
        {
            if (i == selectedAction)
            {
                actionTexts[i].color = LightUpWord;
                Debug.Log($"Highlighting action: {actionTexts[i].text}");
            }
            else
            {
                actionTexts[i].color = Color.black;
                Debug.Log($"Resetting action: {actionTexts[i].text}");
            }
        }
    }

    public void SetMoveName(List<Move> moves)
    {
        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i < moves.Count)
                moveTexts[i].text = moves[i].Base.name;
            else
            {
                moveTexts[i].text = "-";
            }
        }
    }

    public void UpdateMoveSelection(int selectedMove, Move move)
    {
        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i == selectedMove)
            {
                moveTexts[i].color = LightUpWord;
                Debug.Log($"Highlighting move: {moveTexts[i].text}");
            }
            else
            {
                moveTexts[i].color = Color.black;
                Debug.Log($"Resetting move: {moveTexts[i].text}");
            }

            ppText.text = $"PP {move.PP}/{move.Base.PP}";
            typeText.text = move.Base.Type.ToString();
        }
    }
}
