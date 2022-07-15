using System.Collections.Generic;
using Machina.Components;
using Machina.Data;
using Machina.Data.Layout;
using Machina.Engine;
using Machina.Engine.Assets;
using Machina.Engine.Cartridges;
using Microsoft.Xna.Framework;

namespace GMTK22
{
    public class GmtkCartridge : GameCartridge
    {
        public GmtkCartridge() : base(new Point(1600, 900), ResizeBehavior.KeepAspectRatio)
        {
        }

        public override void OnGameLoad(GameSpecification specification, MachinaRuntime runtime)
        {
            SceneLayers.BackgroundColor = Color.Black;

            var layout = LayoutNode.OneOffParent("screen", LayoutSize.Pixels(1600, 900),
                new LayoutStyle(alignment: Alignment.Center, margin: new Point(100, 100)),
                LayoutNode.VerticalParent("dieBody", LayoutSize.FixedAspectRatio(1,1), LayoutStyle.Empty,
                    LayoutNode.HorizontalParent("row 1", LayoutSize.StretchedBoth(), LayoutStyle.Empty,
                        LayoutNode.Leaf("cell 1", LayoutSize.StretchedBoth()),
                        LayoutNode.Leaf("cell 2", LayoutSize.StretchedBoth()),
                        LayoutNode.Leaf("cell 3", LayoutSize.StretchedBoth())
                    ),
                    LayoutNode.HorizontalParent("row 2", LayoutSize.StretchedBoth(), LayoutStyle.Empty,
                        LayoutNode.Leaf("cell 4", LayoutSize.StretchedBoth()),
                        LayoutNode.Leaf("cell 5", LayoutSize.StretchedBoth()),
                        LayoutNode.Leaf("cell 6", LayoutSize.StretchedBoth())
                    ),
                    LayoutNode.HorizontalParent("row 3", LayoutSize.StretchedBoth(), LayoutStyle.Empty,
                        LayoutNode.Leaf("cell 7", LayoutSize.StretchedBoth()),
                        LayoutNode.Leaf("cell 8", LayoutSize.StretchedBoth()),
                        LayoutNode.Leaf("cell 9", LayoutSize.StretchedBoth())
                    )
                )
            ).Bake();

            var scene = SceneLayers.AddNewScene();
            var layoutActors = new LayoutActors(scene, layout, Point.Zero);

            var gridPositions = new List<Point>
            {
                new Point(0,0),
                new Point(1,0),
                new Point(2,0),
                new Point(0,1),
                new Point(1,1),
                new Point(2,1),
                new Point(0,2),
                new Point(1,2),
                new Point(2,2)
            };

            var actorMap = new Dictionary<Point, Actor>();
            
            for (int i = 1; i <= 9; i++)
            {
                actorMap[gridPositions[i-1]] = layoutActors.GetActor($"cell {i}");
            }

            new BoundingRectFill(layoutActors.GetActor("dieBody"), Color.DimGray);

            foreach (var actor in actorMap.Values)
            {
                new Hoverable(actor);
                new Clickable(actor);
                new Draggable(actor);
            }
        }

        public override void PrepareDynamicAssets(AssetLoader loader, MachinaRuntime runtime)
        {
        }
    }
}
