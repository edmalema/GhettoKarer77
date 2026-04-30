using TMPro.Examples;
using UnityEngine;

public class BuyInteraction : MonoBehaviour, IInteractable
{
    public int Value = -20;
    public string InteractMessage => objectInteractMessage;

    [SerializeField] private string objectInteractMessage;

    public void Interact()
    {
        Buy();
    }
    
    void Buy()
    {
        if (CoinManager.instance.TotalCoins + Value <= 1)
        {
            return;
        }
        else
        {
            CoinManager.instance.AddCoin(Value);
        }
            

    }
}
