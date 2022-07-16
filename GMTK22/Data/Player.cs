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

        public bool CanAfford(int cost)
        {
            return this.money >= cost;
        }

        public void SpendMoney(int commandCost)
        {
            this.money -= commandCost;
        }
    }
}
