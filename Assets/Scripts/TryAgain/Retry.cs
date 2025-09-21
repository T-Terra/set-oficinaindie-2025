using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Retry : MonoBehaviour
{
    public static Retry Instance;
    [SerializeField] int PriceToTry = 0;
    [SerializeField] Button PriceButton;
    [SerializeField] TMP_Text TextPrice;
    [SerializeField] GameObject HUDReTry;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        TextPrice.text = PriceToTry.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableHUDRetry( bool enable )
    {
        GameManager.Instance.Pause(enable);
        HUDReTry.SetActive(enable);
    }

    public void TryAgainButton()
    {
        if (GameManager.Instance.playerData.coins < PriceToTry)
        {
            PriceButton.interactable = false;
        }
        else
        {
            GameManager.Instance.playerData.SpendCoins(PriceToTry);
            HUDReTry.SetActive(false);
            GameManager.Instance.Pause(false);
        }
    }
}
