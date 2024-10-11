using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleDialog : MonoBehaviour
{
    public TextMeshProUGUI DialogText; // Change to public

    public void SetDialog(string text)
    {
        DialogText.text = text;
    }
}
