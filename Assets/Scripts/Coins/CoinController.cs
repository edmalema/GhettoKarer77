using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    
    public static CoinManager instance; // gjør den mulig å bruke over flere scrips

    [SerializeField]
    TMPro.TextMeshProUGUI CoinDisplay;

    public int TotalCoins = 100;

   

    public void AddCoin(int amount) // minus coin = TotalCoins = Totalcoins + -X og pluss = TotalCoins = Totalcoins + X så det er både pluss og minus i samme funcson
    {
        TotalCoins += amount;
        CoinDisplay.text = TotalCoins.ToString();
    }
    private void Awake()
    {
        CoinDisplay.text = TotalCoins.ToString();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
   
}
