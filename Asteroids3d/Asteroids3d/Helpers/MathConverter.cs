// Decompiled with JetBrains decompiler
// Type: Asteroids3d.Helpers.MathConverter
// Assembly: Asteroids3d, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F911F6E9-0655-4B3E-86CE-1C1BB515B56C
// Assembly location: C:\Users\Sam\Downloads\Asteroids3D\Debug\Asteroids3d.exe

using BEPUutilities;

namespace Asteroids3d.Helpers
{
  public static class MathConverter
  {
    public static Microsoft.Xna.Framework.Vector2 Convert(BEPUutilities.Vector2 bepuVector)
    {
      Microsoft.Xna.Framework.Vector2 vector2;
      vector2.X = bepuVector.X;
      vector2.Y = bepuVector.Y;
      return vector2;
    }

    public static void Convert(ref BEPUutilities.Vector2 bepuVector, out Microsoft.Xna.Framework.Vector2 xnaVector)
    {
      xnaVector.X = bepuVector.X;
      xnaVector.Y = bepuVector.Y;
    }

    public static BEPUutilities.Vector2 Convert(Microsoft.Xna.Framework.Vector2 xnaVector)
    {
      BEPUutilities.Vector2 vector2;
      vector2.X = xnaVector.X;
      vector2.Y = xnaVector.Y;
      return vector2;
    }

    public static void Convert(ref Microsoft.Xna.Framework.Vector2 xnaVector, out BEPUutilities.Vector2 bepuVector)
    {
      bepuVector.X = xnaVector.X;
      bepuVector.Y = xnaVector.Y;
    }

    public static Microsoft.Xna.Framework.Vector3 Convert(BEPUutilities.Vector3 bepuVector)
    {
      Microsoft.Xna.Framework.Vector3 vector3;
      vector3.X = bepuVector.X;
      vector3.Y = bepuVector.Y;
      vector3.Z = bepuVector.Z;
      return vector3;
    }

    public static void Convert(ref BEPUutilities.Vector3 bepuVector, out Microsoft.Xna.Framework.Vector3 xnaVector)
    {
      xnaVector.X = bepuVector.X;
      xnaVector.Y = bepuVector.Y;
      xnaVector.Z = bepuVector.Z;
    }

    public static BEPUutilities.Vector3 Convert(Microsoft.Xna.Framework.Vector3 xnaVector)
    {
      BEPUutilities.Vector3 vector3;
      vector3.X = xnaVector.X;
      vector3.Y = xnaVector.Y;
      vector3.Z = xnaVector.Z;
      return vector3;
    }

    public static void Convert(ref Microsoft.Xna.Framework.Vector3 xnaVector, out BEPUutilities.Vector3 bepuVector)
    {
      bepuVector.X = xnaVector.X;
      bepuVector.Y = xnaVector.Y;
      bepuVector.Z = xnaVector.Z;
    }

    public static Microsoft.Xna.Framework.Vector3[] Convert(BEPUutilities.Vector3[] bepuVectors)
    {
      Microsoft.Xna.Framework.Vector3[] vector3Array = new Microsoft.Xna.Framework.Vector3[bepuVectors.Length];
      for (int index = 0; index < bepuVectors.Length; ++index)
        MathConverter.Convert(ref bepuVectors[index], out vector3Array[index]);
      return vector3Array;
    }

    public static BEPUutilities.Vector3[] Convert(Microsoft.Xna.Framework.Vector3[] xnaVectors)
    {
      BEPUutilities.Vector3[] vector3Array = new BEPUutilities.Vector3[xnaVectors.Length];
      for (int index = 0; index < xnaVectors.Length; ++index)
        MathConverter.Convert(ref xnaVectors[index], out vector3Array[index]);
      return vector3Array;
    }

    public static Microsoft.Xna.Framework.Matrix Convert(BEPUutilities.Matrix matrix)
    {
      Microsoft.Xna.Framework.Matrix xnaMatrix;
      MathConverter.Convert(ref matrix, out xnaMatrix);
      return xnaMatrix;
    }

    public static BEPUutilities.Matrix Convert(Microsoft.Xna.Framework.Matrix matrix)
    {
      BEPUutilities.Matrix bepuMatrix;
      MathConverter.Convert(ref matrix, out bepuMatrix);
      return bepuMatrix;
    }

    public static void Convert(ref BEPUutilities.Matrix matrix, out Microsoft.Xna.Framework.Matrix xnaMatrix)
    {
      xnaMatrix.M11 = matrix.M11;
      xnaMatrix.M12 = matrix.M12;
      xnaMatrix.M13 = matrix.M13;
      xnaMatrix.M14 = matrix.M14;
      xnaMatrix.M21 = matrix.M21;
      xnaMatrix.M22 = matrix.M22;
      xnaMatrix.M23 = matrix.M23;
      xnaMatrix.M24 = matrix.M24;
      xnaMatrix.M31 = matrix.M31;
      xnaMatrix.M32 = matrix.M32;
      xnaMatrix.M33 = matrix.M33;
      xnaMatrix.M34 = matrix.M34;
      xnaMatrix.M41 = matrix.M41;
      xnaMatrix.M42 = matrix.M42;
      xnaMatrix.M43 = matrix.M43;
      xnaMatrix.M44 = matrix.M44;
    }

    public static void Convert(ref Microsoft.Xna.Framework.Matrix matrix, out BEPUutilities.Matrix bepuMatrix)
    {
      bepuMatrix.M11 = matrix.M11;
      bepuMatrix.M12 = matrix.M12;
      bepuMatrix.M13 = matrix.M13;
      bepuMatrix.M14 = matrix.M14;
      bepuMatrix.M21 = matrix.M21;
      bepuMatrix.M22 = matrix.M22;
      bepuMatrix.M23 = matrix.M23;
      bepuMatrix.M24 = matrix.M24;
      bepuMatrix.M31 = matrix.M31;
      bepuMatrix.M32 = matrix.M32;
      bepuMatrix.M33 = matrix.M33;
      bepuMatrix.M34 = matrix.M34;
      bepuMatrix.M41 = matrix.M41;
      bepuMatrix.M42 = matrix.M42;
      bepuMatrix.M43 = matrix.M43;
      bepuMatrix.M44 = matrix.M44;
    }

    public static Microsoft.Xna.Framework.Matrix Convert(Matrix3x3 matrix)
    {
      Microsoft.Xna.Framework.Matrix xnaMatrix;
      MathConverter.Convert(ref matrix, out xnaMatrix);
      return xnaMatrix;
    }

    public static void Convert(ref Matrix3x3 matrix, out Microsoft.Xna.Framework.Matrix xnaMatrix)
    {
      xnaMatrix.M11 = matrix.M11;
      xnaMatrix.M12 = matrix.M12;
      xnaMatrix.M13 = matrix.M13;
      xnaMatrix.M14 = 0.0f;
      xnaMatrix.M21 = matrix.M21;
      xnaMatrix.M22 = matrix.M22;
      xnaMatrix.M23 = matrix.M23;
      xnaMatrix.M24 = 0.0f;
      xnaMatrix.M31 = matrix.M31;
      xnaMatrix.M32 = matrix.M32;
      xnaMatrix.M33 = matrix.M33;
      xnaMatrix.M34 = 0.0f;
      xnaMatrix.M41 = 0.0f;
      xnaMatrix.M42 = 0.0f;
      xnaMatrix.M43 = 0.0f;
      xnaMatrix.M44 = 1f;
    }

    public static void Convert(ref Microsoft.Xna.Framework.Matrix matrix, out Matrix3x3 bepuMatrix)
    {
      bepuMatrix.M11 = matrix.M11;
      bepuMatrix.M12 = matrix.M12;
      bepuMatrix.M13 = matrix.M13;
      bepuMatrix.M21 = matrix.M21;
      bepuMatrix.M22 = matrix.M22;
      bepuMatrix.M23 = matrix.M23;
      bepuMatrix.M31 = matrix.M31;
      bepuMatrix.M32 = matrix.M32;
      bepuMatrix.M33 = matrix.M33;
    }

    public static Microsoft.Xna.Framework.Quaternion Convert(BEPUutilities.Quaternion quaternion)
    {
      Microsoft.Xna.Framework.Quaternion quaternion1;
      quaternion1.X = quaternion.X;
      quaternion1.Y = quaternion.Y;
      quaternion1.Z = quaternion.Z;
      quaternion1.W = quaternion.W;
      return quaternion1;
    }

    public static BEPUutilities.Quaternion Convert(Microsoft.Xna.Framework.Quaternion quaternion)
    {
      BEPUutilities.Quaternion quaternion1;
      quaternion1.X = quaternion.X;
      quaternion1.Y = quaternion.Y;
      quaternion1.Z = quaternion.Z;
      quaternion1.W = quaternion.W;
      return quaternion1;
    }

    public static void Convert(ref BEPUutilities.Quaternion bepuQuaternion, out Microsoft.Xna.Framework.Quaternion quaternion)
    {
      quaternion.X = bepuQuaternion.X;
      quaternion.Y = bepuQuaternion.Y;
      quaternion.Z = bepuQuaternion.Z;
      quaternion.W = bepuQuaternion.W;
    }

    public static void Convert(ref Microsoft.Xna.Framework.Quaternion quaternion, out BEPUutilities.Quaternion bepuQuaternion)
    {
      bepuQuaternion.X = quaternion.X;
      bepuQuaternion.Y = quaternion.Y;
      bepuQuaternion.Z = quaternion.Z;
      bepuQuaternion.W = quaternion.W;
    }

    public static BEPUutilities.Ray Convert(Microsoft.Xna.Framework.Ray ray)
    {
      BEPUutilities.Ray ray1;
      MathConverter.Convert(ref ray.Position, out ray1.Position);
      MathConverter.Convert(ref ray.Direction, out ray1.Direction);
      return ray1;
    }

    public static void Convert(ref Microsoft.Xna.Framework.Ray ray, out BEPUutilities.Ray bepuRay)
    {
      MathConverter.Convert(ref ray.Position, out bepuRay.Position);
      MathConverter.Convert(ref ray.Direction, out bepuRay.Direction);
    }

    public static Microsoft.Xna.Framework.Ray Convert(BEPUutilities.Ray ray)
    {
      Microsoft.Xna.Framework.Ray ray1;
      MathConverter.Convert(ref ray.Position, out ray1.Position);
      MathConverter.Convert(ref ray.Direction, out ray1.Direction);
      return ray1;
    }

    public static void Convert(ref BEPUutilities.Ray ray, out Microsoft.Xna.Framework.Ray xnaRay)
    {
      MathConverter.Convert(ref ray.Position, out xnaRay.Position);
      MathConverter.Convert(ref ray.Direction, out xnaRay.Direction);
    }

    public static Microsoft.Xna.Framework.BoundingBox Convert(BEPUutilities.BoundingBox boundingBox)
    {
      Microsoft.Xna.Framework.BoundingBox boundingBox1;
      MathConverter.Convert(ref boundingBox.Min, out boundingBox1.Min);
      MathConverter.Convert(ref boundingBox.Max, out boundingBox1.Max);
      return boundingBox1;
    }

    public static BEPUutilities.BoundingBox Convert(Microsoft.Xna.Framework.BoundingBox boundingBox)
    {
      BEPUutilities.BoundingBox boundingBox1;
      MathConverter.Convert(ref boundingBox.Min, out boundingBox1.Min);
      MathConverter.Convert(ref boundingBox.Max, out boundingBox1.Max);
      return boundingBox1;
    }

    public static void Convert(ref BEPUutilities.BoundingBox boundingBox, out Microsoft.Xna.Framework.BoundingBox xnaBoundingBox)
    {
      MathConverter.Convert(ref boundingBox.Min, out xnaBoundingBox.Min);
      MathConverter.Convert(ref boundingBox.Max, out xnaBoundingBox.Max);
    }

    public static void Convert(ref Microsoft.Xna.Framework.BoundingBox boundingBox, out BEPUutilities.BoundingBox bepuBoundingBox)
    {
      MathConverter.Convert(ref boundingBox.Min, out bepuBoundingBox.Min);
      MathConverter.Convert(ref boundingBox.Max, out bepuBoundingBox.Max);
    }

    public static Microsoft.Xna.Framework.BoundingSphere Convert(
      BEPUutilities.BoundingSphere boundingSphere)
    {
      Microsoft.Xna.Framework.BoundingSphere boundingSphere1;
      MathConverter.Convert(ref boundingSphere.Center, out boundingSphere1.Center);
      boundingSphere1.Radius = boundingSphere.Radius;
      return boundingSphere1;
    }

    public static BEPUutilities.BoundingSphere Convert(Microsoft.Xna.Framework.BoundingSphere boundingSphere)
    {
      BEPUutilities.BoundingSphere boundingSphere1;
      MathConverter.Convert(ref boundingSphere.Center, out boundingSphere1.Center);
      boundingSphere1.Radius = boundingSphere.Radius;
      return boundingSphere1;
    }

    public static void Convert(
      ref BEPUutilities.BoundingSphere boundingSphere,
      out Microsoft.Xna.Framework.BoundingSphere xnaBoundingSphere)
    {
      MathConverter.Convert(ref boundingSphere.Center, out xnaBoundingSphere.Center);
      xnaBoundingSphere.Radius = boundingSphere.Radius;
    }

    public static void Convert(
      ref Microsoft.Xna.Framework.BoundingSphere boundingSphere,
      out BEPUutilities.BoundingSphere bepuBoundingSphere)
    {
      MathConverter.Convert(ref boundingSphere.Center, out bepuBoundingSphere.Center);
      bepuBoundingSphere.Radius = boundingSphere.Radius;
    }

    public static Microsoft.Xna.Framework.Plane Convert(BEPUutilities.Plane plane)
    {
      Microsoft.Xna.Framework.Plane plane1;
      MathConverter.Convert(ref plane.Normal, out plane1.Normal);
      plane1.D = plane.D;
      return plane1;
    }

    public static BEPUutilities.Plane Convert(Microsoft.Xna.Framework.Plane plane)
    {
      BEPUutilities.Plane plane1;
      MathConverter.Convert(ref plane.Normal, out plane1.Normal);
      plane1.D = plane.D;
      return plane1;
    }

    public static void Convert(ref BEPUutilities.Plane plane, out Microsoft.Xna.Framework.Plane xnaPlane)
    {
      MathConverter.Convert(ref plane.Normal, out xnaPlane.Normal);
      xnaPlane.D = plane.D;
    }

    public static void Convert(ref Microsoft.Xna.Framework.Plane plane, out BEPUutilities.Plane bepuPlane)
    {
      MathConverter.Convert(ref plane.Normal, out bepuPlane.Normal);
      bepuPlane.D = plane.D;
    }
  }
}
