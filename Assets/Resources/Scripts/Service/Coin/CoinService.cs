using System;
using UnityEngine;

namespace PathGame.Service.Coin
{
    public class CoinService : MonoBehaviour, ICoinService
    {
        [SerializeField] private int _coin = 0;
        public event Action<int> coinUpdate;

        private void Start()
        {
            coinUpdate?.Invoke(_coin);
        }

        public void AddCoin(int coin)
        {
            _coin += coin;
            coinUpdate?.Invoke(coin);
        }

        
    }
}