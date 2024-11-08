using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    IEnumerator LoadSceneAsync(string sceneName)
    {
        Debug.Log("Setting trigger: End");
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        
        Debug.Log("Scene loaded, resetting player position");
        Player.Instance.transform.position = new Vector3(0, 0, 0);

        Debug.Log("Setting trigger: Start");
        animator.SetTrigger("Start");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
