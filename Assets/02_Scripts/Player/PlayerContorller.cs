using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContorller : MonoBehaviour
{
    PlayerInputAction inputAction;
    Animator anim;
    readonly int Attack_String = Animator.StringToHash("Attack");
    public BoxCollider2D attackCollider;
    Transform attackTransform;
    private void Awake()
    {
        inputAction = new PlayerInputAction();
        anim = GetComponent<Animator>();
        attackTransform = transform.GetChild(0);
        attackCollider = attackTransform.GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        inputAction.Player.Enable();
        inputAction.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputAction.Player.Attack.performed -= OnAttack;
        inputAction.Player.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.Log("OnAttack ´­¸²");
            anim.SetTrigger(Attack_String);
            StartCoroutine(Attack());
        }
    }

    private void Update()
    {
        
    }

    IEnumerator Attack()
    {
        attackCollider.enabled = false;
        attackCollider.enabled = true; 
        yield return new WaitForSeconds(0.3f);
        attackCollider.enabled = false;
    }
}
