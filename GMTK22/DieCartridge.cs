using System.Collections.Generic;
using GMTK22.Components;
using Machina.Components;
using Machina.Data;
using Machina.Data.Layout;
using Machina.Engine;
using Machina.Engine.Assets;
using Machina.Engine.Cartridges;
using Microsoft.Xna.Framework;

namespace GMTK22
{
    public class DieCartridge : GameCartridge
    {
        public DieCartridge() : base(new Point(1600, 900), ResizeBehavior.KeepAspectRatio)
        {
        }

        public override void OnGameLoad(GameSpecification specification, MachinaRuntime runtime)
        {
            SceneLayers.BackgroundColor = Color.Black;

            var cellStyle = new LayoutStyle(new Point(20), alignment: Alignment.Center);
            var layout = LayoutNode.OneOffParent("screen", LayoutSize.Pixels(1600, 900),
                new LayoutStyle(alignment: Alignment.Center, margin: new Point(100, 100)),
                LayoutNode.VerticalParent("dieBody", LayoutSize.FixedAspectRatio(1, 1), LayoutStyle.Empty,
                    LayoutNode.HorizontalParent("row 1", LayoutSize.StretchedBoth(), LayoutStyle.Empty,
                        LayoutNode.OneOffParent("cell 1 parent", LayoutSize.StretchedBoth(), cellStyle,
                            LayoutNode.Leaf("cell 1", LayoutSize.StretchedBoth())
                        ),
                        LayoutNode.OneOffParent("cell 2 parent", LayoutSize.StretchedBoth(), cellStyle,
                            LayoutNode.Leaf("cell 2", LayoutSize.StretchedBoth())
                        ),
                        LayoutNode.OneOffParent("cell 3 parent", LayoutSize.StretchedBoth(), cellStyle,
                            LayoutNode.Leaf("cell 3", LayoutSize.StretchedBoth())
                        )
                    ),
                    LayoutNode.HorizontalParent("row 2", LayoutSize.StretchedBoth(), LayoutStyle.Empty,
                        LayoutNode.OneOffParent("cell 4 parent", LayoutSize.StretchedBoth(), cellStyle,
                            LayoutNode.Leaf("cell 4", LayoutSize.StretchedBoth())
                        ),
                        LayoutNode.OneOffParent("cell 5 parent", LayoutSize.StretchedBoth(), cellStyle,
                            LayoutNode.Leaf("cell 5", LayoutSize.StretchedBoth())
                        ),
                        LayoutNode.OneOffParent("cell 6 parent", LayoutSize.StretchedBoth(), cellStyle,
                            LayoutNode.Leaf("cell 6", LayoutSize.StretchedBoth())
                        )
                    ),
                    LayoutNode.HorizontalParent("row 3", LayoutSize.StretchedBoth(), LayoutStyle.Empty,
                        LayoutNode.OneOffParent("cell 7 parent", LayoutSize.StretchedBoth(), cellStyle,
                            LayoutNode.Leaf("cell 7", LayoutSize.StretchedBoth())
                        ),
                        LayoutNode.OneOffParent("cell 8 parent", LayoutSize.StretchedBoth(), cellStyle,
                            LayoutNode.Leaf("cell 8", LayoutSize.StretchedBoth())
                        ),
                        LayoutNode.OneOffParent("cell 9 parent", LayoutSize.StretchedBoth(), cellStyle,
                            LayoutNode.Leaf("cell 9", LayoutSize.StretchedBoth())
                        )
                    )
                )
            ).Bake();

            var scene = SceneLayers.AddNewScene();
            var layoutActors = new LayoutActors(scene, layout, Point.Zero);

            var actorMap = new Dictionary<Slot, Actor>();

            for (var i = 1; i <= 9; i++)
            {
                var actor = layoutActors.GetActor($"cell {i}");
                actor.GetComponent<BoundingRect>().CenterToBounds();
                actorMap[Slot.All[i - 1]] = actor;
            }

            var dieActor = layoutActors.GetActor("dieBody");

            foreach (var actor in actorMap.Values)
            {
                new Hoverable(actor);
                new Clickable(actor);
                new Draggable(actor);
            }

            var gameDie = new Die();
            gameDie.Fill(Slot.TopCenter);
            gameDie.Fill(Slot.CenterCenter);
            gameDie.Fill(Slot.CenterLeft);

            new DieComponent(dieActor, actorMap, gameDie);
            new DieRenderer(dieActor);
        }

        public override void PrepareDynamicAssets(AssetLoader loader, MachinaRuntime runtime)
        {
        }
    }
}
