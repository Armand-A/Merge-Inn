using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class waitNgo : MonoBehaviour
{
    public string sc_name;
    public float time = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Next());
    }

    // Update is called once per frame
    IEnumerator Next(){
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene(sc_name);
    }
}
