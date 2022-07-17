using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Components
{
    public class MoneyMaker : BaseComponent
    {
        public MoneyMaker(Actor actor) : base(actor)
        {
        }

        public void GainMoneyFromRoll(int amount)
        {
            MoneyMaker.GainMoney(amount, transform.Position);
        }

        public static void GainMoney(int amount, Vector2 particleSpawnPosition)
        {
            DieCartridge.frogSounds.PlayRandom();

            MoneyMaker.SpawnMoneyTextParticle(amount, particleSpawnPosition);
            DieCartridge.GameCore.Player.GainMoney(amount);
        }

        private static void SpawnMoneyTextParticle(int value, Vector2 particleSpawnPosition)
        {
            var moneyCounter = DieCartridge.GameCore.GameScene.AddActor("TextParticle");
            moneyCounter.transform.Position = particleSpawnPosition;

            // very foregrounded
            moneyCounter.transform.Depth = 100;

            new BoundingRect(moneyCounter, 5, 5).SetOffsetToCenter();
            var text = new BoundedTextRenderer(moneyCounter, "0", MachinaClient.Assets.GetSpriteFont("UIFont"),
                Color.White,
                Alignment.Center, Overflow.Ignore);
            text.Text = $"+{value}";
            text.TextColor = Palette.MoneyColor;

            new TextToastTween(moneyCounter);
        }
    }
}
