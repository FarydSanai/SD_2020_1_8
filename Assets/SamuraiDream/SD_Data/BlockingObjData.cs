using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [System.Serializable]
    public class BlockingObjData
    {
        public int FrontBlockingDicCount;
        public int UpBlockingDicCount;

        public delegate void DoSomething();
        public delegate bool ReturnBool();
        public delegate List<GameObject> ReturnGameObjList();

        public DoSomething ClearFrontBlockingObjDic;

        public ReturnBool LeftSideBLocked;
        public ReturnBool RightSideBlocked;

        public ReturnGameObjList FrontBlockingObjectsList;
        public ReturnGameObjList FrontBlockingCharacterList;
    }
}