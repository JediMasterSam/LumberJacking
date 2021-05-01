using LumberJacking.GameObject.Components;
using LumberJacking.GameObject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacking.GameObject
{
    public abstract class BaseGameObject : DrawableGameComponent
    {
        public BaseGameObject(Game game) : base(game)
        {
            Components = new Dictionary<Type, Component>
            {
                { typeof(Transform), new Transform(this) },
                { typeof(MeshRenderer), new MeshRenderer(this) }
            };

            Transform = GetComponent<Transform>();
            MeshRenderer = GetComponent<MeshRenderer>();
        }

        public Transform Transform { get; }
        public MeshRenderer MeshRenderer { get; }
        public bool Drawable { get; set; }

        private Dictionary<Type, Component> Components { get; }

        public T GetComponent<T>() where T : Component
        {
            return Components.TryGetValue(typeof(T), out var component) ? (T)component : null;
        }

        public T AddComponent<T>() where T : Component, new()
        {
            var component = new T();
            return Components.TryAdd(typeof(T), component) ? component : null;
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var component in Components)
            {
                component.Value.UpdateComponent(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Drawable) return;

            foreach (ModelMesh mesh in MeshRenderer.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = Matrix.CreateTranslation(new Vector3(0, 0, 0));
                    effect.View = Matrix.CreateLookAt(new Vector3(5, 5, 5), Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), 1.6f, 0.1f, 10000.0f);
                    effect.EnableDefaultLighting();
                }

                mesh.Draw();
            }

            base.Draw(gameTime);
        }
    }
}
