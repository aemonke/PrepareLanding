using System.Collections.Generic;
using HarmonyLib;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace PrepareLanding.Patches
{
    [HarmonyPatch(typeof(WorldGenStep_Terrain), nameof(WorldGenStep_Terrain.GenerateFresh))]
    public static class PatchGenerateGridIntoWorld
    {
        public static readonly List<KeyValuePair<int, Vector3>> TileIdsAndVectors =
            new List<KeyValuePair<int, Vector3>>();

        private static WorldGrid _cachedGrid;

        [HarmonyPostfix]
        public static void GenerateGridIntoWorldPostFix()
        {
            RebuildTileCache();
        }

        /// <summary>
        ///     Make sure the tile cache matches the current world (it's empty or stale after loading a save).
        /// </summary>
        public static void EnsureTileCache()
        {
            var grid = Find.WorldGrid;
            if (grid == null)
                return;

            if (grid != _cachedGrid || TileIdsAndVectors.Count != grid.TilesCount)
                RebuildTileCache();
        }

        private static void RebuildTileCache()
        {
            var grid = Find.WorldGrid;
            if (grid == null)
                return;

            _cachedGrid = grid;
            var tilesCount = grid.TilesCount;

            TileIdsAndVectors.Clear();
            TileIdsAndVectors.Capacity = tilesCount;

            for (var i = 0; i < tilesCount; i++)
            {
                var tileCenter = grid.GetTileCenter(i);
                var kvp = new KeyValuePair<int, Vector3>(i, tileCenter);
                TileIdsAndVectors.Add(kvp);
            }

            // sort by y axis (latitude)
            TileIdsAndVectors.Sort((x, y) => x.Value.y.CompareTo(y.Value.y));
        }
    }
}
