using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.AI;

namespace SamuraiGame
{
    public class PathFindingAgent : MonoBehaviour
    {
        public bool TargetPlayableCharacter;
        public GameObject Target;
        NavMeshAgent navMeshAgent;
        Coroutine MoveRoutine;
        public GameObject StartSphere;
        public GameObject EndSphere;
        public bool StartWalk;
        public List<Vector3> MeshLinks = new List<Vector3>();

        public CharacterController owner = null;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void GoToTarget()
        {
            MeshLinks.Clear();

            navMeshAgent.enabled = true;
            StartSphere.transform.parent = null;
            EndSphere.transform.parent = null;
            StartWalk = false;

            navMeshAgent.isStopped = false;

            if (TargetPlayableCharacter)
            {
                Target = CharacterManager.Instance.GetPlayableCharacter().gameObject;
            }

            navMeshAgent.SetDestination(Target.transform.position);
            MoveRoutine = StartCoroutine(_Move()); 
        }

        private void OnEnable()
        {
            if (MoveRoutine != null)
            {
                StopCoroutine(MoveRoutine);
            }
        }

        IEnumerator _Move()
        {
            while (true)
            {
                if (navMeshAgent.isOnOffMeshLink)
                {
                    MeshLinks.Add(navMeshAgent.currentOffMeshLinkData.startPos);
                    MeshLinks.Add(navMeshAgent.currentOffMeshLinkData.endPos);

                    //StartSphere.transform.position = navMeshAgent.currentOffMeshLinkData.startPos;
                    //EndSphere.transform.position = navMeshAgent.currentOffMeshLinkData.endPos;

                    //navMeshAgent.CompleteOffMeshLink();

                    //navMeshAgent.isStopped = true;
                    //StartWalk = true;

                    //break;
                }

                Vector3 dist = transform.position - navMeshAgent.destination;
                if (Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    if (MeshLinks.Count > 0)
                    {
                        StartSphere.transform.position = MeshLinks[0];
                        EndSphere.transform.position = MeshLinks[1];
                    }
                    else
                    {
                        StartSphere.transform.position = navMeshAgent.destination;
                        EndSphere.transform.position = navMeshAgent.destination;
                    }

                    navMeshAgent.isStopped = true;
                    StartWalk = true;

                    break;
                }

                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.5f);
            owner.navMeshObstacle.carving = true;
        }
    }
}
