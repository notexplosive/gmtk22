using System;

namespace GMTK22.Data
{
    public class Player
    {
        private int money;

        public void GainMoney(int delta)
        {
            this.money += delta;

            MoneyChanged?.Invoke(this.money, delta);
        }

        public event Action<int, int> MoneyChanged;
    }
}
