using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mergeable : MonoBehaviour
{
    TapHandler taphandler;
    bool grabbed; 

    public float basex, basey;

    public int mergelv = 0;

    [SerializeField]
    Sprite[] icons;

    SpriteRenderer spriteRenderer;

    Collider2D currentCollision;

    // Start is called before the first frame update
    void Start()
    {
        taphandler = GetComponent<TapHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        basex = transform.position.x;
        basey = transform.position.y;
        UpdateIcon();
    }

    // Update is called once per frame
    void Update()
    {
        if (!grabbed){
            if (Input.GetMouseButtonDown(0) && !GameController.GC.finished_dish){
                Vector2 c = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (taphandler.checkTouch(c)){
                    grabbed = true;
                    if (!GameController.GC.first_touch){
                        GameController.GC.EndTutorial();
                    }
                    spriteRenderer.sortingOrder = 3;
                }
            }
        } else {
            Vector2 c = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = c;
            if (transform.position.y >= 2.5 || transform.position.y <= -4.8 || transform.position.x <= -3 || transform.position.x >= 3){
                GameController.GC.WrongSound();
                Release();
            } else if (Input.GetMouseButtonUp(0)){
                if (currentCollision != null){
                    if (currentCollision.gameObject.GetComponent<Mergeable>().mergelv == mergelv){
                        basex = currentCollision.gameObject.GetComponent<Mergeable>().basex; 
                        basey = currentCollision.gameObject.GetComponent<Mergeable>().basey; 
                        currentCollision.gameObject.SetActive(false);
                        LevelUp();
                    } else {
                        GameController.GC.WrongSound();
                    }
                } else {
                   GameController.GC.WrongSound(); 
                }
                Release();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        currentCollision = collision; 
    }

    void OnTriggerExit2D(){
        currentCollision = null;
    }

    void OnTriggerStay2D(Collider2D collision){
        currentCollision = collision;
    }


    void LevelUp(){
        if (mergelv < icons.Length - 1){
            mergelv++; 
            UpdateIcon();
            GameController.GC.Progress(basex, basey);
        }

    }

    void UpdateIcon(){
        try {
            spriteRenderer.sprite = icons[mergelv];
        } catch(Exception e){
            Debug.Log(e);
        }
    }

    void Release(){
        if (GameController.GC.finished_dish){
            basex = 0f;
            basey = -1.05f;
        }
        transform.position = new Vector3(basex, basey, 0);
        grabbed = false;
        spriteRenderer.sortingOrder = 2;
    }
}
