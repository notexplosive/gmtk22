using GMTK22.Components;
using Machina.Data;

namespace GMTK22.Data.Buildings
{
    public class CosmicDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(new NameAndDescription("Cosmic Space Die", "This die only rolls 7, also it wins the game"),
                new Costs(1000), new ColorPair(Palette.CosmicDieBody, Palette.CosmicDiePips),
                info => new CosmicDie(info),
                DieRenderer.GenericDrawDie7Pips);

        public CosmicDie(PositionAndMap positionAndMap) : base(positionAndMap,
            new DieData(new[] {7}, Palette.CosmicDieBody, Palette.CosmicDiePips, 20))
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollStarted += DieCartridge.GameCore.Progression.TriggerEndCutscene;
        }

        public override BuildingSpecification MySpec => CosmicDie.Spec;
    }
}
