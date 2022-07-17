using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class NormalDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(new NameAndDescription("Average Die", "Can roll 1, 2, 3, 4, 5, 6"),
                info => new NormalDie(info),
                new Costs(40)
            );

        public NormalDie(PositionAndMap positionAndMap) : base(positionAndMap, new DieData(new[] {1, 2, 3, 4, 5, 6}, Palette.NormalDieBody, Palette.NormalDiePips))
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }

        public override BuildingSpecification MySpec => NormalDie.Spec;
    }
}
