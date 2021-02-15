using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] AudioClip howlSound;
    [SerializeField] float howlTime;


    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            animator.SetBool("howl", true);
            AudioSource.PlayClipAtPoint(howlSound, transform.position);
            StartCoroutine(StopHowlAnimation());
        }

        
    }

    IEnumerator StopHowlAnimation()
    {
        yield return new WaitForSeconds(howlTime);
        animator.SetBool("howl", false);
    }
}
