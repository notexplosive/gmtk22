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
            var cleanRandom = new NoiseBasedRNG((uint)Random.Seed);
            var scene = SceneLayers.AddNewScene();
            var uiScene = SceneLayers.AddNewScene();
            scene.camera.UnscaledPosition -=  new Vector2(1600 / 2f, 900 / 2f);

            var player = new Player();
            
            // Money Tracker
            var moneyCounter = uiScene.AddActor("MoneyCounter");
            new BoundingRect(moneyCounter, 1600, 200);
            new BoundedTextRenderer(moneyCounter, "0", MachinaClient.Assets.GetSpriteFont("UIFont"), Color.White,
                Alignment.Center);
            new MoneyTracker(moneyCounter, player);
            
            // Starting Die
            var die = scene.AddActor("Die");
            new BoundingRect(die, new Point(128, 128)).SetOffsetToCenter();
            new Hoverable(die);
            new DieComponent(die, player, cleanRandom);
            new DieRenderer(die);
        }

        public override void PrepareDynamicAssets(AssetLoader loader, MachinaRuntime runtime)
        {
        }
    }
}
