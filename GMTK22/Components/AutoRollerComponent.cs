using System;
using GMTK22.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public abstract class MainBuilding : Building
    {
        public MainBuilding(BuildingPosition position, string name, BuildingMap map) : base(position, name, map, 128)
        {
        }

        public abstract bool IsIdle();
        public abstract void Roll();
    }
    
    public class AutoRollerComponent : BaseComponent
    {
        private readonly MainBuilding targetBuilding;
        private readonly float maxCooldown;
        private float cooldown;
        private readonly BoundingRect boundingRect;

        public AutoRollerComponent(Actor actor, MainBuilding targetBuilding) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.targetBuilding = targetBuilding;
            this.maxCooldown = 5f;
            this.cooldown = this.maxCooldown;
        }

        public override void Update(float dt)
        {
            this.cooldown -= dt;

            if (this.cooldown <= 0)
            {
                if (this.targetBuilding.IsIdle())
                {
                    this.targetBuilding.Roll();
                    this.cooldown = this.maxCooldown;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var rect = this.boundingRect.Rect;
            
            spriteBatch.FillRectangle(rect, Color.White, transform.Depth + 10);

            var percent = 1f - this.cooldown / this.maxCooldown;
            var prevRadians = 0f;
            var radius = rect.Width * 0.4f;

            for (float f = 0; f <= percent; f += 0.1f)
            {
                var radians = MathF.PI * 2 * f;
                if (f > 0)
                {
                    var prevPoint = new Vector2(MathF.Cos(prevRadians), MathF.Sin(prevRadians)) * radius + transform.Position;
                    var currPoint = new Vector2(MathF.Cos(radians), MathF.Sin(radians)) * radius + transform.Position;
                    spriteBatch.DrawLine(prevPoint, currPoint, Color.Blue, 4f, transform.Depth - 10);
                }
                
                prevRadians = radians;
            }
        }
    }
}
