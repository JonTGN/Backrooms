using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePreviousLevel : MonoBehaviour
{
    public GameObject levelToHide;
    public GameObject levelToShow;
    public Animator doorAnim;

    private bool alreadyActivated;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!alreadyActivated)
            {
                doorAnim.SetBool("close", true);
                Invoke(nameof(HideShowLevel), 1f);
            }
        }
    }

    private void HideShowLevel()
    {
        levelToHide.SetActive(false);
        levelToShow.SetActive(true);
    }
}
