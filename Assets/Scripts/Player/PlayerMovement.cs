using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;
    private Rigidbody2D rb2d;
    private Animator animator;

    private const string lookX = "LookX";
    private const string lookY = "LookY";
    private const string skeleton = "Skeleton";

    public bool isPaused;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            return;
        }

        movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        rb2d.linearVelocity = movement * moveSpeed;

        if (movement != Vector2.zero)
        {
            animator.SetFloat(lookX, movement.x);
            animator.SetFloat(lookY, movement.y);
        }
    }

    public void SetSkeleton(bool isSkeleton)
    {
        animator.SetBool(skeleton, isSkeleton);
    }
}
