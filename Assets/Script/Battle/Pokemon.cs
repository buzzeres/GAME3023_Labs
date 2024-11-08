using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PokemonBase;


public class Pokemon
{
    public PokemonBase Base { get; private set; } 
    public int Level { get; private set; } 

    public int CurrentHealth { get; private set; } 
    public List<Move> Moves { get; internal set; }

    public int HP { get; set; }

    public Pokemon(PokemonBase pBase, int pLevel)
    {
        Base = pBase;
        Level = pLevel;
        CurrentHealth = MaxHp; 

        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
                Moves.Add(new Move(move.Base));
            if (Moves.Count >= 4)
                break;
        }
    }

   
    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; } // Calculate Attack by multiplying the base attack with
                                                                           // the level and dividing by 100 to get the final value
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; } 
    }

    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; } 
    }

    public int SpDefense
    {
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5; } 
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; } 
    }

    public int MaxHp
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; } 
    }

    public DamageDetails TakeDamage(Move move, Pokemon attacker)
    {
        float critical = 1f;
        float  type = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) * TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2);
        if (Random.value * 100f <= 6.25f) // 6.25% chance of critical hit 
        {
            critical = 2f;
        }

        var dameageDetails = new DamageDetails()
        {
            Type = type,
            Critical = critical,
            Fainted = false
        };

        float modifiers = Random.Range(0.85f, 1f) * type * critical; 
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        CurrentHealth -= damage; 
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            dameageDetails.Fainted = true;
        }

        return dameageDetails; 
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }

}


public class DamageDetails
{ 

    public bool Fainted { get; set; }
    public float Critical { get; set; }
    public float Type { get; set; }
}