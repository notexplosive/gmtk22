using System;
using System.Collections.Generic;
using GMTK22.Data;
using Machina.Components;
using Machina.Data;
using Machina.Data.Layout;
using Machina.Data.TextRendering;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class BuildMenu : BaseComponent
    {
        public event Action<BuildingPosition, Command> RequestedBuilding;
        private readonly BoundingRect boundingRect;
        private readonly List<Actor> buttonActors = new List<Actor>();

        public BuildMenu(Actor actor) : base(actor)
        {
            this.boundingRect = RequireComponent<BoundingRect>();
        }

        public void PopulateButtons(Building building)
        {
            var commandsFromBuilding = building.Commands();
            var commands = new List<Command>();

            var isSellable = building is IHasSpec;
            
            if (isSellable)
            {
                commands.Add(new SellCommand(building));
            }

            commands.AddRange(commandsFromBuilding);

            ClearButtons();
            
            var leaves = new List<FlowLayout.LayoutNodeOrInstruction>();
            var buttonSize = LayoutSize.Pixels(128, 128);
            
            foreach (var command in commands)
            {
                leaves.Add(LayoutNode.Leaf($"{command.Name} LayoutNode", buttonSize));
            }

            var layout = FlowLayout.HorizontalFlowParent("BuildCommands",
                LayoutSize.Pixels(this.boundingRect.Rect.Size),
                new FlowLayoutStyle(paddingBetweenItemsInEachRow: 10, margin: new Point(10, 10),
                    alignment: Alignment.Center),
                leaves.ToArray()
            ).Bake();

            foreach (var row in layout.Rows)
            {
                for (var i = 0; i < row.ItemCount; i++)
                {
                    var node = row.GetItemNode(i);
                    var command = commands[i];
                    
                    var child = this.actor.transform.AddActorAsChild("Build command cell",
                        node.PositionRelativeToRoot.ToVector2());
                    this.buttonActors.Add(child);
                    
                    child.transform.LocalDepth -= 10;
                    new BoundingRect(child, node.Size);
                    new Hoverable(child);
                    new Clickable(child);
                    new BuildMenuButton(child, command, building, this);
                    new BuildMenuButtonRenderer(child, command);
                }
            }
        }

        public void ClearButtons()
        {
            foreach (var button in this.buttonActors)
            {
                button.Destroy();
            }
            
            this.buttonActors.Clear();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var rect = this.boundingRect.Rect;

            spriteBatch.FillRectangle(rect, Color.Black, transform.Depth);
            spriteBatch.DrawRectangle(rect, Color.White, 2f, transform.Depth - 1);
        }

        public void RequestBuilding(BuildingPosition buildingGridPosition, Command command)
        {
            RequestedBuilding?.Invoke(buildingGridPosition, command);
        }
    }
}
