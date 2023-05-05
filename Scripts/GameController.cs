using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController GC;
    int level = 1;
    public int stage = 0;
    public bool finished_dish;
    public bool first_touch;
    public GameObject level_one;
    public GameObject level_two;
    public GameObject merge_sparks; 
    public GameObject complete_sparks;
    public GameObject progress_indicator;
    public GameObject character;
    public GameObject tutorial_hand;
    public GameObject second_msg;
    SpriteRenderer prg_SpriteRenderer, char_SpriteRenderer;
    public Sprite[] lv_one_icons; 
    public Sprite[] lv_two_icons;
    public Sprite[] char_sprites;
    public AudioSource sfx_merge; 
    public AudioSource sfx_complete;
    public AudioSource sfx_wrong;

    Animator merge_anim, complete_anim;
    bool double_flag;

    void Awake()
    {
        GC = this;
        merge_anim = merge_sparks.GetComponent<Animator>();
        complete_anim = complete_sparks.GetComponent<Animator>();
        prg_SpriteRenderer = progress_indicator.GetComponent<SpriteRenderer>();
        char_SpriteRenderer = character.GetComponent<SpriteRenderer>();
        //DontDestroyOnLoad(this);
    }

    void Update()
    {
        
    }

    public void Progress(float x, float y){
        stage++;
        if (level == 1){
            if (stage >= 3){
                finished_dish = true;
                second_msg.SetActive(true);
                StartCompleteSparks(1);
            } else {
                StartMergeSparks(x, y);
            }
            prg_SpriteRenderer.sprite = lv_one_icons[stage];
        }
        if (level == 2){
            if (stage == 2 && !double_flag){
                stage = 1;
                double_flag = true;
            }
            if (stage >= 5){
                finished_dish = true;
                StartCompleteSparks(2);
            } else {
                StartMergeSparks(x, y);
            }
            prg_SpriteRenderer.sprite = lv_two_icons[stage];
        }
        char_SpriteRenderer.sprite = char_sprites[1];
        StartCoroutine(ResetChar());
        
    }

    void StartMergeSparks(float x, float y){
        sfx_merge.Play();
        merge_sparks.transform.position = new Vector3(x, y, 0);
        merge_sparks.SetActive(true);
        StartCoroutine(StopAnim(merge_sparks, merge_anim));
    }

    void StartCompleteSparks(int transition_flag = 0){
        sfx_complete.Play();
        complete_sparks.SetActive(true);
        StartCoroutine(StopAnim(complete_sparks, complete_anim, transition_flag));
    }

    IEnumerator StopAnim(GameObject obj, Animator animator, int transition_flag = 0){
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        obj.SetActive(false);
        if (transition_flag == 1){
            level_two.SetActive(true);
            level_one.SetActive(false);
            level = 2; 
            stage = 0;
            prg_SpriteRenderer.sprite = lv_two_icons[stage];
            finished_dish = false;
            char_SpriteRenderer.sprite = char_sprites[0];
        } else if (transition_flag == 2){
            SceneManager.LoadScene("CTA");
        }
    }

    IEnumerator ResetChar(){
        yield return new WaitForSeconds(0.5f);
        if (!finished_dish){
            char_SpriteRenderer.sprite = char_sprites[0];
        }
    }

    public void EndTutorial(){
        tutorial_hand.SetActive(false);
        char_SpriteRenderer.sprite = char_sprites[0];
    }

    public void WrongSound(){
        sfx_wrong.Play();
    }
    
}
