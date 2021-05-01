using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using LumberJacking.Geometry;
using Microsoft.Xna.Framework;

namespace LumberJacking.World
{
    public sealed class Level
    {
        public Level(int level = 1)
        {
            var cellTypes = GetLevel(level);
            var width = cellTypes.GetLength(0);
            var height = cellTypes.GetLength(1);

            var corners = GetCorners(cellTypes, width, height);
            var walls = new List<Line>(corners.Count);

            for (var index = 0; index < corners.Count; index++)
            {
                walls.Add(new Line(corners[index].ToVector2(), corners[index == corners.Count - 1 ? 0 : index + 1].ToVector2()));
            }

            var markers = new List<Marker>();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var cellType = cellTypes[x, y];
                    if (cellType != CellType.Empty && cellType != CellType.Wall)
                    {
                        markers.Add(new Marker {CellType = cellType, Position = new Vector2(x, y)});
                    }
                }
            }

            Walls = walls;
            Markers = markers;
        }

        public List<Line> Walls { get; }

        public List<Marker> Markers { get; }

        public static CellType[,] GetLevel(in int level)
        {
            var lines = File.ReadAllLines($"./Content/level_{level}.txt");
            var width = lines.Max(line => line.Length);
            var height = lines.Length;
            var cellTypes = new CellType[width, height];

            for (var y = 0; y < height; y++)
            {
                var line = lines[y];

                for (var x = 0; x < line.Length; x++)
                {
                    cellTypes[x, y] = line[x] switch
                    {
                        ' ' => CellType.Empty,
                        '|' or '-' => CellType.Wall,
                        'S' => CellType.Spawn,
                        _ => throw new Exception($"Unknown character: {line[x]}")
                    };
                }
            }

            return cellTypes;
        }

        public static void PrintLevel(int level)
        {
            var cellTypes = GetLevel(level);

            var width = cellTypes.GetLength(0);
            var height = cellTypes.GetLength(1);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var cellType = cellTypes[x, y];

                    Debug.Write(cellType == CellType.Empty ? " " : (int) cellType);
                }

                Debug.Write("\n");
            }
        }

        public static void PrintPath(int level)
        {
            var cellTypes = GetLevel(level);
            var width = cellTypes.GetLength(0);
            var height = cellTypes.GetLength(1);

            foreach (var corner in GetCorners(cellTypes, width, height))
            {
                Debug.WriteLine(corner);
            }
        }

        public static List<Point> GetCorners(in CellType[,] cellTypes, in int width, in int height)
        {
            var start = Point.Zero;
            var foundStart = false;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (cellTypes[x, y] != CellType.Wall) continue;
                    start = new Point(x, y);
                    foundStart = true;
                    break;
                }

                if (foundStart) break;
            }

            if (!foundStart)
            {
                throw new Exception("The specified cell types does not contain any walls.");
            }

            var directions = new[]
            {
                new Point(-1, 0),
                new Point(0, -1),
                new Point(1, 0),
                new Point(0, 1)
            };

            var next = new[] {1, 2, 3, 0};
            var inverses = new[] {2, 3, 0, 1};

            var index = 0;
            var current = start;
            var triplet = new[] {0, 0, 2};

            var corners = new List<Point> {start};

            do
            {
                var foundWall = false;

                for (var i = 0; i < 4; i++)
                {
                    index = next[index];

                    var (x, y) = current + directions[index];

                    if (x < 0 || x >= width || y < 0 || y >= height || cellTypes[x, y] != CellType.Wall) continue;

                    foundWall = true;
                    break;
                }

                if (!foundWall)
                {
                    throw new Exception("The specified cell types does not contain an enclosed wall.");
                }

                current += directions[index];

                triplet[0] = triplet[1];
                triplet[1] = triplet[2];
                triplet[2] = index;

                if (triplet[0] == triplet[1] && triplet[1] == triplet[2])
                {
                    corners[^1] = current;
                }
                else
                {
                    corners.Add(current);
                }

                index = inverses[index];
            }
            while (current != start);

            return corners;
        }
    }
}