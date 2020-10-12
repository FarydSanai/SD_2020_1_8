using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace SamuraiGame
{
    public class RaiseEmission : MonoBehaviour
    {
        private Material SkinMat;

        private void Start()
        {
            SkinMat = this.GetComponent<SkinnedMeshRenderer>().material;
        }
        public void RaiseEmissionLight(Vector3 val)
        {
            SkinMat.SetVector("_colorIntense", val);
            //Debug.Log("Eval");
        }
    }
}
