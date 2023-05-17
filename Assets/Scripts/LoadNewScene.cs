using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public AudioSource ElevatorClose;
    public Animator ElevatorAnim;


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadBackroomsAsync());

            ElevatorAnim.SetBool("close", true);
            ElevatorClose.Play();
        }
    }

    IEnumerator LoadBackroomsAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Backrooms");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
