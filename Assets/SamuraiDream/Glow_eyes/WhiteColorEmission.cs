using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteColorEmission : MonoBehaviour
{
    private Material GlowMat;
    private void Start()
    {
        GlowMat = this.gameObject.GetComponent<SkinnedMeshRenderer>().material;
    }
    public void TurnOnWhiteEmission(Vector3 val)
    {
        GlowMat.SetVector("_whiteGlow", val);
    }

}
