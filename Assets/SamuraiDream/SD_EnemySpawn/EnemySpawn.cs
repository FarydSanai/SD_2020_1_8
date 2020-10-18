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
        public int MaxEnemiesCount;
        public bool SpawnNext => EnemySpawnManager.Instance.AllEnemyList.Count < MaxEnemiesCount;

        public EnemyType[] enemyArr;

        public Coroutine SpawnRoutine;
        private void Start()
        {
            enemyArr = System.Enum.GetValues(typeof(EnemyType)) as EnemyType[];
        }
        private void OnEnable()
        {
            if (SpawnRoutine != null)
            {
                StopCoroutine(SpawnRoutine);
            }
            if (SpawnDelay > 0f)
            {
                SpawnRoutine = StartCoroutine(_SpawnEnemies());
            }
        }
        IEnumerator _SpawnEnemies()
        {
            while(SpawnNext)
            {
                SpawnRandomYBot();

                yield return new WaitForSeconds(SpawnDelay);
            }
        }
        private void SpawnRandomYBot()
        {
            //int enemyRange = Random.Range(0, enemyArr.Length);
            //string enemyTypeStr = enemyArr[enemyRange].ToString();

            string enemyTypeStr = EnemyType.yBot_Blue_Enemy.ToString();

            GameObject enemyObj = Instantiate(Resources.Load(enemyTypeStr, typeof(GameObject)) as GameObject);

            EnemySpawnManager.Instance.AllEnemyList.Add(enemyObj);

            enemyObj.transform.position = this.transform.position;

        }
    }
}