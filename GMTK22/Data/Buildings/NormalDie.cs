using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class NormalDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(new NameAndDescription("Average Die", "Can roll 1, 2, 3, 4, 5, 6, yields 2x pips"),
                new Costs(40), new ColorPair(Palette.NormalDieBody, Palette.NormalDiePips), info => new NormalDie(info),
                DieRenderer.GenericDrawDie3Pips);

        public NormalDie(PositionAndMap positionAndMap) : base(positionAndMap,
            new DieData(new[] {1, 2, 3, 4, 5, 6}, Palette.NormalDieBody, Palette.NormalDiePips, DieCartridge.popSounds.GetNext()))
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += (roll) => moneyMaker.GainMoneyFromRoll(roll.FaceValue * 2);
        }

        public override BuildingSpecification MySpec => NormalDie.Spec;
    }
}
