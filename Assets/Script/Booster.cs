using System;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public static event Action boosTer1;

    public static event Action booster2;

    public static event Action booster3;

    public void Booster1Clicked() => boosTer1?.Invoke();

    public void Booster2Clicked() => booster2?.Invoke();

    public void Booster3Clicked() => booster3?.Invoke();
}
