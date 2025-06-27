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
    public float stoppingDistance = 0.1f;

    void Awake()
    {
        IsUsing = false;
        IsSearching = false;
        IsTalking = false;
        anim = GetComponent<Animator>();
        target = transform.position;
    }

    void Update()
    {
        // Обработка клика (в Update для мгновенной реакции)
        if (!IsTalking && !IsUsing && !IsSearching && !UseOnItem.IsUse && !InventoryUI.IsInv)
        {
            if (Input.GetMouseButtonDown(0))
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ReflectPlayer();
                IsMoving = true;
                anim.SetBool("IsMoving", true);
            }
        }
    }

    void FixedUpdate()
    {
        // Физика перемещения (остается в FixedUpdate)
        if (IsMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);

            if (Vector2.Distance(transform.position, target) <= stoppingDistance)
            {
                IsMoving = false;
                anim.SetBool("IsMoving", false);
                transform.position = target;
            }
        }

        // Блокировка движения при диалогах/использовании
        if (IsTalking || IsUsing || IsSearching)
        {
            anim.SetBool("IsMoving", false);
            IsMoving = false;
            target = transform.position;
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
        target = transform.position;
        IsMoving = false;
        anim.SetBool("IsMoving", false);
    }

    public void ConvEnd()
    {
        IsTalking = false;
        IsSearching = false;
        IsUsing = false;
        UseOnItem.IsUse = false;
        Debug.Log("Talk end");
    }
}