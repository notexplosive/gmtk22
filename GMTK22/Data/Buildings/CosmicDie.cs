using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class CosmicDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(new NameAndDescription("Cosmic Space Die", "Can roll from 1-7"),
                info => new CosmicDie(info),
                new Costs(1000)
            );

        public CosmicDie(PositionAndMap positionAndMap) : base(positionAndMap,
            new DieData(new[] {1, 2, 3, 4, 5, 6, 7}, Palette.CosmicDieBody, Palette.CosmicDiePips))
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }

        public override BuildingSpecification MySpec => CosmicDie.Spec;
    }
}
