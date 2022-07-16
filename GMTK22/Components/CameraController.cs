using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Components
{
    public class CameraController : BaseComponent
    {
        private Vector2 mousePos;

        public CameraController(Actor actor) : base(actor)
        {
            this.actor.scene.camera.ZoomTarget = () => this.mousePos;
        }

        public override void OnMouseUpdate(Vector2 currentPosition, Vector2 positionDelta, Vector2 rawDelta)
        {
            this.mousePos = currentPosition;
        }

        public override void OnScroll(int scrollDelta)
        {
            this.actor.scene.camera.Zoom *= (1f + scrollDelta / 50f);
        }
    }
}
