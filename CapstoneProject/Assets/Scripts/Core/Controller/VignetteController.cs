using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VignetteController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DisableVignette();
        //StartCoroutine(TestVignette());
    }

    IEnumerator TestVignette()
    {
        SetActiveBlack();
        yield return new WaitForSeconds(2);
        SetActiveRed();
        yield return new WaitForSeconds(2);
        DisableVignette();
        yield return new WaitForSeconds(2);
    }

    public void SetActiveBlack()
    {
        this.GetComponent<Image>().enabled = true;
        this.GetComponent<Image>().color = new Color32(0, 0, 0, 185);
    }
    public void SetActiveRed()
    {
        this.GetComponent<Image>().enabled = true;
        this.GetComponent<Image>().color = new Color32(255, 0, 0, 150);
    }

    public void DisableVignette()
    {
        this.GetComponent<Image>().enabled = false;
    }

}
