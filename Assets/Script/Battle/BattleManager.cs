using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private PokemonBase _base;
    [SerializeField] private int level;
    [SerializeField] private bool isPlayerUnit;
    [SerializeField] private Image pokemonImage;
    [SerializeField] private BattleHud battleHud;

    private Color originalColor; // Store the original color
    private Vector3 targetPosition;

    public Pokemon Pokemon { get; private set; }

    private void Awake()
    {
        // Capture the original color once during initialization
        originalColor = pokemonImage.color;
    }

    public void SetUp()
    {
        Pokemon = new Pokemon(_base, level);
        pokemonImage.sprite = isPlayerUnit ? Pokemon.Base.BackSprite : Pokemon.Base.FrontSprite;

        // Reset to the original color with full opacity
        pokemonImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        Vector3 startPosition = isPlayerUnit ? new Vector3(-1000, -94, 0) : new Vector3(1000, 141, 0);
        targetPosition = isPlayerUnit ? new Vector3(-500, -94, 0) : new Vector3(400, 141, 0);

        pokemonImage.transform.localPosition = startPosition;
        StartCoroutine(MoveToPosition(pokemonImage.transform, targetPosition, 1f));
    }

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

        transform.localPosition = destination;
    }

    public void PlayerAttackMove() => StartCoroutine(AttackMoveSequence());

    private IEnumerator AttackMoveSequence()
    {
        float directionMultiplier = isPlayerUnit ? 1f : -1f;
        Vector3 attackPosition = targetPosition + new Vector3(50f * directionMultiplier, 0, 0);
        yield return StartCoroutine(MoveToPosition(pokemonImage.transform, attackPosition, 0.25f));
        yield return StartCoroutine(MoveToPosition(pokemonImage.transform, targetPosition, 0.25f));
    }

    public void PlayerHitSequence() => StartCoroutine(HitEffectSequence());

    private IEnumerator HitEffectSequence()
    {
        pokemonImage.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        pokemonImage.color = originalColor;
    }

    public void PlayerFaintAnimation() => StartCoroutine(FaintEffectSequence());

    private IEnumerator FaintEffectSequence()
    {
        float duration = 1f;
        Vector3 startPosition = pokemonImage.transform.localPosition;
        Vector3 endPosition = startPosition + new Vector3(0, -100, 0);
        Color startColor = pokemonImage.color;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            pokemonImage.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            pokemonImage.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, 0, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pokemonImage.transform.localPosition = endPosition;
        pokemonImage.color = new Color(startColor.r, startColor.g, startColor.b, 0);
    }
}
