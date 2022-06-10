using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utilitiesFunction : MonoBehaviour
{
    public static IEnumerator delayForSecond(float sec){
        yield return new WaitForSeconds(sec);
    }
}