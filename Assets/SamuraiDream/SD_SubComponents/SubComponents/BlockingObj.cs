using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class BlockingObj : SubComponent
    {
        public BlockingObjData blockingData;

        private Dictionary<GameObject, GameObject> FrontBlockingObjs = new Dictionary<GameObject, GameObject>();
        private Dictionary<GameObject, GameObject> UpBlockingObjs = new Dictionary<GameObject, GameObject>();
        private Dictionary<GameObject, GameObject> DownBlockingObjs = new Dictionary<GameObject, GameObject>();

        private List<CharacterController> MarioStumpTargets = new List<CharacterController>();

        private List<GameObject> FrontBlockingObjsList = new List<GameObject>();
        private List<GameObject> FrontBlockingCharacters = new List<GameObject>();

        private GameObject[] FrontSpheresArray;
        private float DirBlock;
        private void Start()
        {
            blockingData = new BlockingObjData
            {
                RaycastContact = new Vector3(),
                FrontBlockingDicCount = 0,
                UpBlockingDicCount = 0,
                ClearFrontBlockingObjDic = ClearFrontBlockingObjDic,
                RightSideBlocked = RightSideIsBlocked,
                LeftSideBlocked = LeftSideIsBlocked,
                FrontBlockingObjectsList = GetFrontBlockingObjsList,
                FrontBlockingCharacterList = GetFrontBlockingCharacters,
            };
            subComponentProcessor.blockingData = blockingData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.BLOCKINGOBJECTS] = this;
            //subComponentProcessor.ComponentsDic.Add(SubComponentType.BLOCKINGOBJECTS, this);
        }
        public override void OnFixedUpdate()
        {
            if (control.ANIMATION_DATA.IsRunning(typeof(MoveForward)))
            {
                CheckFrontBlocking();
            }
            else
            {
                if (FrontBlockingObjs.Count != 0)
                {
                    FrontBlockingObjs.Clear();
                }
            }
            //checking UpBlock while LedgeGrabbing
            if (control.ANIMATION_DATA.IsRunning(typeof(MoveUp)))
            {
                if (control.animationProgress.LatestMoveUp.Speed > 0f)
                {
                    CheckUpBlocking();
                }
            }
            else
            {
                //checking UpBlock while Jumping
                if (control.RIGID_BODY.velocity.y > 0.001f)
                {
                    CheckUpBlocking();

                    foreach (KeyValuePair<GameObject, GameObject> data in UpBlockingObjs)
                    {
                        CharacterController c = CharacterManager.Instance.GetCharacter(
                                                data.Value.transform.root.gameObject);
                        if (c == null)
                        {
                            control.animationProgress.NullifyUpVelocity();
                            break;
                        }
                        else
                        {
                            if ((control.transform.position.y + c.boxCollider.center.y) >
                                        c.transform.position.y)
                            {
                                control.animationProgress.NullifyUpVelocity();
                                break;
                            }
                        }
                    }
                }
                if (UpBlockingObjs.Count != 0)
                {
                    UpBlockingObjs.Clear();
                }
            }

            CheckMarioStomp();

            blockingData.FrontBlockingDicCount = FrontBlockingObjs.Count;
            blockingData.UpBlockingDicCount = UpBlockingObjs.Count;
        }

        public override void OnUpdate()
        {
            
        }
        private void CheckMarioStomp()
        {
            if (control.RIGID_BODY.velocity.y >= 0f)
            {
                MarioStumpTargets.Clear();
                DownBlockingObjs.Clear();
                return;
            }
            if (MarioStumpTargets.Count > 0)
            {
                control.RIGID_BODY.velocity = Vector3.zero;
                control.RIGID_BODY.AddForce(Vector3.up * 250f);

                foreach (CharacterController c in MarioStumpTargets)
                {
                    if (control.aiController != null && c.aiController != null)
                    {
                        break;
                    }

                    AttackCondition info = gameObject.AddComponent<AttackCondition>();
                    info.CopyInfo(c.DAMAGE_DATA.MarioStompAttack, control);

                    int index = Random.Range(0, c.RAGDOLL_DATA.BodyParts.Count);
                    TriggerDetector randomPart = c.RAGDOLL_DATA.BodyParts[index].GetComponent<TriggerDetector>();
                    
                    c.DAMAGE_DATA.SetData(
                        control,
                        c.DAMAGE_DATA.MarioStompAttack,
                        randomPart,
                        control.RightFoot_Attack);

                    c.DAMAGE_DATA.TakeDamage(info);
                }

                MarioStumpTargets.Clear();
                return;
            }

            CheckDownBlocking();

            if (DownBlockingObjs.Count > 0)
            {
                foreach (KeyValuePair<GameObject, GameObject> data in DownBlockingObjs)
                {
                    CharacterController c =
                            CharacterManager.Instance.GetCharacter(data.Value.transform.root.gameObject);
                    if (c != null)
                    {
                        if (c.boxCollider.center.y + c.transform.position.y < control.transform.position.y)
                        {
                            if (c != control)
                            {
                                if (!MarioStumpTargets.Contains(c))
                                {
                                    MarioStumpTargets.Add(c);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void CheckFrontBlocking()
        {
            if (!control.animationProgress.ForwardIsReversed())
            {
                FrontSpheresArray = control.COLLISION_SPHERE_DATA.FrontSpheres;
                DirBlock = 1f;
                foreach (GameObject s in control.COLLISION_SPHERE_DATA.BackSpheres)
                {
                    if (FrontBlockingObjs.ContainsKey(s))
                    {
                        FrontBlockingObjs.Remove(s);
                    }
                }
            }
            else
            {
                FrontSpheresArray = control.COLLISION_SPHERE_DATA.BackSpheres;
                DirBlock = -1f;
                foreach (GameObject s in control.COLLISION_SPHERE_DATA.FrontSpheres)
                {
                    if (FrontBlockingObjs.ContainsKey(s))
                    {
                        FrontBlockingObjs.Remove(s);
                    }
                }
            }
            for(int i = 0; i < FrontSpheresArray.Length; i++)
            {
                GameObject blockingObj = CollisionDetection.GetCollidingObject(control, FrontSpheresArray[i],
                                                            this.transform.forward *
                                                            DirBlock,
                                                            control.animationProgress.LatestMoveForward.BlockDistance,
                                                            ref control.BLOCKING_DATA.RaycastContact);

                if (blockingObj != null)
                {
                    AddBlockingObjToDic(FrontBlockingObjs, FrontSpheresArray[i], blockingObj);
                }
                else
                {
                    RemoveKeyFromDic(FrontBlockingObjs, FrontSpheresArray[i]);
                }
            }
        }
        private void CheckDownBlocking()
        {
            foreach (GameObject s in control.COLLISION_SPHERE_DATA.BottomSpheres)
            {
                GameObject blockingObj = CollisionDetection.GetCollidingObject(control, s, Vector3.down,
                                                                    0.1f, ref control.BLOCKING_DATA.RaycastContact);
                if (blockingObj != null)
                {
                    AddBlockingObjToDic(DownBlockingObjs, s, blockingObj);
                }
                else
                {
                    RemoveKeyFromDic(DownBlockingObjs, s);
                }
            }
        }
        private void CheckUpBlocking()
        {
            foreach (GameObject s in control.COLLISION_SPHERE_DATA.UpSpheres)
            {

                GameObject blockingObj = CollisionDetection.GetCollidingObject(control, s, Vector3.up, 0.3f,
                                                                               ref control.BLOCKING_DATA.RaycastContact);

                if (blockingObj != null)
                {
                    AddBlockingObjToDic(UpBlockingObjs, s, blockingObj);
                }
                else
                {
                    RemoveKeyFromDic(UpBlockingObjs, s);
                }
            }
        }
        private void AddBlockingObjToDic(Dictionary<GameObject, GameObject> dic, GameObject key, GameObject value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }
        private void RemoveKeyFromDic(Dictionary<GameObject, GameObject> dic, GameObject key)
        {
            if (dic.ContainsKey(key))
            {
                dic.Remove(key);
            }
        }
        private bool RightSideIsBlocked()
        {
            foreach (KeyValuePair<GameObject, GameObject> data in FrontBlockingObjs)
            {
                if ((data.Value.transform.position - control.transform.position).z > 0f)
                {
                    return true;
                }
            }
            return false;
        }
        private bool LeftSideIsBlocked()
        {
            foreach (KeyValuePair<GameObject, GameObject> data in FrontBlockingObjs)
            {
                if ((data.Value.transform.position - control.transform.position).z < 0f)
                {
                    return true;
                }
            }
            return false;
        }
        private void ClearFrontBlockingObjDic()
        {
            FrontBlockingObjs.Clear();
        }
        private List<GameObject> GetFrontBlockingCharacters()
        {
            FrontBlockingCharacters.Clear();

            foreach (KeyValuePair<GameObject, GameObject> data in FrontBlockingObjs)
            {
                CharacterController c = CharacterManager.Instance.GetCharacter(data.Value.transform.root.gameObject);
                if (c != null)
                {
                    if (!FrontBlockingCharacters.Contains(c.gameObject))
                    {
                        FrontBlockingCharacters.Add(c.gameObject);
                    }
                }
            }

            return FrontBlockingCharacters;
        }
        private List<GameObject> GetFrontBlockingObjsList()
        {
            FrontBlockingObjsList.Clear();

            foreach (KeyValuePair<GameObject, GameObject> data in FrontBlockingObjs)
            {
                if (!FrontBlockingObjsList.Contains(data.Value))
                {
                    FrontBlockingObjsList.Add(data.Value);
                }
            }

            return FrontBlockingObjsList;
        }
    }
}