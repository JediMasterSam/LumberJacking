// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Sounds.Sound
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Asteroids3d.Framework.Sounds
{
  public class Sound
  {
    private SoundEffect SoundEffect { get; }

    private Queue<SoundEffectInstance> Instances { get; }

    public string Name { get; }

    public SoundState State { get; private set; }

    public Sound(string name)
    {
      this.Name = name;
      this.SoundEffect = Asteroids3D.Instance.Content.Load<SoundEffect>("Sounds/" + name);
      this.Instances = new Queue<SoundEffectInstance>();
      this.State = SoundState.Stopped;
    }

    private void Update()
    {
      if (this.Instances.Count == 0)
        return;
      while (this.Instances.Peek().State == SoundState.Stopped)
        this.Instances.Dequeue();
    }

    public void Play(bool loop = false, float volume = 1f)
    {
      SoundEffectInstance instance = this.SoundEffect.CreateInstance();
      instance.IsLooped = loop;
      instance.Volume = volume;
      this.Instances.Enqueue(instance);
      if (this.State == SoundState.Paused)
        return;
      instance.Play();
      this.State = SoundState.Playing;
      this.Update();
    }

    public void Pause()
    {
      if ((uint) this.State > 0U)
        return;
      foreach (SoundEffectInstance instance in this.Instances)
        instance.Pause();
      this.State = SoundState.Paused;
    }

    public void Unpause()
    {
      if (this.State != SoundState.Paused)
        return;
      foreach (SoundEffectInstance instance in this.Instances)
        instance.Resume();
      this.State = SoundState.Playing;
    }

    public void Stop()
    {
      if (this.State == SoundState.Stopped)
        return;
      foreach (SoundEffectInstance instance in this.Instances)
        instance.Stop();
      this.State = SoundState.Stopped;
      this.Instances.Clear();
    }
  }
}
