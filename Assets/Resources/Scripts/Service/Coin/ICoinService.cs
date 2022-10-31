using System;

namespace PathGame.Service.Coin
{
    public interface ICoinService
    {
        public void AddCoin(int coin);
        public event Action<int> coinUpdate;
    }
}