using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace LumberJacking.Input
{
    public sealed class InputManager
    {
        public InputManager()
        {
            Bindings = new Dictionary<Keys, PlayerAction>
            {
                {Keys.W, PlayerAction.Forward},
                {Keys.S, PlayerAction.Backward},
                {Keys.A, PlayerAction.Left},
                {Keys.D, PlayerAction.Right},
                {Keys.Left, PlayerAction.RotateLeft},
                {Keys.Right, PlayerAction.RotateRight},
                {Keys.LeftShift, PlayerAction.Run},
                {Keys.Space, PlayerAction.Attack}
            };
        }

        public Dictionary<Keys, PlayerAction> Bindings { get; }

        private PlayerAction PlayerAction { get; set; }

        public void Update()
        {
            var current = PlayerAction.None;

            foreach (var pressedKey in Keyboard.GetState().GetPressedKeys())
            {
                if (Bindings.TryGetValue(pressedKey, out var playerAction))
                {
                    current |= playerAction;
                }
            }

            PlayerAction = current;
        }

        public bool IsActive(PlayerAction playerAction)
        {
            return (PlayerAction & playerAction) != 0;
        }
    }
}