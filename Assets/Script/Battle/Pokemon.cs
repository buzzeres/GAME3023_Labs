using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PokemonBase
{
    public string Name;               // Name of the Pokémon
    public int MaxHealth;             // Maximum health of the Pokémon (must be greater than 0)
    public Sprite BackSprite;         // Sprite when facing away
    public Sprite FrontSprite;        // Sprite when facing towards the player
}


public class Pokemon
{
    public PokemonBase Base { get; private set; }
    public int Level { get; private set; }
    public int CurrentHealth { get; private set; }

    // Constructor to initialize the Pokémon
    public Pokemon(PokemonBase pokemonBase, int level)
    {
        Base = pokemonBase;
        Level = level;
        CurrentHealth = Base.MaxHealth; // Starts with full health
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0) CurrentHealth = 0; // Prevent negative health
    }

    // Method to heal the Pokémon
    public void Heal(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > Base.MaxHealth) CurrentHealth = Base.MaxHealth; // Prevent exceeding max health
    }

    // Method to check if the Pokémon is fainted
    public bool IsFainted()
    {
        return CurrentHealth <= 0;
    }
}
