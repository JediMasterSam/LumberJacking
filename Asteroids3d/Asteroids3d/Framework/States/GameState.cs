// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.States.GameState
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Entities;
using Asteroids3d.Framework.Entities.Base;
using Asteroids3d.Framework.Scene;
using Asteroids3d.Framework.Sounds;
using Asteroids3d.Framework.States.Base;
using Asteroids3d.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Asteroids3d.Framework.States
{
  public class GameState : IState
  {
    public Level Level { get; private set; }

    public Hud Hud { get; }

    public Player Player { get; private set; }

    public bool IsActive { get; set; }

    private float Timer { get; set; }

    public GameState()
    {
      this.SetLevel(Difficulty.Expert);
      this.Hud = new Hud();
    }

    public void SetLevel(Difficulty difficulty)
    {
      SoundManager.Instance.StopAll();
      this.Level = new Level(difficulty);
      this.Player = new Player(this.Level);
      this.Level.Spawn<Player>(this.Player);
      this.IsActive = false;
      this.Level.Skybox.Target = (GameObject) this.Player;
      this.Timer = 600f;
    }

    public void Initialize()
    {
      Asteroids3D.Instance.IsMouseVisible = false;
      SoundManager.Instance.UnpauseAll();
      this.IsActive = true;
    }

    public void Update(GameTime gameTime)
    {
      if (Asteroids3D.Instance.StateManager.KeyboardState.IsKeyDown(Keys.Escape))
        Asteroids3D.Instance.StateManager.SetState(State.Paused);
      else if (this.Player.Health == 0 || (double) this.Timer <= 0.0)
      {
        this.IsActive = false;
        Asteroids3D.Instance.StateManager.SetState(State.GameOver);
      }
      else if (this.Player.AsteroidsDestroyed == this.Level.Goal)
      {
        this.IsActive = false;
        Asteroids3D.Instance.StateManager.SetState(State.Victory);
      }
      else
      {
        float deltaTime = (float) gameTime.ElapsedGameTime.Milliseconds / 1000f;
        this.Level.Update(deltaTime);
        this.Timer -= deltaTime;
        if ((double) this.Timer >= 0.0)
          return;
        this.Timer = 0.0f;
      }
    }

    public void Draw(GameTime gameTime)
    {
      this.Level.Draw(this.IsActive);
      if (!this.IsActive)
        return;
      Asteroids3D.Instance.SpriteBatch.Begin();
      this.Hud.Draw(Asteroids3D.Instance.SpriteBatch, (Ship) this.Player, this.Level, this.Timer);
      Asteroids3D.Instance.SpriteBatch.End();
    }
  }
}
