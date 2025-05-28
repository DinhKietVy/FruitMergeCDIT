using System;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public static event Action boosTer1;

    public void Booster1Clicked()
    {
        boosTer1?.Invoke();
    }
}
