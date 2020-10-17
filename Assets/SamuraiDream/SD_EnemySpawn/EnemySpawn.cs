using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SamuraiGame
{
    public enum EnemyType
    {
        yBot_Blue_Enemy,
    }
    public class EnemySpawn : MonoBehaviour
    {
        public float SpawnDelay;
        public List<CharacterController> AllEnemyList = new List<CharacterController>();
        public List<CharacterController> AliveEnemyList = new List<CharacterController>();
        public EnemyType[] enemyArr;
        private void Start()
        {
            AllEnemyList.Clear();
            AliveEnemyList.Clear();

            enemyArr = System.Enum.GetValues(typeof(EnemyType)) as EnemyType[];

            SpawnRandomYBot();

        }
        
        private void SpawnRandomYBot()
        {
            //int enemyRange = Random.Range(0, enemyArr.Length);
            //string enemyTypeStr = enemyArr[enemyRange].ToString();

            string enemyTypeStr = EnemyType.yBot_Blue_Enemy.ToString();

            GameObject enemyObj = Instantiate(Resources.Load(enemyTypeStr, typeof(GameObject)) as GameObject);

            enemyObj.transform.position = this.transform.position;

        }
    }
}