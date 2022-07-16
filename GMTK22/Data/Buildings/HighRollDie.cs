using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class HighRollDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification("Royal Die",
                info => new HighRollDie(info),
                new Costs(500)
            );

        public HighRollDie(PositionAndMap positionAndMap) : base(positionAndMap, new[] {4, 5, 6}, WeakDie.Spec)
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }
    }
}
