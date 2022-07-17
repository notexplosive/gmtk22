using GMTK22.Components;
using Machina.Data;

namespace GMTK22.Data.Buildings
{
    public class CosmicDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(new NameAndDescription("Cosmic Space Die", "Can roll from 1-7"),
                new Costs(1000), new ColorPair(Palette.CosmicDieBody, Palette.CosmicDiePips),
                info => new CosmicDie(info),
                DieRenderer.GenericDrawDie);

        public CosmicDie(PositionAndMap positionAndMap) : base(positionAndMap,
            new DieData(new[] {1, 2, 3, 4, 5, 6, 7}, Palette.CosmicDieBody, Palette.CosmicDiePips))
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }

        public override BuildingSpecification MySpec => CosmicDie.Spec;
    }
}
