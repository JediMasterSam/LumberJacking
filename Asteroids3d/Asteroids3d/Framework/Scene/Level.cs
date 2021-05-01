// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Framework.Scene.Level
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using Asteroids3d.Framework.Entities;
using Asteroids3d.Framework.Entities.Base;
using Asteroids3d.Framework.States.Base;
using BEPUphysics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids3d.Framework.Scene
{
  public class Level
  {
    private readonly List<Asteroid> _asteroids;
    private readonly List<Laser> _lasers;
    private readonly List<Ship> _ships;

    private Queue<Asteroid> AsteroidSpawner { get; }

    private Queue<Laser> LaserSpawner { get; }

    private Queue<Ship> ShipSpawner { get; }

    private Queue<Asteroid> AsteroidDestroyer { get; }

    private Queue<Laser> LaserDestroyer { get; }

    private Queue<Ship> ShipDestroyer { get; }

    public IReadOnlyList<Asteroid> Asteroids { get; }

    public IReadOnlyList<Laser> Lasers { get; }

    public IReadOnlyList<Ship> Ships { get; }

    public Camera Camera { get; set; }

    public World World { get; }

    public Space Space { get; }

    public Skybox Skybox { get; }

    public int Goal { get; }

    public Difficulty Difficulty { get; }

    private Random Random { get; }

    public Level(Difficulty difficulty)
    {
      this.Random = new Random();
      this._asteroids = new List<Asteroid>();
      this._lasers = new List<Laser>();
      this._ships = new List<Ship>();
      this.AsteroidSpawner = new Queue<Asteroid>();
      this.LaserSpawner = new Queue<Laser>();
      this.ShipSpawner = new Queue<Ship>();
      this.AsteroidDestroyer = new Queue<Asteroid>();
      this.LaserDestroyer = new Queue<Laser>();
      this.ShipDestroyer = new Queue<Ship>();
      this.Asteroids = (IReadOnlyList<Asteroid>) this._asteroids.AsReadOnly();
      this.Lasers = (IReadOnlyList<Laser>) this._lasers.AsReadOnly();
      this.Ships = (IReadOnlyList<Ship>) this._ships.AsReadOnly();
      this.Camera = new Camera(Vector3.Zero);
      this.Space = new Space();
      this.Skybox = new Skybox(this);
      int total;
      switch (difficulty)
      {
        case Difficulty.Beginner:
          this.World = new World(200f, 15f);
          this.Goal = 10;
          total = 20;
          break;
        case Difficulty.Easy:
          this.World = new World(200f, 15f);
          this.Goal = 20;
          total = 50;
          break;
        case Difficulty.Medium:
          this.World = new World(300f, 10f);
          this.Goal = 30;
          total = 100;
          break;
        case Difficulty.Hard:
          this.World = new World(400f, 5f);
          this.Goal = 50;
          total = 300;
          break;
        case Difficulty.Expert:
          this.World = new World(500f, 5f);
          this.Goal = 100;
          total = 500;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (difficulty), (object) difficulty, (string) null);
      }
      this.Difficulty = difficulty;
      this.SpawnAsteroids(total);
    }

    public void SpawnAsteroids(int total, bool spawnOnEdge = false)
    {
      for (int index = 0; index < total; ++index)
      {
        double num = this.Random.NextDouble();
        this.Spawn<Asteroid>(new Asteroid(num <= 0.5 ? 1 : (num <= 0.75 ? 2 : 3), this, spawnOnEdge));
      }
    }

    public void Spawn<T>(T gameObject) where T : GameObject
    {
      Type type = gameObject.GetType();
      if (type == typeof (Asteroid))
        this.AsteroidSpawner.Enqueue((object) gameObject as Asteroid);
      else if (type == typeof (Laser))
        this.LaserSpawner.Enqueue((object) gameObject as Laser);
      else if (type == typeof (Ship))
      {
        this.ShipSpawner.Enqueue((object) gameObject as Ship);
      }
      else
      {
        if (!(type == typeof (Player)))
          return;
        this.ShipSpawner.Enqueue((Ship) ((object) gameObject as Player));
      }
    }

    public void Destroy<T>(T gameObject) where T : GameObject
    {
      Type type = gameObject.GetType();
      if (type == typeof (Asteroid))
        this.AsteroidDestroyer.Enqueue((object) gameObject as Asteroid);
      else if (type == typeof (Laser))
        this.LaserDestroyer.Enqueue((object) gameObject as Laser);
      else if (type == typeof (Ship))
      {
        this.ShipDestroyer.Enqueue((object) gameObject as Ship);
      }
      else
      {
        if (!(type == typeof (Player)))
          return;
        this.ShipDestroyer.Enqueue((Ship) ((object) gameObject as Player));
      }
    }

    public void Update(float deltaTime)
    {
      this.SpawnAll();
      this.DestroyAll();
      this.Space.Update(deltaTime);
      this._asteroids.ForEach((Action<Asteroid>) (asteroid => asteroid.Update(deltaTime)));
      this._lasers.ForEach((Action<Laser>) (laser => laser.Update(deltaTime)));
      this._ships.ForEach((Action<Ship>) (ship => ship.Update(deltaTime)));
      this.Camera.Update();
    }

    public void Draw(bool isActive)
    {
      this.Skybox.Draw(this.Camera);
      if (!isActive)
        return;
      this._asteroids.ForEach((Action<Asteroid>) (asteroid => asteroid.Draw(this.Camera)));
      this._lasers.ForEach((Action<Laser>) (laser => laser.Draw(this.Camera)));
      this._ships.ForEach((Action<Ship>) (ship => ship.Draw(this.Camera)));
    }

    public void StopPhysics(GameObject gameObject) => this.Space.Remove<GameObject>(gameObject.PhysicsObject);

    private void SpawnAll()
    {
      this.SpawnAll<Asteroid>(this.AsteroidSpawner, (ICollection<Asteroid>) this._asteroids);
      this.SpawnAll<Laser>(this.LaserSpawner, (ICollection<Laser>) this._lasers);
      this.SpawnAll<Ship>(this.ShipSpawner, (ICollection<Ship>) this._ships);
    }

    private void DestroyAll()
    {
      this.DestroyAll<Asteroid>(this.AsteroidDestroyer, (ICollection<Asteroid>) this._asteroids);
      this.DestroyAll<Laser>(this.LaserDestroyer, (ICollection<Laser>) this._lasers);
      this.DestroyAll<Ship>(this.ShipDestroyer, (ICollection<Ship>) this._ships);
    }

    public bool IsSafeToSpawn(BoundingSphere boundingSphere) => this._asteroids.All<Asteroid>((Func<Asteroid, bool>) (asteroid => asteroid.PhysicsObject.BoundingSphere.Contains(boundingSphere) == ContainmentType.Disjoint)) && this._lasers.All<Laser>((Func<Laser, bool>) (laser => laser.PhysicsObject.BoundingSphere.Contains(boundingSphere) == ContainmentType.Disjoint)) && this._ships.All<Ship>((Func<Ship, bool>) (ship => ship.PhysicsObject.BoundingSphere.Contains(boundingSphere) == ContainmentType.Disjoint));

    private void SpawnAll<T>(Queue<T> queue, ICollection<T> list) where T : GameObject
    {
      while (queue.Count > 0)
      {
        T obj = queue.Dequeue();
        list.Add(obj);
        this.Space.Add<GameObject>(obj.PhysicsObject);
      }
    }

    private void DestroyAll<T>(Queue<T> queue, ICollection<T> list) where T : GameObject
    {
      while (queue.Count > 0)
      {
        T obj = queue.Dequeue();
        if (list.Remove(obj))
          this.Space.Remove<GameObject>(obj.PhysicsObject);
      }
    }
  }
}
