using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SamuraiGame
{
    public class TrapSpikes : MonoBehaviour
    {
        public List<CharacterController> ListCharacters = new List<CharacterController>();
        public List<CharacterController> ListSpikeVictims = new List<CharacterController>();
        public List<Spike> ListSpikes = new List<Spike>();
        public RuntimeAnimatorController SpikeDeathAnimator;

        private Coroutine SpikeTriggerRoutine;
        private bool SpikesReloaded;

        private void Start()
        {
            SpikeTriggerRoutine = null;
            SpikesReloaded = true;
            ListCharacters.Clear();
            ListSpikes.Clear();
            ListSpikeVictims.Clear();

            Spike[] arr = this.gameObject.GetComponentsInChildren<Spike>();

            foreach (Spike s in arr)
            {
                ListSpikes.Add(s);
            }
        }
        private void Update()
        {
            if (ListCharacters.Count != 0)
            {
                foreach (CharacterController control in ListCharacters)
                {
                    if (!control.DAMAGE_DATA.IsDead())
                    {
                        if (SpikeTriggerRoutine == null && SpikesReloaded)
                        {
                            if (!ListSpikeVictims.Contains(control))
                            {
                                ListSpikeVictims.Add(control);
                                control.DAMAGE_DATA.DamagedTrigger = null;
                                control.DAMAGE_DATA.HP = 0f;
                            }
                        }
                    }
                }
            }
            foreach (CharacterController control in ListSpikeVictims)
            {
                if (control.SkinnedMeshAnimator.avatar != null)
                {
                    if (SpikeTriggerRoutine == null && SpikesReloaded)
                    {
                        SpikeTriggerRoutine = StartCoroutine(_TriggerSpikes());
                    }
                }
            }
        }

        private IEnumerator _TriggerSpikes()
        {
            SpikesReloaded = false;

            foreach (Spike s in ListSpikes)
            {
                s.Shoot();
            }

            yield return new WaitForSeconds(0.09f);

            foreach (CharacterController control in ListSpikeVictims)
            {
                control.SkinnedMeshAnimator.runtimeAnimatorController = SpikeDeathAnimator;

                foreach (Collider c in control.RAGDOLL_DATA.BodyParts)
                {
                    c.attachedRigidbody.velocity = Vector3.zero;
                }
            }

            yield return new WaitForSeconds(1.5f);

            foreach (Spike s in ListSpikes)
            {
                s.Retract();
            }

            foreach (CharacterController control in ListSpikeVictims)
            {
                control.RAGDOLL_DATA.RagdollTriggered = true;
            }

            yield return new WaitForSeconds(1f);

            SpikeTriggerRoutine = null;
            SpikesReloaded = true;

        }

        public static bool IsTrap(GameObject obj)
        {
            if (obj.transform.root.gameObject.GetComponent<TrapSpikes>() == null)
            {
                return false;
            }
            else
            {
                return true; 
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            CharacterController control = col.transform.root.gameObject.GetComponent<CharacterController>();

            if (control != null)
            {
                if (control.gameObject != col.gameObject)
                {
                    if (!ListCharacters.Contains(control))
                    {
                        ListCharacters.Add(control);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider col)
        {
            CharacterController control = col.transform.root.gameObject.GetComponent<CharacterController>();

            if (control != null)
            {
                if (control.gameObject != col.gameObject)
                {
                    if (ListCharacters.Contains(control))
                    {
                        ListCharacters.Remove(control);
                    }
                }
            }
        }
    }
}
