using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class GameCore
    {
        public GameCore(SceneLayers sceneLayers, int randomSeed)
        {
            CleanRandom = new NoiseBasedRNG((uint) randomSeed);
            GameScene = sceneLayers.AddNewScene();
            UiScene = sceneLayers.AddNewScene();
            Player = new Player();
            
            Progression = new Progression(UiScene.AddActor("ProgressTracker"), GameScene);
            
            Player.MoneyChanged += Progression.OnMoneyChanged;
            
            GameScene.camera.Zoom = 2f;
            GameScene.camera.UnscaledPosition = -new Vector2(1600 / 4f, 900 * 4);
        }

        public Scene UiScene { get; }
        public Player Player { get; }
        public Scene GameScene { get; }
        public NoiseBasedRNG CleanRandom { get; }
        public Progression Progression { get; }
    }
}
