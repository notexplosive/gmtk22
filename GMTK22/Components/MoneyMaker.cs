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

        public void GainMoneyFromRoll(Roll roll)
        {
            SpawnMoneyTextParticle(roll.FaceValue);
            DieCartridge.GameCore.Player.GainMoney(roll.FaceValue);
        }
        
        private void SpawnMoneyTextParticle(int value)
        {
            var moneyCounter = this.actor.transform.AddActorAsChild("TextParticle");

            moneyCounter.transform.LocalDepth = -10;
            
            new BoundingRect(moneyCounter, 5, 5).SetOffsetToCenter();
            var text = new BoundedTextRenderer(moneyCounter, "0", MachinaClient.Assets.GetSpriteFont("UIFont"), Color.White,
                Alignment.Center, Overflow.Ignore);
            text.Text = $"+{value}";
            text.TextColor = Color.Goldenrod;

            new TextToastTween(moneyCounter);
        }
    }
}
