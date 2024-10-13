using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PokemonBase;

[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] string moveName; // Use 'new' to hide the inherited member

    [TextArea]
    [SerializeField] string description; // Move description
    [SerializeField] PokemonType type; // Move type (e.g., Fire, Water)
    [SerializeField] int power; // Power of the move
    [SerializeField] int accuracy; // Accuracy of the move
    [SerializeField] int pp; // Power Points

    public string Name
    {
        get { return moveName; }
    }

    public string Description
    {
        get { return description; }
    }

    public PokemonType Type
    {
        get { return type; }
    }

    public int Power
    {
        get { return power; }
    }

    public int Accuracy
    {
        get { return accuracy; }
    }

    public int PP
    {
        get { return pp; }
    }
}
