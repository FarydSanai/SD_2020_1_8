using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class EnemySpawnManager : Singleton<EnemySpawnManager>
    {
        public List<GameObject> AliveEnemyList = new List<GameObject>();
        public List<GameObject> DeadEnemyList = new List<GameObject>();
    }
}