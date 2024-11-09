using UnityEngine;

public class FountainFlowController : MonoBehaviour
{
    public Sprite[] frames; // Assign the 3 fountain sprites in the Inspector
    public float frameRate = 0.1f; // Adjust speed as needed

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length;
            spriteRenderer.sprite = frames[currentFrame];
        }
    }
}
