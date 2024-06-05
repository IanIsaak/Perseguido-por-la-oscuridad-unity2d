using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScr : MonoBehaviour
{
    static public MainScr Instance;
    public int switchCount;
    private int onCount = 0;
    public bool isSWActive = false;

    private void Awake()
    { 
        if(isSWActive == true )
        {
            Instance = this;
        }
        
    }

    private void Update()
    {
        Awake();
    }

    private void Start()
    {
        //switchCount = 5;
    }

    public void switchChange(int points)
    {
        onCount = onCount + points;
        if (onCount == switchCount)
        {
            Destroy(this.gameObject, 0.5f);
        }
    }


}
