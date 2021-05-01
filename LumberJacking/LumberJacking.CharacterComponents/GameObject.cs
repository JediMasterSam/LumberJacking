using LumberJacking.GameObject.Components;
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
        public GameObject(Game game) : base(game)
        {
            Components = new Dictionary<Type, IComponent>
            {
                { typeof(Transform), new Transform() }
            };
        }

        public Transform Transform { get => GetComponent<Transform>(); }

        private Dictionary<Type, IComponent> Components { get; }

        public T GetComponent<T>()
        {
            return (T)Components[typeof(T)];
        }

        public void AddComponent<T>(T component) where T : IComponent
        {
            Components.Add(typeof(T), component);
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var component in Components)
            {
                component.Value.UpdateComponent(gameTime);
            }

            base.Update(gameTime);
        }
    }
}
