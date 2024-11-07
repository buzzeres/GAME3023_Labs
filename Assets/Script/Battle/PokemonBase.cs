using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
// Pokémon base class definition
public class PokemonBase : ScriptableObject
{
    [SerializeField] private new string name; // Pokémon's name, private field

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
        Electric,
        Grass,
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
        Steal,
        Fairy
    }

    public class TypeChart
    {
        static float[][] chart =
        {
            //                     NOR   FIR     WAT     ELE     GRA     ICE     FIG     POI     GRO    FLY     PSY     BUG     ROC      GHO     DRA     DAR    STE     FAI
            /* NOR */ new float[] {1f,   1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,    1f,     1f,     1f,     0.5f,    0f,     1f,     1f,    0.5f,   1f,},
            /* FIR */ new float[] {1f,   0.5f,   0.5f,   1f,     2f,     2f,     1f,     1f,     1f,    1f,     1f,     2f,     0.5f,    1f,     0.5f,   1f,    2f,     1f,},
            /* WAT */ new float[] {1f,   2f,     0.5f,   1f,     0.5f,   1f,     1f,     1f,     2f,    1f,     1f,     1f,     2f,      1f,     0.5f,   1f,    1f,     1f,},
            /* ELE */ new float[] {1f,   1f,     2f,     0.5f,   0.5f,   1f,     1f,     1f,     0f,    2f,     1f,     1f,     1f,      1f,     0.5f,   1f,    1f,     1f,},
            /* GRA */ new float[] {1f,   0.5f,   2f,     1f,     0.5f,   1f,     1f,     0.5f,   2f,    0.5f,   1f,     0.5f,   2f,      1f,     0.5f,   1f,    1f,     1f,},
            /* ICE */ new float[] {1f,   0.5f,   0.5f,   1f,     2f,     0.5f,   1f,     1f,     2f,    2f,     1f,     1f,     1f,      1f,     2f,     1f,    1f,     1f,},
            /* FIG */ new float[] {2f,   1f,     1f,     1f,     1f,     2f,     1f,     0.5f,   1f,    0.5f,   0.5f,   0.5f,   2f,      0f,     1f,     2f,    2f,     0.5f,},
            /* POI */ new float[] {1f,   1f,     1f,     1f,     2f,     1f,     1f,     0.5f,   0.5f,  1f,     1f,     1f,     0.5f,    0.5f,   1f,     1f,    0f,     2f,},
            /* GRO */ new float[] {1f,   2f,     1f,     2f,     0.5f,   1f,     1f,     2f,     1f,    0f,     1f,     0.5f,   2f,      1f,     1f,     1f,    2f,     1f,},
            /* FLY */ new float[] {1f,   1f,     1f,     0.5f,   2f,     1f,     2f,     1f,     1f,    1f,     1f,     2f,     0.5f,    1f,     1f,     1f,    0.5f,   1f,},
            /* PSY */ new float[] {1f,   1f,     1f,     1f,     1f,     1f,     2f,     2f,     1f,    1f,     0.5f,   1f,     1f,      1f,     1f,     0f,    0.5f,   1f,},
            /* BUG */ new float[] {1f,   0.5f,   1f,     1f,     2f,     1f,     0.5f,   0.5f,   1f,    0.5f,   2f,     1f,     1f,      0.5f,   1f,     2f,    0.5f,   0.5f,},
            /* ROC */ new float[] {1f,   2f,     1f,     1f,     2f,     1f,     0.5f,   1f,     0.5f,  2f,     1f,     2f,     1f,      1f,     1f,     1f,    0.5f,   1f,},
            /* GHO */ new float[] {0f,   1f,     1f,     1f,     1f,     1f,     0f,     1f,     1f,    1f,     2f,     1f,     1f,      2f,     1f,     0.5f,  1f,     1f,},
            /* DRA */ new float[] {1f,   1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,    1f,     1f,     1f,     1f,      1f,     2f,     1f,    0.5f,   0f,},
            /* DAR */ new float[] {1f,   1f,     1f,     1f,     1f,     1f,     0.5f,   1f,     1f,    1f,     2f,     1f,     1f,      2f,     1f,     0.5f,  1f,     0.5f,},
            /* STE */ new float[] {1f,   0.5f,   0.5f,   0.5f,   1f,     2f ,    1f,     1f,     1f,    1f,     1f,     1f,     2f,      1f,     1f,     1f,    0.5f,   2f,},
            /* FAI */ new float[] {1f,   0.5f,   1f,     1f,     1f,     1f,     2f,     0.5f,   1f,    1f,     1f,     1f,     1f,      1f,     2f,     2f,    0.5f,   1f,},
        };

        public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
        {

            // If the attack type or defense type is None, return 1
            if (attackType == PokemonType.None || defenseType == PokemonType.None)
                return 1;

            int Col = (int)defenseType - 1;
            int Row = (int)attackType - 1;

            return chart[Row][Col];
        }
    }


}
