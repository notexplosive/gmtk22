using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class BuildingHoverSelectionRenderer : BaseComponent
    {
        private readonly BoundingRect boundingRect;
        private readonly Hoverable hoverable;
        private readonly SelectableBuilding selectable;

        public BuildingHoverSelectionRenderer(Actor actor) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
            this.hoverable = RequireComponent<Hoverable>();
            this.selectable = RequireComponent<SelectableBuilding>();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var borderColor = Palette.HoveredBuildingBorderColor;
            
            if (this.selectable.IsSelected())
            {
                borderColor = Palette.SelectedBuildingBorderColor;
            }
            
            if (this.selectable.IsSelected() || (this.hoverable.IsHovered && BusyFlags == 0))
            {
                var rect = this.boundingRect.Rect;
                rect.Inflate(10, 10);
                spriteBatch.DrawRectangle(rect, borderColor.WithMultipliedOpacity(0.5f), 5, transform.Depth - 15);
            }
        }

        public int BusyFlags { get; set; }
    }
}
