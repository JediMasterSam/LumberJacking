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
                {typeof(Transform), new Transform(this)},
                {typeof(CustomMeshRenderer), new CustomMeshRenderer(this, GraphicsDevice)}
            };

            Transform = GetComponent<Transform>();
            MeshRenderer = GetComponent<CustomMeshRenderer>();
            Drawable = true;
        }

        public Transform Transform { get; }
        public CustomMeshRenderer MeshRenderer { get; }
        public bool Drawable { get; set; }

        private Dictionary<Type, Component> Components { get; }

        public T GetComponent<T>() where T : Component
        {
            return Components.TryGetValue(typeof(T), out var component) ? (T) component : null;
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
            foreach (var component in Components)
            {
                component.Value.UpdateComponent(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Drawable) return;

            ////////////////////////////////////////////////////////

            var basicEffect = MeshRenderer.Effect;

            // These three lines are required if you use SpriteBatch, to reset the states that it sets
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            // Transform your model to place it somewhere in the world
            // basicEffect.World = Matrix.CreateRotationZ(MathHelper.PiOver4) * Matrix.CreateTranslation(0.5f, 0, 0); // for sake of example
            basicEffect.World = Matrix.Identity; // Use this to leave your model at the origin
            // Transform the entire world around (effectively: place the camera)
            basicEffect.View = LumberJackingGame.Instance.Camera.View;
            // Specify how 3D points are projected/transformed onto the 2D screen
            basicEffect.Projection = LumberJackingGame.Instance.Camera.Projection;

            // Tell BasicEffect to make use of your vertex colors
            basicEffect.VertexColorEnabled = false;
            basicEffect.Texture = MeshRenderer.Texture;
            basicEffect.TextureEnabled = true;
            // I'm setting this so that *both* sides of your triangle are drawn
            // (so it won't be back-face culled if you move it, or the camera around behind it)
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // Render with a BasicEffect that was created in LoadContent
            // (BasicEffect only has one pass - but effects in general can have many rendering passes)
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                // This is the all-important line that sets the effect, and all of its settings, on the graphics device
                pass.Apply();
                // var vertexBuffer = new VertexBuffer(GraphicsDevice, new VertexDeclaration(new []{new VertexElement()}))
                // GraphicsDevice.SetVertexBuffer();
                GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, MeshRenderer.Verticies, 0, 4, MeshRenderer.TriangleIndices, 0, 2);
                // Here's your code:
                // VertexPositionColor[] vertices = new VertexPositionColor[3];
                // vertices[0].Position = new Vector3(-0.5f, -0.5f, 0f);
                // vertices[0].Color = Color.Red;
                // vertices[1].Position = new Vector3(0, 0.5f, 0f);
                // vertices[1].Color = Color.Green;
                // vertices[2].Position = new Vector3(0.5f, -0.5f, 0f);
                // vertices[2].Color = Color.Yellow;
                // GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertices, 0, 1);
            }

            // These three lines are required if you use SpriteBatch, to reset the states that it sets
            //GraphicsDevice.BlendState = BlendState.Opaque;
            //GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            //GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            //// Transform your model to place it somewhere in the world
            //basicEffect.World = Matrix.Identity; //Matrix.CreateRotationZ(MathHelper.PiOver4) * Matrix.CreateTranslation(0.5f, 0, 0); // for sake of example
            //                                                                                                       //basicEffect.World = Matrix.Identity; // Use this to leave your model at the origin
            //                                                                                                       // Transform the entire world around (effectively: place the camera)
            //basicEffect.View = LumberJackingGame.Instance.Camera.View;
            //// Specify how 3D points are projected/transformed onto the 2D screen
            //basicEffect.Projection = LumberJackingGame.Instance.Camera.Projection;

            //// Tell BasicEffect to make use of your vertex colors
            //basicEffect.VertexColorEnabled = true;
            //// I'm setting this so that *both* sides of your triangle are drawn
            //// (so it won't be back-face culled if you move it, or the camera around behind it)
            //GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            //// Render with a BasicEffect that was created in LoadContent
            //// (BasicEffect only has one pass - but effects in general can have many rendering passes)
            //foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            //{
            //    // This is the all-important line that sets the effect, and all of its settings, on the graphics device
            //    pass.Apply();

            //    // Here's our code:
            //    //VertexBuffer vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), MeshRenderer.Verticies.Length, BufferUsage.None);

            //    //vertexBuffer.SetData(MeshRenderer.Verticies);

            //    //IndexBuffer triangleIndexBuffer = new IndexBuffer(
            //    //    GraphicsDevice,
            //    //    IndexElementSize.SixteenBits,
            //    //    sizeof(short) * MeshRenderer.TriangleIndices.Length,
            //    //    BufferUsage.None);

            //    //triangleIndexBuffer.SetData(MeshRenderer.TriangleIndices);

            //    //GraphicsDevice.Indices = triangleIndexBuffer;
            //    //GraphicsDevice.SetVertexBuffer(vertexBuffer);
            //    //GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, MeshRenderer.TriangleIndices.Length / 3);

            //    // Here's the example code
            //    VertexPositionColor[] vertices = new VertexPositionColor[3];
            //    vertices[0].Position = new Vector3(-0.5f, -0.5f, 0f);
            //    vertices[0].Color = Color.Red;
            //    vertices[1].Position = new Vector3(0, 0.5f, 0f);
            //    vertices[1].Color = Color.Green;
            //    vertices[2].Position = new Vector3(0.5f, -0.5f, 0f);
            //    vertices[2].Color = Color.Yellow;
            //    GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertices, 0, 1);
            //}

            ////////////////////////////////////////////////////////

            base.Draw(gameTime);
        }
    }
}