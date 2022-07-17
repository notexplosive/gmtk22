namespace GMTK22.Data
{
    public class ConstructBuildingCommand : Command
    {
        private readonly BuildingSpecification spec;

        public ConstructBuildingCommand(BuildingSpecification spec)
        {
            this.spec = spec;
        }

        public override NameAndDescription NameAndDescription => new NameAndDescription($"Build {this.spec.Name}", this.spec.Description);
        public override int Cost => this.spec.Costs.ConstructCost;

        public override void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            this.spec.buildCallback(new PositionAndMap(buildingLocation,map));
        }

        public override void DrawButtonGraphic(DrawInfo drawInfo)
        {
            this.spec.drawCallback(drawInfo);
        }
    }
}
