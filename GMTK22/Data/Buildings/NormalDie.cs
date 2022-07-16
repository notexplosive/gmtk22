﻿using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class NormalDie : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification("Average Die",
                info => new NormalDie(info),
                new Costs(40)
            );
        

        public NormalDie(PositionAndMap positionAndMap) : base(positionAndMap, new[] {1, 2, 3, 4, 5, 6}, NormalDie.Spec)
        {
            var moneyMaker = new MoneyMaker(Actor);
            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }
    }
}
