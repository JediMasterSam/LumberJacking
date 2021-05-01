// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Sounds.SoundManager
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Asteroids3d.Framework.Sounds
{
  public class SoundManager
  {
    private static SoundManager _soundManager;

    public static SoundManager Instance => SoundManager._soundManager ?? (SoundManager._soundManager = new SoundManager());

    private Dictionary<string, Sound> Sounds { get; }

    private SoundManager() => this.Sounds = new Dictionary<string, Sound>();

    public void Play(string name, bool loop = false, float volume = 1f)
    {
      Sound sound;
      if (!this.Sounds.ContainsKey(name) || !this.Sounds.TryGetValue(name, out sound))
      {
        sound = new Sound(name);
        this.Sounds[name] = sound;
      }
      sound.Play(loop, volume);
    }

    public void Pause(string name)
    {
      Sound sound;
      if (!this.Sounds.TryGetValue(name, out sound))
        return;
      sound.Pause();
    }

    public void Unpause(string name)
    {
      Sound sound;
      if (!this.Sounds.TryGetValue(name, out sound))
        return;
      sound.Unpause();
    }

    public void Stop(string name)
    {
      Sound sound;
      if (!this.Sounds.TryGetValue(name, out sound))
        return;
      sound.Stop();
    }

    public SoundState State(string name)
    {
      Sound sound;
      return this.Sounds.TryGetValue(name, out sound) ? sound.State : SoundState.Stopped;
    }

    public void PauseAll()
    {
      foreach (KeyValuePair<string, Sound> sound in this.Sounds)
        sound.Value.Pause();
    }

    public void UnpauseAll()
    {
      foreach (KeyValuePair<string, Sound> sound in this.Sounds)
        sound.Value.Unpause();
    }

    public void StopAll()
    {
      foreach (KeyValuePair<string, Sound> sound in this.Sounds)
        sound.Value.Stop();
    }
  }
}
