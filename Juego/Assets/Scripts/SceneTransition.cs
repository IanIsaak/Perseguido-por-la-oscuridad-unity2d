using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AnimationClip finalAnimation;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerSceneChange()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(finalAnimation.length);
        SceneManager.LoadScene("MainMenu");
    }
}