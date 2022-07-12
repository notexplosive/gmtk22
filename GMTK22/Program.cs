using System;
using Machina.Data;
using Machina.Engine;
using MachinaDesktop;
using Microsoft.Xna.Framework;

namespace GMTK22
{
    public static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            MachinaBootstrap.Run(
                new GameSpecification(
                    "GMTK2022 - NotExplosive & Quarkimo",
                    args,
                    new GameSettings(new Point(1600, 900))), new GmtkCartridge(),
                ".");
        }
    }
}
