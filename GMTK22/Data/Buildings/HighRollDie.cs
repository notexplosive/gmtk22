using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class HighRollDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(new NameAndDescription("Royal Die", "Can roll 4, 5, and 6"),
                info => new HighRollDie(info),
                new Costs(500)
            );

        public HighRollDie(PositionAndMap positionAndMap) : base(positionAndMap, new DieData(new[] {4, 5, 6}, Palette.RoyalDieBody, Palette.RoyalDiePips))
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }

        public override BuildingSpecification MySpec => HighRollDie.Spec;
    }
}
