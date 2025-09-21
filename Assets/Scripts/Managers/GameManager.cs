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
        Retry.Instance.EnableHUDRetry(true);
        Debug.Log("Game Over!");
    }

    public void LevelUp()
    {
        // Handle game over logic here
        PowerUpSelector.Instance.RollPowerUp();
        Debug.Log("Level Up!");
    }

    public void Pause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }

}