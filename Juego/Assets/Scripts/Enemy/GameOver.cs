using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject playerUI;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            gameOver.SetActive(true);
            playerUI.SetActive(false);
            ModifyGameOver(true);
        }
    }

    void ModifyGameOver(bool newValue)
    {
        MovementTopDown[] players = FindObjectsOfType<MovementTopDown>();

        foreach (MovementTopDown player in players)
        {
            player.gameOver = newValue;
        }
    }
}
