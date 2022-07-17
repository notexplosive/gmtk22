using GMTK22.Components;
using Microsoft.Xna.Framework;

namespace GMTK22.Data.Buildings
{
    public class HighRollDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(new NameAndDescription("Royal Die", "Can roll 4, 5, and 6, yields 3x pips"),
                new Costs(300), new ColorPair(Palette.RoyalDieBody, Palette.RoyalDiePips), 
                info => new HighRollDie(info),
                DieRenderer.GenericDrawDie5Pips
                );

        public HighRollDie(PositionAndMap positionAndMap) : base(positionAndMap, new DieData(new[] {4, 5, 6}, Palette.RoyalDieBody, Palette.RoyalDiePips, DieCartridge.loBlockSounds))
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += (roll) => moneyMaker.GainMoneyFromRoll(roll.FaceValue * 3);
        }

        public override BuildingSpecification MySpec => HighRollDie.Spec;
    }
}
