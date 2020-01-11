using System;
using System.Collections.Generic;
using System.Linq;
using TheMatiaz0_MonobeamTheMercenary.Movement;
using System.Diagnostics;

namespace TheMatiaz0_MonobeamTheMercenary.NPC
{
    /// <summary>
    /// Main class for spawning entities.
    /// </summary>
    public class SpawnRegionEntities
    {
        /// <summary>
        /// All entities in current Region (enum).
        /// </summary>
        public static List<IPositionEntity> CurrentRegionEntities = new List<IPositionEntity>();

        public static bool Exists<T>() where T : IPositionEntity => CurrentRegionEntities.Any(enemy => enemy is T);
        public static bool Exists(Type type) => (CurrentRegionEntities.Exists(entity => entity.GetType() == type));

        public static void AddEntity<T>(Point position) where T : IPositionEntity, new() => AddEntity(new T(), position);

        public static void RemoveEntity<T>() where T : IPositionEntity, new() => RemoveEntity(new T());

        public static void AddEntity(IPositionEntity positionEntity, Point position)
        {
            positionEntity.Position = position;
            Debug.WriteLine($"X: {positionEntity.Position.X}, Y: {positionEntity.Position.Y}");
            CurrentRegionEntities.Add(positionEntity);
        }

        public static void RemoveEntity(IPositionEntity positionEntity)
        {
            CurrentRegionEntities.Remove(positionEntity);
        }

        public static void Clear()
        {
            CurrentRegionEntities = new List<IPositionEntity>();
        }
    }
}
