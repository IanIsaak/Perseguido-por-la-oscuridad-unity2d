using UnityEngine;

public class Task2 : MonoBehaviour
{
    public GameObject task;
    private bool playerClose;
    private bool taskCompleted = false;

    void Start()
    {
       
    }

    void Update()
    {
        if (isTaskActive() && !taskCompleted)
        {
            Instantiate(task);
            taskCompleted = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerClose = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Correcci�n del error tipogr�fico
        {
            playerClose = false;
        }
    }

    private bool isTaskActive()
    {
        return playerClose && !GameObject.FindWithTag("Task");
    }
}
