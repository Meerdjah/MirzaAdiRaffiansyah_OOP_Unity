using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (animator == null)
        {
            Debug.LogWarning(this + "tidak memiliki Animator");
            yield break;
        }

        animator.SetTrigger("StartTransition");//Menjalankan animasi StartTransition

        yield return new WaitForSeconds(1);//Menunggu satu detik agar animasi dapat selesai dijalankan

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);//Mengganti scene

        while (!asyncLoad.isDone)//Menunggu scene dimuat
        {
            yield return null;
        }
        
        animator.SetTrigger("EndTransition");//Menjalankan animasi EndTransition
    }

    /*
    Method untuk menjalankan start coroutine
    coroutine adalah method pada unity yang dapat dihentikan
    dan dilanjutkan kembali prosesnya sehingga animasi dapat 
    dijalankan terlebih dahulu dan melanjutkan animasi berikutnya
    setelah berganti scene  
    */
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));//Menjalankan start coroutine
    }
}
