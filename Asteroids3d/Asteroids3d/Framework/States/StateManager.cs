// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.States.StateManager
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Input;
using Asteroids3d.Framework.Sounds;
using Asteroids3d.Framework.States.Base;
using Asteroids3d.Framework.States.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Asteroids3d.Framework.States
{
  public class StateManager
  {
    public IState CurrentState { get; private set; }

    public KeyboardState KeyboardState { get; private set; }

    private MainMenuState MainMenuState { get; }

    private GameState GameState { get; }

    private PausedState PausedState { get; }

    private GameOverState GameOverState { get; }

    private DifficultyState DifficultyState { get; }

    private VictoryState VictoryState { get; }

    private bool IsGameState { get; set; }

    private Difficulty Difficulty { get; set; }

    public StateManager()
    {
      this.MainMenuState = new MainMenuState();
      this.GameState = new GameState();
      this.PausedState = new PausedState();
      this.GameOverState = new GameOverState();
      this.DifficultyState = new DifficultyState();
      this.VictoryState = new VictoryState();
      this.SetState(Asteroids3d.Framework.States.Base.State.MainMenu);
      this.Difficulty = Difficulty.Medium;
    }

    public void SetState(Asteroids3d.Framework.States.Base.State stateName)
    {
      this.IsGameState = false;
      switch (stateName)
      {
        case Asteroids3d.Framework.States.Base.State.MainMenu:
          this.CurrentState = (IState) this.MainMenuState;
          this.GameState.IsActive = false;
          break;
        case Asteroids3d.Framework.States.Base.State.Game:
          if (!this.GameState.IsActive)
            this.GameState.SetLevel(this.Difficulty);
          this.CurrentState = (IState) this.GameState;
          this.IsGameState = true;
          break;
        case Asteroids3d.Framework.States.Base.State.Paused:
          this.CurrentState = (IState) this.PausedState;
          break;
        case Asteroids3d.Framework.States.Base.State.Exit:
          SoundManager.Instance.StopAll();
          Asteroids3D.Instance.Exit();
          return;
        case Asteroids3d.Framework.States.Base.State.GameOver:
          this.CurrentState = (IState) this.GameOverState;
          break;
        case Asteroids3d.Framework.States.Base.State.Victory:
          this.CurrentState = (IState) this.VictoryState;
          break;
        case Asteroids3d.Framework.States.Base.State.Difficulty:
          this.CurrentState = (IState) this.DifficultyState;
          break;
        case Asteroids3d.Framework.States.Base.State.Beginner:
          this.Difficulty = Difficulty.Beginner;
          this.GameState.SetLevel(this.Difficulty);
          this.CurrentState = (IState) this.MainMenuState;
          break;
        case Asteroids3d.Framework.States.Base.State.Easy:
          this.Difficulty = Difficulty.Easy;
          this.GameState.SetLevel(this.Difficulty);
          this.CurrentState = (IState) this.MainMenuState;
          break;
        case Asteroids3d.Framework.States.Base.State.Medium:
          this.Difficulty = Difficulty.Medium;
          this.GameState.SetLevel(this.Difficulty);
          this.CurrentState = (IState) this.MainMenuState;
          break;
        case Asteroids3d.Framework.States.Base.State.Hard:
          this.Difficulty = Difficulty.Hard;
          this.GameState.SetLevel(this.Difficulty);
          this.CurrentState = (IState) this.MainMenuState;
          break;
        case Asteroids3d.Framework.States.Base.State.Expert:
          this.Difficulty = Difficulty.Expert;
          this.GameState.SetLevel(this.Difficulty);
          this.CurrentState = (IState) this.MainMenuState;
          break;
        case Asteroids3d.Framework.States.Base.State.Play:
          this.GameState.SetLevel(this.Difficulty);
          this.CurrentState = (IState) this.GameState;
          this.IsGameState = true;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (stateName), (object) stateName, (string) null);
      }
      this.CurrentState.Initialize();
    }

    public void Update(GameTime gameTime)
    {
      if (this.IsGameState)
        MouseController.Instance.Update();
      this.KeyboardState = Keyboard.GetState();
      this.CurrentState.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
      if (!this.IsGameState)
        this.GameState.Draw(gameTime);
      this.CurrentState.Draw(gameTime);
    }
  }
}
