using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionType
{
    Player,
    Wall,
    Door,
    Bed,
    ExtraInventory,
    ClosetWashroom,
    Unknown
}

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject, "Collision");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject, "Trigger");
    }

    // Handles both Collision and Trigger events
    private void HandleCollision(GameObject other, string collisionSource)
    {
        CollisionType type = CollisionUtility.GetCollisionType(other.tag);

        switch (type)
        {
            case CollisionType.Player:
                HandlePlayerCollision(other);
                break;

            case CollisionType.Wall:
                HandleWallCollision(other);
                break;

            case CollisionType.Door:
                HandleDoorCollision(other);
                break;

            case CollisionType.Bed:
                HandleBedCollision(other);
                break;

            case CollisionType.ExtraInventory:
                HandleExtraInventoryCollision(other);
                break;

            case CollisionType.ClosetWashroom:
                HandleClosetWashroomCollision();
                break;

            default:
                Debug.LogWarning("Unknown collision type detected with: " + other.name);
                break;
        }
    }

    private void HandleClosetWashroomCollision()
    {
        Debug.Log("Closet Washroom collision detected. Transitioning to the next level.");
        LevelManager.Instance.LoadLevelByName("ClosetWashroom");
    }

    private void HandlePlayerCollision(GameObject player)
    {
        Debug.Log("Player collision handled.");
        // Add player-specific collision logic here
    }

    private void HandleWallCollision(GameObject wall)
    {
        Debug.Log("Wall collision handled.");
        // Add logic to handle collision with walls or obstacles
    }

    private void HandleDoorCollision(GameObject door)
    {
        Debug.Log("Door collision detected. Transitioning to the next level.");
        LevelManager.Instance.LoadNextLevel();
    }

    private void HandleBedCollision(GameObject bed)
    {
        Debug.Log("Resting at the bed. Restoring health.");
        // Implement health restoration logic here
    }

    private void HandleExtraInventoryCollision(GameObject inventoryItem)
    {
        Debug.Log("Extra Inventory acquired. Adding items to inventory.");
        // Implement inventory logic here
    }
}





// Utility class for handling collision type determination
public static class CollisionUtility
{
    // Returns the collision type based on the object's tag
    public static CollisionType GetCollisionType(string tag)
    {
        return tag switch
        {
            "Player" => CollisionType.Player,
            "Collision" => CollisionType.Wall,
            "Door" => CollisionType.Door,
            "Bed" => CollisionType.Bed,
            "ExtraInventory" => CollisionType.ExtraInventory,
            _ => CollisionType.Unknown,
        };
    }
}
