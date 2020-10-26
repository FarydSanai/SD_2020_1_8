using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class GameEvent : MonoBehaviour
    {
        public List<GameEventListener> ListenersList = new List<GameEventListener>();
        public GameObject EventObj { get; private set; }
        private void Awake()
        {
            ListenersList.Clear();
        }
        public void Raise()
        {
            foreach(GameEventListener listener in ListenersList)
            {
                listener.OnRaiseEvent();
            }
        }
        public void Raise(GameObject eventObj)
        {
            EventObj = eventObj;
            foreach (GameEventListener listener in ListenersList)
            {
                listener.OnRaiseEvent();
            }
        }
        public void AddListener(GameEventListener listener)
        {
            if (!ListenersList.Contains(listener))
            {
                ListenersList.Add(listener);
            }
        }
    }
}
