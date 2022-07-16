namespace GMTK22.Data
{
    public class Costs
    {
        public int SellValue { get; }
        public int ConstructCost { get; }

        public Costs(int constructCost)
        {
            ConstructCost = constructCost;
            SellValue = (int) (ConstructCost * 0.8f);
        }
    }
}
