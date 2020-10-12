using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class CollisionSpheres : SubComponent
    {
        public CollisionData collisionData;
        private void Start()
        {
            collisionData = new CollisionData
            {
                FrontSpheres = new List<GameObject>(),
                BottomSpheres = new List<GameObject>(),
                BackSpheres = new List<GameObject>(),
                UpSpheres = new List<GameObject>(),

                FrontOverlapCheckers = new List<OverlapChecker>(),
                AllOverlapCheckers = new List<OverlapChecker>(),

                Reposition_TopSpheres = Reposition_TopSpheres,
                Reposition_FrontSpheres = Reposition_FrontSpheres,
                Reposition_BottomSpheres  = Reposition_BottomSpheres,
                Reposition_BackSpheres = Reposition_BackSpheres,
            };

            subComponentProcessor.collisionData = collisionData;
            subComponentProcessor.ComponentsDic.Add(SubComponentType.COLLISION_SPHERES, this);

            SetColliderSpheres();
        }
        public override void OnFixedUpdate()
        {
            foreach (OverlapChecker oc in collisionData.AllOverlapCheckers)
            {
                oc.UpdateChecker();
            }
        }
        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
        private void SetColliderSpheres()
        {
            //Bottom
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)),
                                             Vector3.zero, Quaternion.identity) as GameObject;
                collisionData.BottomSpheres.Add(obj);
                obj.transform.parent = this.transform.Find("Bottom");
            }

            Reposition_BottomSpheres();

            //Front
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)),
                                             Vector3.zero, Quaternion.identity) as GameObject;
                collisionData.FrontSpheres.Add(obj);
                collisionData.FrontOverlapCheckers.Add(obj.GetComponent<OverlapChecker>());
                obj.transform.parent = this.transform.Find("Front");
            }

            Reposition_FrontSpheres();

            //Back
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)),
                                             Vector3.zero, Quaternion.identity) as GameObject;
                collisionData.BackSpheres.Add(obj);
                obj.transform.parent = this.transform.Find("Back");
            }
            Reposition_BackSpheres();

            //Top
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)),
                                             Vector3.zero, Quaternion.identity) as GameObject;
                collisionData.UpSpheres.Add(obj);
                obj.transform.parent = this.transform.Find("Up");
            }

            Reposition_TopSpheres();

            //Add everything
            OverlapChecker[] arr = this.GetComponentsInChildren<OverlapChecker>();
            collisionData.AllOverlapCheckers.Clear();
            collisionData.AllOverlapCheckers.AddRange(arr);

        }
        private void Reposition_FrontSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - control.boxCollider.bounds.extents.y;
            float top = control.boxCollider.bounds.center.y + control.boxCollider.bounds.extents.y;
            float front = control.boxCollider.bounds.center.z + control.boxCollider.bounds.extents.z;
            //float back = boxCollider.bounds.center.z - boxCollider.bounds.extents.z;
            float debth = control.boxCollider.bounds.center.x;

            collisionData.FrontSpheres[0].transform.localPosition =
                new Vector3(debth, bottom + 0.05f, front) - control.transform.position;

            collisionData.FrontSpheres[1].transform.localPosition =
                new Vector3(debth, top, front) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < collisionData.FrontSpheres.Count; i++)
            {
                collisionData.FrontSpheres[i].transform.localPosition = 
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

            collisionData.BottomSpheres[0].transform.localPosition =
                new Vector3(debth, bottom, back) - control.transform.position;

            collisionData.BottomSpheres[1].transform.localPosition =
                new Vector3(debth, bottom, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < collisionData.BottomSpheres.Count; i++)
            {
                collisionData.BottomSpheres[i].transform.localPosition =
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

            collisionData.BackSpheres[0].transform.localPosition =
                new Vector3(debth, bottom + 0.05f, back) - control.transform.position;

            collisionData.BackSpheres[1].transform.localPosition =
                new Vector3(debth, top, back) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < collisionData.BackSpheres.Count; i++)
            {
                collisionData.BackSpheres[i].transform.localPosition =
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

            collisionData.UpSpheres[0].transform.localPosition =
                new Vector3(debth, top, back) - control.transform.position;

            collisionData.UpSpheres[1].transform.localPosition =
                new Vector3(debth, top, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < collisionData.UpSpheres.Count; i++)
            {
                collisionData.UpSpheres[i].transform.localPosition =
                    new Vector3(debth, top, back + (interval * (i - 1))) - control.transform.position;
            }
        }
    }
}
