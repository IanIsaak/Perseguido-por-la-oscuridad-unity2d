using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScr : MonoBehaviour
{
    static public MainScr Instance;
    public int switchCount;
    private int onCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void switchChange(int points)
    {
        onCount = onCount + points;
        if (onCount == switchCount)
        {
            Destroy(this.gameObject, 1f);
        }
    }


}
