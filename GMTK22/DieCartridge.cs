using GMTK22.Components;
using GMTK22.Data;
using Machina.Components;
using Machina.Data;
using Machina.Data.Layout;
using Machina.Engine;
using Machina.Engine.Assets;
using Machina.Engine.Cartridges;
using Microsoft.Xna.Framework;

namespace GMTK22
{
    public class DieCartridge : GameCartridge
    {
        public static GameCore GameCore { get; private set; }

        public DieCartridge() : base(new Point(1600, 900), ResizeBehavior.KeepAspectRatio)
        {
        }

        public override void OnGameLoad(GameSpecification specification, MachinaRuntime runtime)
        {
            SceneLayers.BackgroundColor = Color.ForestGreen;

            // Setup
            GameCore = new GameCore(SceneLayers, Random.Seed);
            
            // UI
            var layout = LayoutNode.VerticalParent("", LayoutSize.Pixels(1600, 900), LayoutStyle.Empty,
                LayoutNode.Leaf("MoneyCounter", LayoutSize.StretchedHorizontally(120)),
                LayoutNode.StretchedSpacer(),
                LayoutNode.OneOffParent("BuildMenuWrapper", LayoutSize.StretchedHorizontally(200), new LayoutStyle(margin: new Point(5)),
                    LayoutNode.Leaf("BuildMenu", LayoutSize.StretchedBoth())
                )
            ).Bake();

            var uiLayoutActors = new LayoutActors(GameCore.UiScene, layout);
            
            // Money counter
            var moneyCounter = uiLayoutActors.GetActor("MoneyCounter");
            new BoundedTextRenderer(moneyCounter, "0", MachinaClient.Assets.GetSpriteFont("UIFont"), Color.White,
                Alignment.Center);
            new MoneyTracker(moneyCounter, GameCore.Player);
            
            // Build menu
            var buildMenuActor = uiLayoutActors.GetActor("BuildMenu");
            new Hoverable(buildMenuActor);
            var buildMenu = new BuildMenu(buildMenuActor);
            
            // Selector
            var selector = new BuildingSelector(buildMenu);
            
            // WORLD
            var buildingMap = new BuildingMap(selector);

            buildMenu.RequestedBuilding += buildingMap.BuildFromCommand;
            
            // Starting Die
            BuildingPosition position = new BuildingPosition(Point.Zero);
            new DieBuilding(position, buildingMap);
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    var offset = new Point(x, y);
                    buildingMap.CreateBuildSite(new BuildingPosition(position.GridPosition + offset));
                    buildingMap.CreateUpgradeSite(new BuildingPosition(position.GridPosition,offset));
                }
            }
        }

        public override void PrepareDynamicAssets(AssetLoader loader, MachinaRuntime runtime)
        {
        }
    }
}
