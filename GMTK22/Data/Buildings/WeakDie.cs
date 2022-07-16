using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class WeakDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification("Build Weak Die",
                info => new WeakDie(info),
                new Costs(0)
            );

        public WeakDie(PositionAndMap positionAndMap) : base(positionAndMap, new[] {1, 2, 3}, WeakDie.Spec)
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }
    }
}
