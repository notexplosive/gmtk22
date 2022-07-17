using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class WeakDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(new NameAndDescription("Starter Die","Can roll 1, 2, 3"),
                info => new WeakDie(info),
                new Costs(0)
            );

        public WeakDie(PositionAndMap positionAndMap) : base(positionAndMap, new[] {1, 2, 3})
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }

        public override BuildingSpecification MySpec => WeakDie.Spec;
    }
}
