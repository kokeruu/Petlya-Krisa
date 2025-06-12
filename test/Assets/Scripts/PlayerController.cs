using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Vector2 target;
    public static Animator anim;
    public bool IsMoving = false;
    private bool FaceRight = false;
    public static bool IsTalking = false;
    public static bool IsUsing = false;
    public static bool IsSearching = false;
    void Awake()
    {
        IsUsing = false;
        IsSearching = false;
        IsTalking = false;
        anim = GetComponent<Animator>();
        target = new Vector2(transform.position.x, transform.position.y);
    }

    void FixedUpdate()
    {
        if (IsMoving)
        {
            if (target == new Vector2(transform.position.x, transform.position.y))
            {
                IsMoving = false;
                anim.SetBool("IsMoving", false);
            }
        }
        if (IsTalking || IsUsing || IsSearching)
        {

            target = new Vector2(transform.position.x, transform.position.y);
            IsMoving = false;
            anim.SetBool("IsMoving", false);

        }
        if (!IsTalking && !IsUsing && !IsSearching)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {



                target = new Vector2(mousePos.x, mousePos.y);
                if (target.y > mousePos.y) target.y = target.y - 20;
                else target.y = target.y + 20;
                ReflectPlayer();
                IsMoving = true;
                anim.SetBool("IsMoving", true);

            }


            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 30f);
        }
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        target = new Vector2(transform.position.x, transform.position.y);
        IsMoving = false;
        anim.SetBool("IsMoving", false);

    }
    public void ConvEnd()
    {
        PlayerController.IsTalking = false;
        Debug.Log("Talk end: ");
    }
        public void SearchEnd()
    {
        PlayerController.IsSearching = false;
        Debug.Log("Search end: ");
    }
}