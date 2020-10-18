using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class EnemySpawnManager : Singleton<EnemySpawnManager>
    {
        public List<GameObject> AllEnemyList = new List<GameObject>();
    }
}