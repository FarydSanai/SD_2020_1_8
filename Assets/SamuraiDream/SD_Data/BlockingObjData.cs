using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [System.Serializable]
    public class BlockingObjData
    {
        public Vector3 RaycastContact;

        public int FrontBlockingDicCount;
        public int UpBlockingDicCount;

        public delegate void DoSomething();
        public DoSomething ClearFrontBlockingObjDic;

        public delegate bool ReturnBool();
        public ReturnBool LeftSideBLocked;
        public ReturnBool RightSideBlocked;

        public delegate List<GameObject> ReturnGameObjList();
        public ReturnGameObjList FrontBlockingObjectsList;
        public ReturnGameObjList FrontBlockingCharacterList;

    }
}