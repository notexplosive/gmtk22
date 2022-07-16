using GMTK22.Components;
using GMTK22.Data;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Machina.Engine.Assets;
using Machina.Engine.Cartridges;
using Microsoft.Xna.Framework;

namespace GMTK22
{
    public class DieCartridge : GameCartridge
    {
        public DieCartridge() : base(new Point(1600, 900), ResizeBehavior.KeepAspectRatio)
        {
        }

        public override void OnGameLoad(GameSpecification specification, MachinaRuntime runtime)
        {
            SceneLayers.BackgroundColor = Color.ForestGreen;

            // Setup
            var gameCore = new GameCore(SceneLayers, Random.Seed);
            
            // Money Tracker
            var moneyCounter = gameCore.UiScene.AddActor("MoneyCounter");
            new BoundingRect(moneyCounter, 1600, 200);
            new BoundedTextRenderer(moneyCounter, "0", MachinaClient.Assets.GetSpriteFont("UIFont"), Color.White,
                Alignment.Center);
            new MoneyTracker(moneyCounter, gameCore.Player);

            var buildingMap = new BuildingMap(gameCore);
            
            // Starting Die
            buildingMap.CreateDie(new Point(0,0));
        }

        public override void PrepareDynamicAssets(AssetLoader loader, MachinaRuntime runtime)
        {
        }
    }
}
