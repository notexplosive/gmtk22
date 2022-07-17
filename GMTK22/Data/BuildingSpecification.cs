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

        public BuildingSpecification(NameAndDescription nameAndDescription, Costs costs,
            Action<PositionAndMap> buildCallback)
        {
            Name = nameAndDescription.Name;
            Description = nameAndDescription.Description;
            Costs = costs;
            this.buildCallback = buildCallback;
        }
    }
}
