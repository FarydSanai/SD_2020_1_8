using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCustomCoroutine : MonoBehaviour
{
    private int Num;
    private Coroutine MyCoroutine;
    private bool SecondRoutineReloaded;

    void Start()
    {
        Num = 0;
        MyCoroutine = null;
        SecondRoutineReloaded = true;
    }

    void Update()
    {
        if (MyCoroutine == null && SecondRoutineReloaded)
        {
            MyCoroutine = StartCoroutine(_SecondRoutine());
        }
    }
    private IEnumerator _SecondRoutine()
    {
        Debug.Log("second routine : " + (Num));
        SecondRoutineReloaded = false;
        yield return new WaitForSeconds(1f);
        SecondRoutineReloaded = true;
        MyCoroutine = null;
    }
}
