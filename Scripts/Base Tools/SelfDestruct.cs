using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float time;
    public bool erase;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Destruct(time));
    }

    void OnEnable()
    {
        StartCoroutine(Destruct(time));
    }


    public void RemoteDestruct(){
        StartCoroutine(Destruct(time));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Destruct(float time){
        yield return new WaitForSecondsRealtime(time);
        if (erase){
            Destroy(this.gameObject);
        } else {
            this.gameObject.SetActive(false);
        }
    }
}
