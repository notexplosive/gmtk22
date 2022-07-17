using ExTween;
using GMTK22.Components;
using GMTK22.Data;
using Machina.Components;
using Machina.Data;
using Machina.Data.Layout;
using Machina.Engine;
using Machina.Engine.Assets;
using Machina.Engine.Cartridges;
using Machina.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace GMTK22
{
    public class DieCartridge : GameCartridge
    {
        public static SoundEffectInstance bgm;
        public static SoundBundle bubbleSounds;
        public static SoundBundle frogSounds;
        public static SoundBundle hiBlockSounds;
        public static SoundBundle loBlockSounds;
        public static SoundBundle popSounds;
        public static SoundEffectInstance shakeSound;
        public static SoundEffectInstance snapSound;
        public static SoundEffectInstance claveSound;

        public DieCartridge() : base(new Point(1600, 900), ResizeBehavior.KeepAspectRatio)
        {
        }

        public static GameCore GameCore { get; private set; }

        public override void OnGameLoad(GameSpecification specification, MachinaRuntime runtime)
        {
            SceneLayers.BackgroundColor = Color.ForestGreen;

            // spin sounds
            DieCartridge.bubbleSounds = new SoundBundle("bubble", 7);
            DieCartridge.popSounds = new SoundBundle("pop", 7);
            DieCartridge.hiBlockSounds = new SoundBundle("hiblock", 2);
            DieCartridge.loBlockSounds = new SoundBundle("lowblock", 2);
            
            // number sounds
            DieCartridge.frogSounds = new SoundBundle("frog", 7);
            DieCartridge.shakeSound = MachinaClient.Assets.GetSoundEffectInstance("shake");
            DieCartridge.snapSound = MachinaClient.Assets.GetSoundEffectInstance("snap");
            DieCartridge.claveSound = MachinaClient.Assets.GetSoundEffectInstance("clave");

            DieCartridge.bgm = MachinaClient.Assets.GetSoundEffectInstance("bgm");
            DieCartridge.bgm.IsLooped = true;

            // Setup
            DieCartridge.GameCore = new GameCore(SceneLayers, Random.Seed);

            // UI
            var layout = LayoutNode.VerticalParent("", LayoutSize.Pixels(1600, 900), LayoutStyle.Empty,
                LayoutNode.Leaf("MoneyCounter", LayoutSize.StretchedHorizontally(120)),
                LayoutNode.Leaf("Tooltip", LayoutSize.StretchedBoth()),
                LayoutNode.OneOffParent("BuildMenuWrapper", LayoutSize.StretchedHorizontally(200),
                    new LayoutStyle(new Point(5)),
                    LayoutNode.Leaf("BuildMenu", LayoutSize.StretchedBoth())
                )
            ).Bake();

            var uiLayoutActors = new LayoutActors(DieCartridge.GameCore.UiScene, layout);

            // Money counter
            var moneyCounter = uiLayoutActors.GetActor("MoneyCounter");
            new BoundedTextRenderer(moneyCounter, "0", MachinaClient.Assets.GetSpriteFont("UIFont"), Color.White,
                Alignment.Center);
            new MoneyTracker(moneyCounter, DieCartridge.GameCore.Player);

            // Build menu
            var buildMenuActor = uiLayoutActors.GetActor("BuildMenu");
            new Hoverable(buildMenuActor);
            var buildMenu = new BuildMenu(buildMenuActor);

            // Selector
            var selector = new BuildingSelector(buildMenu);

            // Tooltip
            var tooltipActor = uiLayoutActors.GetActor("Tooltip");
            new Tooltip(tooltipActor, buildMenu, selector);

            // WORLD
            var buildingMap = new BuildingMap(selector);

            buildMenu.RequestedBuilding += buildingMap.BuildFromCommand;

            // Starting Die
            var position = new BuildingPosition(Point.Zero);

            buildingMap.CreateBuildSite(new BuildingPosition(position.GridPosition + new Point(0, 0)));

            buildingMap.CreateBuildSite(new BuildingPosition(position.GridPosition + new Point(-1, 0)));
            buildingMap.CreateBuildSite(new BuildingPosition(position.GridPosition + new Point(1, 0)));

            buildingMap.CreateBuildSite(new BuildingPosition(position.GridPosition + new Point(-2, 0)));
            buildingMap.CreateBuildSite(new BuildingPosition(position.GridPosition + new Point(2, 0)));

            buildingMap.CreateBuildSite(new BuildingPosition(position.GridPosition + new Point(-3, 0)));
            buildingMap.CreateBuildSite(new BuildingPosition(position.GridPosition + new Point(3, 0)));
            buildingMap.CreateBuildSite(new BuildingPosition(position.GridPosition + new Point(-3, 1)));
            buildingMap.CreateBuildSite(new BuildingPosition(position.GridPosition + new Point(3, 1)));
            buildingMap.CreateBuildSite(new BuildingPosition(position.GridPosition + new Point(-3, -1)));
            buildingMap.CreateBuildSite(new BuildingPosition(position.GridPosition + new Point(3, -1)));

            new FullscreenToggler(GameCore.GameScene.AddActor("fullscren"));
            
            var titleScene = SceneLayers.AddNewScene();

            var titleLayout = LayoutNode.VerticalParent("Screen", LayoutSize.Pixels(1600, 900), LayoutStyle.Empty,
                LayoutNode.StretchedSpacer(),
                LayoutNode.Leaf("Title", LayoutSize.StretchedHorizontally(90)),
                LayoutNode.Spacer(50),
                LayoutNode.Leaf("Byline1", LayoutSize.StretchedHorizontally(30)),
                LayoutNode.Spacer(10),
                LayoutNode.Leaf("Byline2", LayoutSize.StretchedHorizontally(30)),
                LayoutNode.Spacer(100),
                LayoutNode.Leaf("StartText", LayoutSize.StretchedHorizontally(30)),
                LayoutNode.StretchedSpacer()
            ).Bake();

            var titleLayoutActors = new LayoutActors(titleScene, titleLayout);
            new BoundedTextRenderer(titleLayoutActors.GetActor("Title"), "Seven Pips",
                    MachinaClient.Assets.GetSpriteFont("UIFont"), Color.White, Alignment.Center, Overflow.Ignore)
                .EnableDropShadow(Color.Black);
            new BoundedTextRenderer(titleLayoutActors.GetActor("Byline1"),
                "game design and programming by NotExplosive", MachinaClient.Assets.GetSpriteFont("UIFontSmall"),
                Color.White, Alignment.Center, Overflow.Ignore).EnableDropShadow(Color.Black);
            new BoundedTextRenderer(titleLayoutActors.GetActor("Byline2"), "sound design and music by quarkimo",
                    MachinaClient.Assets.GetSpriteFont("UIFontSmall"), Color.White, Alignment.Center, Overflow.Ignore)
                .EnableDropShadow(Color.Black);
            new BoundedTextRenderer(titleLayoutActors.GetActor("StartText"), "roll a 7 to win\npress F to toggle fullscreen\nclick anywhere to start",
                    MachinaClient.Assets.GetSpriteFont("UIFontSmall"), Color.White, Alignment.Center, Overflow.Ignore)
                .EnableDropShadow(Color.Black);
            new ClickToStart(titleScene.AddActor("click to start"));

#if DEBUG
            new Cheats(DieCartridge.GameCore.UiScene.AddActor("Cheat"));
#endif
        }

        public override void PrepareDynamicAssets(AssetLoader loader, MachinaRuntime runtime)
        {
        }
    }

    public class FullscreenToggler : BaseComponent
    {
        public FullscreenToggler(Actor actor) : base(actor)
        {
        }

        public override void OnKey(Keys key, ButtonState state, ModifierKeys modifiers)
        {
            if (key == Keys.F && state == ButtonState.Pressed && modifiers.None)
            {
                Runtime.Specification.settings.fullscreen.State = !Runtime.Specification.settings.fullscreen.State;
                Runtime.Specification.settings.Apply(Runtime.WindowInterface);
            }
        }
    }

    public class ClickToStart : BaseComponent
    {
        public ClickToStart(Actor actor) : base(actor)
        {
        }

        public override void OnMouseButton(MouseButton button, Vector2 currentPosition, ButtonState state)
        {
            if (button == MouseButton.Left && state == ButtonState.Released)
            {
                this.actor.scene.sceneLayers.RemoveScene(this.actor.scene);
                DieCartridge.GameCore.Progression.StartGame();
            }
        }
    }
}
