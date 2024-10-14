using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
// Pokémon base class definition
public class PokemonBase : ScriptableObject
{
    [SerializeField] private string name; // Pokémon's name, private field

    [TextArea]
    [SerializeField] private string description; // Description of the Pokémon

    [SerializeField] private Sprite frontSprite; // Sprite displayed when facing the player
    [SerializeField] private Sprite backSprite; // Sprite displayed when facing away from the player

    [SerializeField] private PokemonType type1; // Primary type of the Pokémon
    [SerializeField] private PokemonType type2; // Secondary type of the Pokémon

    [SerializeField] private int maxHp; // Maximum health points
    [SerializeField] private int attack; // Attack points
    [SerializeField] private int defense; // Defense points
    [SerializeField] private int spAttack; // Special Attack points
    [SerializeField] private int spDefense; // Special Defense points
    [SerializeField] private int speed; // Speed points

    // List of moves that the Pokémon can learn
    [SerializeField] private List<LearnableMove> learnableMoves;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public PokemonType Type1
    {
        get { return type1; }
    }

    public PokemonType Type2
    {
        get { return type2; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int SpDefense
    {
        get { return spDefense; }
    }

    public int Speed
    {
        get { return speed; }
    }
    public List<LearnableMove> LearnableMoves => learnableMoves; // Read-only property for learnable moves

    // Class to define learnable moves
    [System.Serializable]
    public class LearnableMove
    {
        [SerializeField] private MoveBase moveBase; 
        [SerializeField] private int level; // The level at which the move can be learned

        public MoveBase Base => moveBase; // Read-only property for the move
        public int Level => level; // Read-only property for the level
    }

    // Enumeration Pokemon Type
    public enum PokemonType
    {
        None,
        Normal,
        Fire,
        Water,
        Grass,
        Electric,
        Ice,
        Fighting,
        Poison,
        Ground,
        Flying,
        Psychic,
        Bug,
        Rock,
        Ghost,
        Dragon,
        Dark,
        Fairy
    }
}
