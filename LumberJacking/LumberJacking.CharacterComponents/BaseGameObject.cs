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
                { typeof(CustomMeshRenderer), new CustomMeshRenderer(this) }
            };

            Transform = GetComponent<Transform>();
            MeshRenderer = GetComponent<CustomMeshRenderer>();
        }

        public Transform Transform { get; }
        public CustomMeshRenderer MeshRenderer { get; }
        public bool Drawable { get; set; }

        private Dictionary<Type, Component> Components { get; }

        public T GetComponent<T>() where T : Component
        {
            return Components.TryGetValue(typeof(T), out var component) ? (T)component : null;
        }

        public T AddComponent<T>(BaseGameObject parent) where T : Component, new()
        {
            var component = new T
            {
                GameObject = parent
            };
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

            VertexBuffer vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 8, BufferUsage.None);

            vertexBuffer.SetData(MeshRenderer.Verticies);

            IndexBuffer lineListIndexBuffer = new IndexBuffer(
                GraphicsDevice,
                IndexElementSize.SixteenBits,
                sizeof(short) * MeshRenderer.TriangleIndices.Length,
                BufferUsage.None);

            lineListIndexBuffer.SetData(MeshRenderer.TriangleIndices);

            GraphicsDevice.Indices = lineListIndexBuffer;
            GraphicsDevice.SetVertexBuffer(vertexBuffer);
            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, MeshRenderer.TriangleIndices.Length / 3);

            base.Draw(gameTime);
        }
    }
}
