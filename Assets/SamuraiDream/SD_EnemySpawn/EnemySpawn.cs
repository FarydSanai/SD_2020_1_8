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
        yBot_Green_Enemy,
        yBot_Red_Enemy,
    }
    public class EnemySpawn : MonoBehaviour
    {
        public float SpawnDelay;

        public int MaxAliveEnemiesCount;
        public int MaxEnemiesCount;

        public EnemyType[] enemyArr;

        public Coroutine SpawnRoutine;
        private void Start()
        {
            enemyArr = System.Enum.GetValues(typeof(EnemyType)) as EnemyType[];

            if (SpawnRoutine != null)
            {
                StopCoroutine(SpawnRoutine);
            }
            else
            {
                SpawnRoutine = StartCoroutine(_SpawnEnemies());
            }

        }
        private void Update()
        {
            if (EnemySpawnManager.Instance.AliveEnemyList.Count >= MaxAliveEnemiesCount ||
                EnemySpawnManager.Instance.DeadEnemyList.Count >= MaxEnemiesCount)
            {
                if (SpawnRoutine != null)
                {
                    StopCoroutine(SpawnRoutine);
                }
            }
            if (SpawnRoutine == null)
            {
                if (EnemySpawnManager.Instance.AliveEnemyList.Count < MaxAliveEnemiesCount &&
                    EnemySpawnManager.Instance.DeadEnemyList.Count < MaxEnemiesCount)
                {
                    SpawnRoutine = StartCoroutine(_SpawnEnemies());
                }
            }
        }
        IEnumerator _SpawnEnemies()
        {
            while(true)
            {
                GameObject newEnemy = SpawnRandomYBot();

                //newEnemy.GetComponent<CharacterController>().COLLISION_SPHERE_DATA.Reposition_FrontSpheres();

                yield return new WaitForSeconds(SpawnDelay);
            }
        }
        private GameObject SpawnRandomYBot()
        {
            int rand = Random.Range(0, enemyArr.Length);
            string enemyTypeStr = enemyArr[rand].ToString();

            GameObject enemyObj = Instantiate(Resources.Load(enemyTypeStr, typeof(GameObject)) as GameObject);
            
            EnemySpawnManager.Instance.AliveEnemyList.Add(enemyObj);
            enemyObj.transform.position = this.transform.position;

            return enemyObj;

        }
    }
}