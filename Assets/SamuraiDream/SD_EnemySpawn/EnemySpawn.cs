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

        public EnemyType[] ArrEnemyType;
        private Coroutine SpawnRoutine;

        [SerializeField] private GameObject[] enemies;

        private void Start()
        {
            ArrEnemyType = System.Enum.GetValues(typeof(EnemyType)) as EnemyType[];
            enemies = new GameObject[MaxEnemiesCount];

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = SpawnRandomYBot();
                if (i > 0)
                {
                    SetEnemyActive(enemies[i], false);
                }
            }
            EnemySpawnManager.Instance.AliveEnemyList.Add(enemies[0]);
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

                if (EnemySpawnManager.Instance.AliveEnemyList.Count < MaxAliveEnemiesCount)
                {
                    SetEnemyActive(enemies[i], true);
                }
                else
                {
                    yield return new WaitUntil(() => EnemySpawnManager.Instance.AliveEnemyList.Count
                                                     < MaxAliveEnemiesCount);
                    SetEnemyActive(enemies[i], true);
                }
            }
            StopCoroutine(SpawnRoutine);
        }
        private GameObject SpawnRandomYBot()
        {
            int rand = Random.Range(0, ArrEnemyType.Length);
            string enemyTypeStr = ArrEnemyType[rand].ToString();

            GameObject enemyObj = Instantiate(Resources.Load(enemyTypeStr, typeof(GameObject)) as GameObject);
            enemyObj.transform.position = this.transform.position;

            return enemyObj;
        }
        private void SetEnemyActive(GameObject enemy, bool active)
        {
            SkinnedMeshRenderer[] arr = enemy.GetComponentsInChildren<SkinnedMeshRenderer>();
            arr[0].enabled = active;
            arr[1].enabled = active;
            enemy.transform.Find("AI").gameObject.SetActive(active);

            if (active)
            {
                EnemySpawnManager.Instance.AliveEnemyList.Add(enemy);
            }
        }
    }
}