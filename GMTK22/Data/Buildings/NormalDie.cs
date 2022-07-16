using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class NormalDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification("Build Average Die",
                info => new NormalDie(info.Position, info.Map),
                new Costs(40)
            );
        

        public NormalDie(BuildingPosition position, BuildingMap map) : base(position, map, "Average Die", new[] {1, 2, 3, 4, 5, 6}, NormalDie.Spec)
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }
    }
}
