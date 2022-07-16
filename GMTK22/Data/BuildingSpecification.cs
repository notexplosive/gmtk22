using System;
using Machina.Data;
using MonoGame.Extended;

namespace GMTK22.Data
{
    public readonly struct BuildingSpecification
    {
        public string Name { get; }
        public Costs Costs { get; }
        public readonly Action<PositionAndMap> buildCallback;
        public readonly Action<DrawInfo> drawCallback;

        public BuildingSpecification(string name, Action<PositionAndMap> buildCallback, Costs costs, Action<DrawInfo> drawCallback = null)
        {
            Name = name;
            Costs = costs;
            this.buildCallback = buildCallback;
            this.drawCallback = drawCallback;

            if (this.drawCallback == null)
            {
                this.drawCallback = DefaultDraw;
            }

        }

        public void DefaultDraw(DrawInfo drawInfo)
        {
            
        }
    }
}
