using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Data
{
    public readonly struct DrawInfo
    {
        public readonly SpriteBatch spriteBatch;
        public readonly Rectangle rectangle;
        public readonly float depth;

        public DrawInfo(SpriteBatch spriteBatch, Rectangle rectangle, float depth)
        {
            this.spriteBatch = spriteBatch;
            this.rectangle = rectangle;
            this.depth = depth;
        }
    }
}
