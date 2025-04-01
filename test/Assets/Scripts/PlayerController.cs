using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 target;
    public Animator anim;
    private bool IsMoving = false;
    private bool FaceRight = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsMoving)
        {
            if (target == new Vector2(transform.position.x, transform.position.y))
            {
                IsMoving = false;
                anim.SetBool("IsMoving", false);
            }
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            target = new Vector2(mousePos.x, mousePos.y);
            ReflectPlayer();
            IsMoving = true;
            anim.SetBool("IsMoving", true);
        }

        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 2f);
    }

    void ReflectPlayer()
    {
        if ((target.x > transform.position.x && !FaceRight) || (target.x < transform.position.x && FaceRight))
        {
            Vector3 temp = transform.localScale;
            temp.x = Mathf.Abs(temp.x) * (FaceRight ? 1 : -1);
            transform.localScale = temp;
            FaceRight = !FaceRight;
        }
    }
}