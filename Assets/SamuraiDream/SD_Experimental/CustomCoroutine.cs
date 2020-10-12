using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCoroutine : MonoBehaviour
{
    private int Num;
    Coroutine MyCoroutine;
    private bool NumReloaded;

    void Start()
    {
        Num = 0;
        MyCoroutine = null;
        NumReloaded = true; ;
    }

    private void Update()
    {
        if (MyCoroutine == null && NumReloaded)
        {
            MyCoroutine = StartCoroutine(_MyCoroutine());
        }
    }
    IEnumerator _MyCoroutine()
    {
            /*
             write your code
             */
            Debug.Log("Coroutine : " + (Num++)); // <-- could delete this row

            NumReloaded = false;

            yield return new WaitForSeconds(1f);

            MyCoroutine = null;
            NumReloaded = true; ;
    }
}
