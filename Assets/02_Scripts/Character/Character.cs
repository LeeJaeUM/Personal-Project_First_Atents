using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private bool isDieCheck = false;
    public int maxHp = 5;
    [SerializeField]
    private int hp;
    public int Hp
    {
        get => hp;
        private set
        {
            if(hp != value)
            {
                hp = value; 
            }
        }
    }

    Animator anim;
    private int isHitted_String = Animator.StringToHash("isHitted");

    private void Awake()
    {
        Hp = maxHp;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Die();
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        anim.SetTrigger(isHitted_String);
    }

    void Die()
    {
        if(isDieCheck)
        {
            return;
        }
        if(hp <= 0)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(0.5f,0.5f,0.5f,0.5f);
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;
            Destroy(gameObject, 0.5f);
            isDieCheck = true;
        }
    }

    public void Checked(int index)
    {
        Debug.Log($"지금 {index}의 위치의 타일에 있다.");
    }
}
