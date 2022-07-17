using GMTK22.Components;
using Microsoft.Xna.Framework;

namespace GMTK22.Data.Buildings
{
    public class HighRollDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(new NameAndDescription("Royal Die", "Can roll 4, 5, and 6"),
                new Costs(500), new ColorPair(Palette.RoyalDieBody, Palette.RoyalDiePips), 
                info => new HighRollDie(info),
                DieRenderer.GenericDrawDie
                );

        public HighRollDie(PositionAndMap positionAndMap) : base(positionAndMap, new DieData(new[] {4, 5, 6}, Palette.RoyalDieBody, Palette.RoyalDiePips))
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }

        public override BuildingSpecification MySpec => HighRollDie.Spec;
    }
}
