using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class WeakDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification("Build Weak Die",
                info => new WeakDie(info.Position, info.Map),
                new Costs(0)
            );

        public WeakDie(BuildingPosition position, BuildingMap map) : base(position, map, "Weak Die", new[] {1, 2, 3}, WeakDie.Spec)
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }
    }
}
