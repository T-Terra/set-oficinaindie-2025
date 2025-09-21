using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public PlayerData playerData;

    public void GameOver()
    {
        // Handle game over logic here
        Debug.Log("Game Over!");
    }

    public void LevelUp()
    {
        // Handle game over logic here
        Debug.Log("Level Up!");
    }

}