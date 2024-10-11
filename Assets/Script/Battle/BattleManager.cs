using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private PokemonBase _base; // Reference to the Pok�mon base data
    [SerializeField] private int level; // Level of the Pok�mon
    [SerializeField] private bool isPlayerUnit; // Indicates if it's the player's Pok�mon
    [SerializeField] private Image pokemonImage; // UI Image to display Pok�mon sprite
    [SerializeField] private BattleHud battleHud; // Reference to BattleHud

    public Pokemon Pokemon { get; private set; } // Reference to the current Pok�mon

    // This method sets up the Pok�mon instance and updates the UI
    public void SetUP()
    {
        // Initialize the Pok�mon instance with base data and level
        Pokemon = new Pokemon(_base, level);

        // Set the appropriate sprite based on whether it is a player unit or not
        if (isPlayerUnit)
        {
            pokemonImage.sprite = Pokemon.Base.BackSprite; // Player's Pok�mon (Back Sprite)
        }
        else
        {
            pokemonImage.sprite = Pokemon.Base.FrontSprite; // Enemy's Pok�mon (Front Sprite)
        }

        // Update the HUD with the new Pok�mon information
        battleHud.SetPokemon(Pokemon);
    }

    // Example method to simulate damage for testing purposes
    public void SimulateDamage(int damage)
    {
        battleHud.UpdateHealth(damage);
    }
}
