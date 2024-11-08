using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneName);
        Player.Instance.transform.position = new(0, 0);
        animator.SetTrigger("Start");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
