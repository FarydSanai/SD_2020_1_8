using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

namespace SamuraiGame
{ 
    public class ManualInput : SubComponent
    {
        public ManualInputData manualInputData;

        [SerializeField] private List<InputKeyType> DoubleTaps = new List<InputKeyType>();
        [SerializeField] private List<InputKeyType> UpKeys = new List<InputKeyType>();
        private Dictionary<InputKeyType, float> DicDoubleTapTimings = new Dictionary<InputKeyType, float>();

        private void Start()
        {
            manualInputData = new ManualInputData
            {
                DoubleTapDown = IsDoubleTap_Down,
                DoubleTapUp = IsDoubleTap_Up,
            };


            subComponentProcessor.manualInputData = manualInputData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.MANUALINPUT] = this;
            //subComponentProcessor.ComponentsDic.Add(SubComponentType.MANUALINPUT, this);

        }
        public override void OnFixedUpdate()
        {

        }
        public override void OnUpdate()
        {
            if (VirtualInputManager.Instance.Turbo)
            {
                control.Turbo = true;
                ProcessDoubleTap(InputKeyType.KEY_TURBO);
            }
            else
            {
                control.Turbo = false;
                RemoveDoubleTap(InputKeyType.KEY_TURBO);
            }

            if (VirtualInputManager.Instance.MoveUp)
            {
                control.MoveUp = true;
                ProcessDoubleTap(InputKeyType.KEY_MOVE_UP);
            } else
            {
                control.MoveUp = false;
                RemoveDoubleTap(InputKeyType.KEY_MOVE_UP);
            }

            if (VirtualInputManager.Instance.MoveDown)
            {
                control.MoveDown = true;
                ProcessDoubleTap(InputKeyType.KEY_MOVE_DOWN);
            }
            else
            {
                control.MoveDown = false;
                RemoveDoubleTap(InputKeyType.KEY_MOVE_DOWN);
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                control.MoveRight = true;
                ProcessDoubleTap(InputKeyType.KEY_MOVE_RIGHT);
            }
            else
            {
                control.MoveRight = false;
                RemoveDoubleTap(InputKeyType.KEY_MOVE_RIGHT);
            }

            if (VirtualInputManager.Instance.MoveLeft)
            {
                control.MoveLeft = true;
                ProcessDoubleTap(InputKeyType.KEY_MOVE_LEFT);
            }
            else
            {
                control.MoveLeft = false;
                RemoveDoubleTap(InputKeyType.KEY_MOVE_LEFT);
            }

            if (VirtualInputManager.Instance.Jump)
            {
                control.Jump = true;
                ProcessDoubleTap(InputKeyType.KEY_JUMP);
            }
            else
            {
                control.Jump = false;
                RemoveDoubleTap(InputKeyType.KEY_JUMP);
            }

            if (VirtualInputManager.Instance.Block)
            {
                control.Block = true;
                ProcessDoubleTap(InputKeyType.KEY_BLOCK);
            }
            else
            {
                control.Block = false;
                RemoveDoubleTap(InputKeyType.KEY_BLOCK);
            }

            if (VirtualInputManager.Instance.Attack)
            {
                control.Attack = true;
                ProcessDoubleTap(InputKeyType.KEY_ATTACK);
            }
            else
            {
                control.Attack = false;
                RemoveDoubleTap(InputKeyType.KEY_ATTACK);
            }

            //double tap running
            if (DoubleTaps.Contains(InputKeyType.KEY_MOVE_LEFT) ||
                DoubleTaps.Contains(InputKeyType.KEY_MOVE_RIGHT))
            {
                control.Turbo = true;
            }

            //double tap running turn

            if (control.MoveRight && control.MoveLeft)
            {
                if (DoubleTaps.Contains(InputKeyType.KEY_MOVE_LEFT) ||
                    DoubleTaps.Contains(InputKeyType.KEY_MOVE_RIGHT))
                {
                    if (!DoubleTaps.Contains(InputKeyType.KEY_MOVE_LEFT))
                    {
                        DoubleTaps.Add(InputKeyType.KEY_MOVE_LEFT);
                    }
                    if (!DoubleTaps.Contains(InputKeyType.KEY_MOVE_RIGHT))
                    {
                        DoubleTaps.Add(InputKeyType.KEY_MOVE_RIGHT);
                    }
                }
            }
        }
        private bool IsDoubleTap_Up()
        {
            if (DoubleTaps.Contains(InputKeyType.KEY_MOVE_UP))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsDoubleTap_Down()
        {
            if (DoubleTaps.Contains(InputKeyType.KEY_MOVE_DOWN))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ProcessDoubleTap(InputKeyType keyType)
        {
            if (!DicDoubleTapTimings.ContainsKey(keyType))
            {
                DicDoubleTapTimings.Add(keyType, 0f);
            }
            if (DicDoubleTapTimings[keyType] == 0f ||
                UpKeys.Contains(keyType))
            {
                if (Time.time < DicDoubleTapTimings[keyType])
                {
                    if (!DoubleTaps.Contains(keyType))
                    {
                        DoubleTaps.Add(keyType);
                    }
                }

                if (UpKeys.Contains(keyType))
                {
                    UpKeys.Remove(keyType);
                }

                DicDoubleTapTimings[keyType] = Time.time + 0.2f;
            }
        }
        private void RemoveDoubleTap(InputKeyType keyType)
        {
            if (DoubleTaps.Contains(keyType))
            {
                DoubleTaps.Remove(keyType);
            }
            if (!UpKeys.Contains(keyType))
            {
                UpKeys.Add(keyType);
            }
        }
    }
}