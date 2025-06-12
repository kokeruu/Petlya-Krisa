using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Vector2 target;
    public static Animator anim;
    public static bool IsMoving = false;
    private bool FaceRight = false;
    public static bool IsTalking = false;
    public static bool IsUsing = false;
    public static bool IsSearching = false;
    public float speed = 30f;
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
        if (IsMoving && target == new Vector2(transform.position.x, transform.position.y))
        {
                IsMoving = false;
                anim.SetBool("IsMoving", false);
        }
        if (IsTalking || IsUsing || IsSearching)
        {
            anim.SetBool("IsMoving", false);
            IsMoving = false;
            target = new Vector2(transform.position.x, transform.position.y);
            

        }
<<<<<<< Updated upstream
        if (!IsTalking && !IsUsing && !IsSearching)
=======
        else
>>>>>>> Stashed changes
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


            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
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
        PlayerController.IsSearching = false;
        PlayerController.IsUsing = false;
        Debug.Log("Talk end: ");
    }
        public void SearchEnd()
    {
        PlayerController.IsSearching = false;
        Debug.Log("Search end: ");
    }
}