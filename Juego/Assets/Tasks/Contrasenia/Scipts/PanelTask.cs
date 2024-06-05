using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Timeline;

public class PanelTask : MonoBehaviour
{
    public TextMeshProUGUI display;
    public TextMeshProUGUI papel;
    public bool panelCompleted=false;


    // Start is called before the first frame update
    void Start()
    {
        GeneratePassword();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNumber(string number)
    { 
        if(display.text.Length >=5)
        {
            return;
        }

        display.text += number;
    }

    public void EraseDisplay()
    {
        display.text = "";
    }

    private void GeneratePassword()
    {
        papel.text = "";
        for(int i = 0; i < 5; i++)
        {
            int randNumber = UnityEngine.Random.Range(1,9);
            papel.text += randNumber;
        }
    }

    public void CheckPassword()
    {
        if(display.text.Equals(papel.text))
        {
            display.text=("Granted");
            Destroy(gameObject, 1.0f);
            panelCompleted = true;
        }
        else
        {
            display.text = ("Denied");
        }
    }
}
