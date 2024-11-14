using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Awake()
    {
        animator.enabled = false;
    }
    IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.enabled = true;
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Player.Instance.transform.position = new(0, 0);
        animator.SetTrigger("Start");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
