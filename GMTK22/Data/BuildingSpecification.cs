using System;
using Machina.Data;
using MonoGame.Extended;

namespace GMTK22.Data
{
    public readonly struct BuildingSpecification
    {
        public string Name { get; }
        public Costs Costs { get; }
        public string Description { get; }

        public readonly Action<PositionAndMap> buildCallback;
        public readonly Action<DrawInfo> drawCallback;

        public BuildingSpecification(NameAndDescription nameAndDescription, Action<PositionAndMap> buildCallback, Costs costs, Action<DrawInfo> drawCallback = null)
        {
            Name = nameAndDescription.Name;
            Description = nameAndDescription.Description;
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
