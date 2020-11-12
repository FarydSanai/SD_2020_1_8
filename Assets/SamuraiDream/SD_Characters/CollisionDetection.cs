using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class CollisionDetection : MonoBehaviour
    {
        public static GameObject GetCollidingObject(CharacterController control, GameObject start,
                                                    Vector3 dir, float blockDistance, ref Vector3 collisionPoint)
        {
            collisionPoint = Vector3.zero;
            //Debug draw line
            Debug.DrawRay(start.transform.position, dir * blockDistance, Color.yellow);
            RaycastHit hit;

            if (Physics.Raycast(start.transform.position, dir, out hit, blockDistance))
            {

                if (!IsBodyPart(control, hit.collider) &&
                    !IsIgnoringCharacter(control, hit.collider) &&
                    !Ledge.IsLedgeChecker(hit.collider.gameObject) &&
                    !MeleeWeapon.IsWeapon(hit.collider.gameObject) &&
                    !TrapSpikes.IsTrap(hit.collider.gameObject) &&
                    !ChangeScene.IsNextScenePoint(hit.collider.gameObject))
                {
                    collisionPoint = hit.point;
                    return hit.collider.transform.root.gameObject;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        static bool IsIgnoringCharacter(CharacterController control, Collider col)
        {
            if (!control.animationProgress.IsIgnoreCharacterTime)
            {
                return false;
            }
            else
            {
                CharacterController blockChar = CharacterManager.Instance.GetCharacter(col.transform.root.gameObject);
                if (blockChar == null)
                {
                    return false;
                }
                if (blockChar == control)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        static bool IsBodyPart(CharacterController control, Collider col)
        {
            if (col.transform.root.gameObject == control.gameObject)
            {
                return true;
            }

            CharacterController target = CharacterManager.Instance.GetCharacter(col.transform.root.gameObject);

            if (target == null)
            {
                return false;
            }

            if (target.DAMAGE_DATA.IsDead())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}