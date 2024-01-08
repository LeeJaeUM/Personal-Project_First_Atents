using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool isMoving = false;      //이동중인지 확인
    public float oneMovingTime = 1.0f; //몇 초마다 움직일건지 

    Animator anim;
    readonly int Move_String = Animator.StringToHash("Move");
    private void Awake()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(OneMove()); 
    }

    IEnumerator OneMove()
    {
        while (true)
        {
            Vector2 targetPosition = new Vector2(transform.position.x - 3, transform.position.y);
            isMoving = true;
            yield return new WaitForSeconds(oneMovingTime);
            anim.SetTrigger(Move_String);
            transform.position = targetPosition;
            isMoving = false;
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy touch Player_coL");
        }
        
    }

}
