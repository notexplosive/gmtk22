using System;
using GMTK22.Components;
using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Data
{
    public readonly struct DrawInfo
    {
        public DrawInfo(SpriteBatch spriteBatch, Rectangle rectangle, Color primaryColor, Color secondaryColor, Depth depth)
        {
            SpriteBatch = spriteBatch;
            Rectangle = rectangle;
            PrimaryColor = primaryColor;
            SecondaryColor = secondaryColor;
            Depth = depth;
        }

        public SpriteBatch SpriteBatch { get; }
        public Rectangle Rectangle { get; }
        public Color PrimaryColor { get; }
        public Color SecondaryColor { get; }
        public Depth Depth { get; }
    }
    
    public readonly struct BuildingSpecification
    {
        public string Name { get; }
        public Costs Costs { get; }
        public string Description { get; }

        public readonly Action<PositionAndMap> buildCallback;
        private readonly Action<DrawInfo> drawCallback;
        public TwoColor Colors { get; }

        public BuildingSpecification(NameAndDescription nameAndDescription,
            Costs costs,
            TwoColor colors,
            Action<PositionAndMap> buildCallback,
            Action<DrawInfo> drawCallback)
        {
            Name = nameAndDescription.Name;
            Description = nameAndDescription.Description;
            Costs = costs;
            Colors = colors;
            this.buildCallback = buildCallback;
            this.drawCallback = drawCallback;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rectangle, Depth depth)
        {
            this.drawCallback(new DrawInfo(spriteBatch, rectangle, Colors.PrimaryColor, Colors.SecondaryColor, depth));
        }
    }
}
