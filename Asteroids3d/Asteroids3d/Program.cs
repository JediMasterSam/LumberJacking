// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Program
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using System;
using System.Diagnostics;

namespace Asteroids3d
{
  public static class Program
  {
    [STAThread]
    private static void Main()
    {
      try
      {
        using (Asteroids3D instance = Asteroids3D.Instance)
          instance.Run();
      }
      catch (Exception ex)
      {
        Debug.WriteLine((object) ex);
      }
    }
  }
}
