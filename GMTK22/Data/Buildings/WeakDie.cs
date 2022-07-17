using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class WeakDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification(
                new NameAndDescription("Starter Die","Can roll 1, 2, 3"),
                new Costs(0), new ColorPair(Palette.WeakDieBody, Palette.WeakDiePips), info => new WeakDie(info), DieRenderer.GenericDrawDie);

        public WeakDie(PositionAndMap positionAndMap) : base(positionAndMap, new DieData(new[] {1, 2, 3}, Palette.WeakDieBody, Palette.WeakDiePips))
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }

        public override BuildingSpecification MySpec => WeakDie.Spec;
    }
}
