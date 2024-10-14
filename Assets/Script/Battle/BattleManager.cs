using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;
    [SerializeField] Image pokemonImage;
    [SerializeField] BattleHud battleHud;

    public Pokemon Pokemon { get; private set; }

    public void SetUp()
    {
        Pokemon = new Pokemon(_base, level);

        if (isPlayerUnit)
        {
            GetComponent<Image>().sprite = Pokemon.Base.BackSprite;
        }
        else
        {
            GetComponent<Image>().sprite = Pokemon.Base.FrontSprite;
        }
    }
}
