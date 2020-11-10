using UnityEngine;

namespace SamuraiGame
{
    public class OverlapChecker : MonoBehaviour
    {
        private CharacterController control;
        public Collider[] arrColliders;
        public bool ObjIsOverlapping;

        private void Start()
        {
            control = this.transform.root.gameObject.GetComponent<CharacterController>();
        }
        public void UpdateChecker()
        {
            if (control == null)
            {
                return;
            }
            if (control.JUMP_DATA.CheckWallBlock)
            {
                if (control.COLLISION_SPHERE_DATA.FrontOverlapCheckerContains(this))
                {
                    ObjIsOverlapping = CheckObj();
                }
            }
            else
            {
                ObjIsOverlapping = false;
            }
        }
        private bool CheckObj()
        {
            arrColliders = Physics.OverlapSphere(this.transform.position, 0.13f);

            foreach (Collider c in arrColliders)
            {
                if (CharacterManager.Instance.GetCharacter(c.transform.root.gameObject) == null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}