using System.Collections;
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

        public GameObject[] enemies = new GameObject[3];

        private void Start()
        {
            enemyArr = System.Enum.GetValues(typeof(EnemyType)) as EnemyType[];

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = SpawnRandomYBot();
                if (i > 0)
                {
                    enemies[i].GetComponentInChildren<Animator>().gameObject.SetActive(false);
                    enemies[i].GetComponentInChildren<AIController>().gameObject.SetActive(false);
                }
            }
            if (SpawnRoutine != null)
            {
                StopCoroutine(SpawnRoutine);
            }
            else
            {
                SpawnRoutine = StartCoroutine(_SpawnEnemies());
            }
        }
        IEnumerator _SpawnEnemies()
        {
            for (int i = 1; i < MaxEnemiesCount; i++)
            {
                yield return new WaitForSeconds(SpawnDelay);

                enemies[i].transform.Find("ybot Skin").gameObject.SetActive(true);
                enemies[i].transform.Find("AI").gameObject.SetActive(true);
            }
            StopCoroutine(SpawnRoutine);
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