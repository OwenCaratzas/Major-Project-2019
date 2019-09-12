using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe_Script : MonoBehaviour
{

    private bool safeSearched = false;
    public bool objectiveCompleted = false;
    public GameObject ObjectiveText;

    public void Search()
    {
        if (safeSearched == false)
        {
            ObjectiveText.SetActive(true);
            safeSearched = true;
            objectiveCompleted = true;
            StartCoroutine(EndText());
        }
    }

    IEnumerator EndText()
    {
        yield return new WaitForSeconds(6);
        ObjectiveText.SetActive(false);
    }
}
