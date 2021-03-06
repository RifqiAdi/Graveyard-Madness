using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    private float timeToAttack;
    [SerializeField] private float startTimeToAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeToAttack -= Time.deltaTime;
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
        {
            animator.SetBool("isAttack", false);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (timeToAttack <= 0)
        {
            animator.SetBool("isAttack", true);
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                Debug.Log("damaged" + damage);
            }
            Debug.Log("Fire");
            timeToAttack = startTimeToAttack;
        }
    }

  
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
