using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task : MonoBehaviour
{
    public GameObject taskp;
    bool playerclose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTaskActive() && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(taskp);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerclose = true;
        }
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerclose = false;
        }

    }

    private bool isTaskActive()
    {
        return playerclose && !GameObject.FindWithTag("Task");
    }
}
