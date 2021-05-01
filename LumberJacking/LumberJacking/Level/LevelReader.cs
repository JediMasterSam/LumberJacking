using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LumberJacking.Level
{
    public static class LevelReader
    {
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
            
            for (var x = 0; x < cellTypes.GetLength(1); x++)
            {
                for (var y = 0; y < cellTypes.GetLength(0); y++)
                {
                    var cellType = cellTypes[y, x];

                    Debug.Write(cellType == CellType.Empty ? " " : (int) cellType);
                }

                Debug.Write("\n");
            }
        }
    }
}