using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace SamuraiGame 
{
    public enum InputKeyType
    {
        KEY_MOVE_UP,
        KEY_MOVE_DOWN,
        KEY_MOVE_RIGHT,
        KEY_MOVE_LEFT,

        KEY_JUMP,
        KEY_ATTACK,
        KEY_TURBO,

        KEY_BLOCK,
    }
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public PlayerInput playerInput;
        [Space(5)]
        public bool Turbo;
        public bool MoveUp;
        public bool MoveDown;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool Attack;
        public bool Block;

        [Header("Custom key bindings")]
        public bool UseCustomKeys;
        [Space(5)]
        public bool Bind_Turbo;
        public bool Bind_MoveUp;
        public bool Bind_MoveDown;
        public bool Bind_MoveRight;
        public bool Bind_MoveLeft;
        public bool Bind_Jump;
        public bool Bind_Attack;
        public bool Bind_Block;
        [Space(10)]
        public Dictionary<InputKeyType, KeyCode> DicKeys = new Dictionary<InputKeyType, KeyCode>();
        public KeyCode[] PossibleKeys;

        private void Awake()
        {
            PossibleKeys = System.Enum.GetValues(typeof(KeyCode)) as KeyCode[];

            GameObject obj = Instantiate(Resources.Load("PlayerInput", typeof(GameObject))) as GameObject;

            playerInput = obj.GetComponent<PlayerInput>();
        }
        public void LoadKeys()
        {
            if (playerInput.savedKeys.keyCodesList.Count > 0)
            {
                foreach (KeyCode k in playerInput.savedKeys.keyCodesList)
                {
                    if (k == KeyCode.None)
                    {
                        SetDefaultKeys();
                        break;
                    }
                }
            } else
            {
                SetDefaultKeys();
            }
            for (int i = 0; i < playerInput.savedKeys.keyCodesList.Count; i++)
            {
                DicKeys[(InputKeyType)i] = playerInput.savedKeys.keyCodesList[i];
            }
        }
        public void SaveKeys()
        {
            playerInput.savedKeys.keyCodesList.Clear();
            
            int keyCount = System.Enum.GetValues(typeof(InputKeyType)).Length;
            for (int i = 0; i < keyCount; i++)
            {
                playerInput.savedKeys.keyCodesList.Add(DicKeys[(InputKeyType)i]);
            }
        }
        public void SetDefaultKeys()
        {
            DicKeys.Clear();

            DicKeys.Add(InputKeyType.KEY_MOVE_UP,      KeyCode.W);
            DicKeys.Add(InputKeyType.KEY_MOVE_DOWN,    KeyCode.S);
            DicKeys.Add(InputKeyType.KEY_MOVE_LEFT,    KeyCode.A);
            DicKeys.Add(InputKeyType.KEY_MOVE_RIGHT,   KeyCode.D);

            DicKeys.Add(InputKeyType.KEY_JUMP,         KeyCode.Space);
            DicKeys.Add(InputKeyType.KEY_ATTACK,       KeyCode.Return);
            DicKeys.Add(InputKeyType.KEY_TURBO,        KeyCode.LeftShift);
            DicKeys.Add(InputKeyType.KEY_BLOCK,        KeyCode.LeftControl);

            SaveKeys();
        }
        private void Update()
        {
            if (!UseCustomKeys)
            {
                return;
            }
            if (UseCustomKeys)
            {
                if (Bind_MoveUp)
                {
                    if (KeyIsChanged(InputKeyType.KEY_MOVE_UP))
                    {
                        Bind_MoveUp = false;
                    }
                }
                if (Bind_MoveDown)
                {
                    if (KeyIsChanged(InputKeyType.KEY_MOVE_DOWN))
                    {
                        Bind_MoveDown = false;
                    }
                }
                if (Bind_MoveLeft)
                {
                    if (KeyIsChanged(InputKeyType.KEY_MOVE_LEFT))
                    {
                        Bind_MoveLeft = false;
                    }
                }
                if (Bind_MoveRight)
                {
                    if (KeyIsChanged(InputKeyType.KEY_MOVE_RIGHT))
                    {
                        Bind_MoveRight = false;
                    }
                }
                if (Bind_Jump)
                {
                    if (KeyIsChanged(InputKeyType.KEY_JUMP))
                    {
                        Bind_Jump = false;
                    }
                }
                if (Bind_Block)
                {
                    if (KeyIsChanged(InputKeyType.KEY_BLOCK))
                    {
                        Bind_Block = false;
                    }
                }
                if (Bind_Attack)
                {
                    if (KeyIsChanged(InputKeyType.KEY_ATTACK))
                    {
                        Bind_Attack = false;
                    }
                }
                if (Bind_Turbo)
                {
                    if (KeyIsChanged(InputKeyType.KEY_TURBO))
                    {
                        Bind_Turbo = false;
                    }
                }
            }
        }
        void SetCustomKey(InputKeyType inputKey, KeyCode key)
        {
            if (!DicKeys.ContainsKey(inputKey))
            {
                DicKeys.Add(inputKey, key);
            } else
            {
                DicKeys[inputKey] = key;
            }
            Debug.Log("Input key changed: " + inputKey.ToString() + " -> " + key.ToString());

            SaveKeys();
        }
        bool KeyIsChanged(InputKeyType inputKey)
        {
            if (Input.anyKey)
            {
                foreach (KeyCode k in PossibleKeys)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        continue;
                    }
                    if (Input.GetKeyDown(k))
                    {
                        SetCustomKey(inputKey, k);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
