using System;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class GameCore
    {
        public Scene UiScene { get; }
        public Player Player { get; }
        public Scene GameScene { get; }
        public NoiseBasedRNG CleanRandom { get; }

        public GameCore(SceneLayers sceneLayers, int randomSeed)
        {
            CleanRandom = new NoiseBasedRNG((uint)randomSeed);
            GameScene = sceneLayers.AddNewScene();
            UiScene = sceneLayers.AddNewScene();
            GameScene.camera.UnscaledPosition -=  new Vector2(1600 / 2f, 900 / 2f);
            Player = new Player();
        }
    }
}
