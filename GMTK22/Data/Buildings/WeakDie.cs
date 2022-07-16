using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class WeakDie : BaseDieBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification("Build Weak Die",
                info => new WeakDie(info.Position, info.Map),
                new Costs(0)
            );

        public WeakDie(BuildingPosition position, BuildingMap map) : base(position, map, new[] {1, 2, 3}, "Weak Die", Spec)
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }
    }
}
