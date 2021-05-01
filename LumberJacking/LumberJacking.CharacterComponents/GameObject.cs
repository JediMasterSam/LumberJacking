using LumberJacking.GameObject.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacking.GameObject
{
    public class GameObject : GameComponent
    {
        public GameObject(Game game, params IComponent[] components) : base(game)
        {
            Components = components;
        }

        public Transform Transform { get; }

        private IComponent[] Components { get; }
    }
}
