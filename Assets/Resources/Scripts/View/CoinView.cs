using PathGame.Service.Coin;
using TMPro;
using UnityEngine;
using Zenject;

public class CoinView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _texts;
    
    private ICoinService _coinService;
    
    [Inject]
    public void Construct(ICoinService coinService)
    {
        _coinService = coinService;
        _coinService.coinUpdate += CoinServiceOnCoinUpdate;
    }
    
    private void CoinServiceOnCoinUpdate(int obj)
    {
        foreach (var text in _texts)
            text.text = obj.ToString();
        
    }
}
