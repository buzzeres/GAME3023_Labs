using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;
    [SerializeField] Image pokemonImage;
    [SerializeField] BattleHud battleHud;

    private Color originalColor; // Store the original color
    private Vector3 targetPosition;

    public Pokemon Pokemon { get; private set; }

    public void SetUp()
    {
        // Set up Pokémon data
        Pokemon = new Pokemon(_base, level);

        // Set sprite based on whether it's player or enemy Pokémon
        pokemonImage.sprite = isPlayerUnit ? Pokemon.Base.BackSprite : Pokemon.Base.FrontSprite;

        // Store the original color of the Pokémon image
        originalColor = pokemonImage.color;

        // Set Pokémon off-screen for entrance animation
        Vector3 startPosition = isPlayerUnit ? new Vector3(-1000, -94, 0) : new Vector3(1000, 141, 0);
        targetPosition = isPlayerUnit ? new Vector3(-500, -94, 0) : new Vector3(400, 141, 0);

        // Set initial position
        pokemonImage.transform.localPosition = startPosition;

        // Start the entrance animation
        StartCoroutine(MoveToPosition(pokemonImage.transform, targetPosition, 1f));
    }

    // Coroutine to move the Pokémon position
    private IEnumerator MoveToPosition(Transform transform, Vector3 destination, float duration)
    {
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, destination, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the final position
        transform.localPosition = destination;
    }

    public void PlayerAttackMove()
    {
        StartCoroutine(AttackMoveSequence());
    }

    private IEnumerator AttackMoveSequence()
    {
        float directionMultiplier = isPlayerUnit ? 1f : -1f;
        Vector3 attackPosition = targetPosition + new Vector3(50f * directionMultiplier, 0, 0);

        // Move forward to attack
        yield return StartCoroutine(MoveToPosition(pokemonImage.transform, attackPosition, 0.25f));

        // Move back to the original position
        yield return StartCoroutine(MoveToPosition(pokemonImage.transform, targetPosition, 0.25f));
    }

    public void PlayerHitSequence()
    {
        StartCoroutine(HitEffectSequence());
    }

    private IEnumerator HitEffectSequence()
    {
        // Flash to red to simulate being hit
        pokemonImage.color = Color.red;

        // Wait for a brief moment
        yield return new WaitForSeconds(0.1f);

        // Return to original color
        pokemonImage.color = originalColor;
    }

    public void PlayerFaintAnimation()
    {
        StartCoroutine(FaintEffectSequence());
    }

    private IEnumerator FaintEffectSequence()
    {
        float duration = 1f; // Duration of the faint animation
        Vector3 startPosition = pokemonImage.transform.localPosition;
        Vector3 endPosition = startPosition + new Vector3(0, -100, 0); // Move down by 100 units
        Color startColor = pokemonImage.color;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Interpolate position and fade out color
            pokemonImage.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            pokemonImage.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, 0, elapsedTime / duration));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and color are set exactly
        pokemonImage.transform.localPosition = endPosition;
        pokemonImage.color = new Color(startColor.r, startColor.g, startColor.b, 0); // Fully transparent
    }
}
