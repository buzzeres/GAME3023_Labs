using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject health; // Reference to health bar UI element

    // Method to set health bar scale
    public void SetHealth(float health)
    {
        health = Mathf.Clamp(health, 0, 1); // Clamp health to prevent exceeding 1 or going below 0
        this.health.transform.localScale = new Vector2(health, 1);
    }
}
