using System.Collections;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    private bool canHit = true;
    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !QuickSlots.Instance.usingGun)
        {
            punch();
        }
    }
    private void punch()
    {
        int leftOrRight;
        leftOrRight = Random.Range(0, 2);
        animator.SetInteger("LeftOrRight", leftOrRight);
        animator.SetTrigger("LeftClick");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Destructible") && canHit)
        {
            collision.GetComponent<BoxDestruction>()?.takeDamage();
            StartCoroutine(HitCooldown());
            
        }
    }
    private IEnumerator HitCooldown()
    {
        canHit = false;
        yield return new WaitForSeconds(0.5f);
        canHit = true;
    }
    private void EnableBoxCollider()
    {
        boxCollider.enabled = true;
    }
    private void offBoxCollider()
    {
        boxCollider.enabled = false;
    }
   
}
