using System;
using System.Collections.Generic;
using ExTween;
using ExTween.MonoGame;
using GMTK22.Data;
using Machina.Components;
using Machina.Data;
using Machina.Data.Layout;
using Machina.Data.TextRendering;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class BuildMenu : BaseComponent
    {
        public event Action<BuildingPosition, Command> RequestedBuilding;
        private readonly BoundingRect boundingRect;
        private readonly List<Actor> buttonActors = new List<Actor>();
        private readonly SequenceTween tween;
        private readonly TweenableVector2 positionOffsetTweenable;
        private readonly Vector2 startingPosition;

        public BuildMenu(Actor actor) : base(actor)
        {
            this.startingPosition = transform.Position;
            this.positionOffsetTweenable = new TweenableVector2(new Vector2(0, 500));
            this.tween = new SequenceTween()
                    .Add(new Tween<Vector2>(this.positionOffsetTweenable, Vector2.Zero, 0.25f, Ease.QuadFastSlow))
                ;
            this.boundingRect = RequireComponent<BoundingRect>();
        }

        public void AnimateShow()
        {
            this.positionOffsetTweenable.ForceSetValue(this.startingPosition);
            this.tween.Reset();
        }

        public override void Update(float dt)
        {
            this.tween.Update(dt);

            transform.Position = this.startingPosition + this.positionOffsetTweenable.Value;
        }

        public void PopulateButtons(Building building)
        {
            AnimateShow();
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
                leaves.Add(LayoutNode.Leaf($"{command.NameAndDescription} LayoutNode", buttonSize));
            }

            var layout = FlowLayout.HorizontalFlowParent("BuildCommands",
                LayoutSize.Pixels(this.boundingRect.Rect.Size),
                new FlowLayoutStyle(paddingBetweenItemsInEachRow: 10, margin: new Point(10, 10),
                    alignment: Alignment.Center),
                leaves.ToArray()
            ).Bake();

            var hotkeys = new Keys[]
            {
                Keys.Q,
                Keys.W,
                Keys.E,
                Keys.R,
                Keys.T,
                Keys.Y,
                Keys.U,
            };
            
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
                    new BuildMenuButton(child, command, building, this, hotkeys[i]);
                    
                    if(command is ConstructBuildingCommand constructCommand)
                    {
                        new ConstructionCommandRenderer(child, constructCommand.spec, hotkeys[i].ToString()[0]);
                    }
                    else
                    {
                        new NormalCommandRenderer(child, command, hotkeys[i].ToString()[0]);
                    }
                    
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

        public Command GetHoveredCommand()
        {
            foreach (var button in this.buttonActors)
            {
                if (button.GetComponent<Hoverable>().IsHovered)
                {
                    return button.GetComponent<BuildMenuButton>().Command;
                }
            }

            return null;
        }

        public void RequestBuilding(BuildingPosition buildingGridPosition, Command command)
        {
            RequestedBuilding?.Invoke(buildingGridPosition, command);
        }
    }
}
