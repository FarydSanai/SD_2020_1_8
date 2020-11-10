using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class CollisionSpheres : SubComponent
    {
        public CollisionSphereData collisionSphereData;
        private void Start()
        {
            collisionSphereData = new CollisionSphereData
            {
                FrontSpheres = new GameObject[10],
                BottomSpheres = new GameObject[5],
                BackSpheres = new GameObject[10],
                UpSpheres = new GameObject[5],

                FrontOverlapCheckers = new OverlapChecker[10],
                FrontOverlapCheckerContains = FrontOverlapCheckersContains,
                //AllOverlapCheckers = new List<OverlapChecker>(),

                Reposition_TopSpheres = Reposition_TopSpheres,
                Reposition_FrontSpheres = Reposition_FrontSpheres,
                Reposition_BottomSpheres  = Reposition_BottomSpheres,
                Reposition_BackSpheres = Reposition_BackSpheres,
            };

            subComponentProcessor.collisionSphereData = collisionSphereData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.COLLISION_SPHERES] = this;
            //subComponentProcessor.ComponentsDic.Add(SubComponentType.COLLISION_SPHERES, this);
            SetColliderSpheres();
        }
        public override void OnFixedUpdate()
        {
            for (int i = 0; i < collisionSphereData.AllOverlapCheckers.Length; i++)
            {
                collisionSphereData.AllOverlapCheckers[i].UpdateChecker();
            }
        }
        public override void OnUpdate()
        {

        }
        private GameObject LoadCollisionSphere()
        {
            return Instantiate(Resources.Load("CollisionSphere", typeof(GameObject)),
                                             Vector3.zero, Quaternion.identity) as GameObject;
        }
        private void SetColliderSpheres()
        {
            //Bottom
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = LoadCollisionSphere();

                collisionSphereData.BottomSpheres[i] = obj;
                obj.transform.parent = this.transform.Find("Bottom");
            }

            Reposition_BottomSpheres();

            //Front
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = LoadCollisionSphere();
                collisionSphereData.FrontSpheres[i] = obj;
                collisionSphereData.FrontOverlapCheckers[i] = obj.GetComponent<OverlapChecker>();

                obj.transform.parent = this.transform.Find("Front");
            }

            Reposition_FrontSpheres();

            //Back
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = LoadCollisionSphere();
                collisionSphereData.BackSpheres[i] = obj;
                obj.transform.parent = this.transform.Find("Back");
            }
            Reposition_BackSpheres();

            //Top
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = LoadCollisionSphere();
                collisionSphereData.UpSpheres[i] = obj;
                obj.transform.parent = this.transform.Find("Up");
            }

            Reposition_TopSpheres();

            //Add everything
            OverlapChecker[] arr = this.GetComponentsInChildren<OverlapChecker>();
            collisionSphereData.AllOverlapCheckers = arr;

        }
        private void Reposition_FrontSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - control.boxCollider.bounds.extents.y;
            float top = control.boxCollider.bounds.center.y + control.boxCollider.bounds.extents.y;
            float front = control.boxCollider.bounds.center.z + control.boxCollider.bounds.extents.z;
            //float back = boxCollider.bounds.center.z - boxCollider.bounds.extents.z;
            float debth = control.boxCollider.bounds.center.x;

            collisionSphereData.FrontSpheres[0].transform.localPosition =
                new Vector3(debth, bottom + 0.05f, front) - control.transform.position;

            collisionSphereData.FrontSpheres[1].transform.localPosition =
                new Vector3(debth, top, front) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < collisionSphereData.FrontSpheres.Length; i++)
            {
                collisionSphereData.FrontSpheres[i].transform.localPosition = 
                new Vector3(debth, bottom + (interval * (i - 1)), front) - control.transform.position;
            }
        }
        private void Reposition_BottomSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - control.boxCollider.bounds.extents.y;
            //float top = boxCollider.bounds.center.y + boxCollider.bounds.extents.y;
            float front = control.boxCollider.bounds.center.z + control.boxCollider.bounds.extents.z;
            float back = control.boxCollider.bounds.center.z - control.boxCollider.bounds.extents.z;
            float debth = control.boxCollider.bounds.center.x;

            collisionSphereData.BottomSpheres[0].transform.localPosition =
                new Vector3(debth, bottom, back) - control.transform.position;

            collisionSphereData.BottomSpheres[1].transform.localPosition =
                new Vector3(debth, bottom, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < collisionSphereData.BottomSpheres.Length; i++)
            {
                collisionSphereData.BottomSpheres[i].transform.localPosition =
                    new Vector3(debth, bottom, back + (interval * (i - 1))) - control.transform.position;
            }
        }
        private void Reposition_BackSpheres()
        {
            //float front = owner.boxCollider.bounds.center.z + owner.boxCollider.bounds.extents.z;
            float bottom = control.boxCollider.bounds.center.y - control.boxCollider.bounds.extents.y;
            float top = control.boxCollider.bounds.center.y + control.boxCollider.bounds.extents.y;
            float back = control.boxCollider.bounds.center.z - control.boxCollider.bounds.extents.z;
            float debth = control.boxCollider.bounds.center.x;

            collisionSphereData.BackSpheres[0].transform.localPosition =
                new Vector3(debth, bottom + 0.05f, back) - control.transform.position;

            collisionSphereData.BackSpheres[1].transform.localPosition =
                new Vector3(debth, top, back) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < collisionSphereData.BackSpheres.Length; i++)
            {
                collisionSphereData.BackSpheres[i].transform.localPosition =
                    new Vector3(debth, bottom + (interval * (i - 1)), back) - control.transform.position;
            }
        }
        private void Reposition_TopSpheres()
        {
            //float bottom = owner.boxCollider.bounds.center.y - owner.boxCollider.bounds.extents.y;
            float top = control.boxCollider.bounds.center.y + control.boxCollider.bounds.extents.y;
            float front = control.boxCollider.bounds.center.z + control.boxCollider.bounds.extents.z;
            float back = control.boxCollider.bounds.center.z - control.boxCollider.bounds.extents.z;
            float debth = control.boxCollider.bounds.center.x;

            collisionSphereData.UpSpheres[0].transform.localPosition =
                new Vector3(debth, top, back) - control.transform.position;

            collisionSphereData.UpSpheres[1].transform.localPosition =
                new Vector3(debth, top, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < collisionSphereData.UpSpheres.Length; i++)
            {
                collisionSphereData.UpSpheres[i].transform.localPosition =
                    new Vector3(debth, top, back + (interval * (i - 1))) - control.transform.position;
            }
        }
        private bool FrontOverlapCheckersContains(OverlapChecker checker)
        {
            for (int i = 0; i < collisionSphereData.FrontOverlapCheckers.Length; i++)
            {
                if (collisionSphereData.FrontOverlapCheckers[i] == checker)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
