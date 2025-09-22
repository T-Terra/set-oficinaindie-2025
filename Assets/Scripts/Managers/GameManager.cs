using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] TMP_Text Text_Health;
    [SerializeField] TMP_Text Text_Coins;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        Text_Health.text = playerData.shield.ToString();
        Text_Coins.text = playerData.coins.ToString();
    }

    public PlayerData playerData;

    void Start()
    {
        Pause(false);
    }

    public void LoseBall()
    {
        // Handle ball loss logic here
        LauncherScript.canLaunch = true;
    }

    public void GameOver()
    {
        // Handle game over logic here
        Retry.Instance.EnableHUDRetry(true);
    }

    public void LevelUp()
    {
        // Handle game over logic here
        PowerUpSelector.Instance.RollPowerUp();
    }

    public void Pause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }

}