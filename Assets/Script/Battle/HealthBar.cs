using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject health; // Reference to the health bar UI element

    // Method to set health bar scale
    public void SetHealth(float healthValue)
    {
        // Ensure the health value is clamped between 0 and 1
        healthValue = Mathf.Clamp(healthValue, 0f, 1f);

        // Set the local scale for the health bar
        health.transform.localScale = new Vector2(healthValue, 1); // Assuming a 2D scale for the health bar
    }
}

