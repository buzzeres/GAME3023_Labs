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
    Encounter,
    Unknown
}

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private GameController gameController;

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

            case CollisionType.Encounter:
                gameController.StartBattle(CollisionType.Encounter); // Pass the type to StartBattle
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
    }

    private void HandleWallCollision(GameObject wall)
    {
        Debug.Log("Wall collision handled.");
    }

    private void HandleDoorCollision(GameObject door)
    {
        Debug.Log("Door collision detected. Transitioning to the next level.");
        LevelManager.Instance.LoadNextLevel();
    }

    private void HandleBedCollision(GameObject bed)
    {
        Debug.Log("Resting at the bed. Restoring health.");
    }

    private void HandleExtraInventoryCollision(GameObject inventoryItem)
    {
        Debug.Log("Extra Inventory acquired. Adding items to inventory.");
    }
}


public static class CollisionUtility
{
    public static CollisionType GetCollisionType(string tag)
    {
        switch (tag)
        {
            case "Player":
                return CollisionType.Player;
            case "Wall":
                return CollisionType.Wall;
            case "Door":
                return CollisionType.Door;
            case "Bed":
                return CollisionType.Bed;
            case "ExtraInventory":
                return CollisionType.ExtraInventory;
            case "ClosetWashroom":
                return CollisionType.ClosetWashroom;
            case "Encounter":
                return CollisionType.Encounter;
            default:
                return CollisionType.Unknown;
        }
    }
}

