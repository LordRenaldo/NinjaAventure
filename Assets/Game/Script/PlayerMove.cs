using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof (Rigidbody2D), typeof (PlayerInput))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb2D;
    Animator animator;
    Vector2 move;
    Vector2 faceDir = Vector2.down;
    bool inMotion;
    [SerializeField] float speed = 5f;

    public bool InMotion => inMotion;
    public Vector2 Move => move;

    void Awake ()
    {
        rb2D = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator> ();
    }

    public void OnMove ( InputValue inputValue )
    {
        move = inputValue.Get<Vector2> ();
        move = Vector2.ClampMagnitude (move, 1f);
        Debug.Log ($"MOVE = {move}");
    }
    void Update ()
    {
        if (move.sqrMagnitude > 0.001f)
            faceDir = move;

        animator.SetFloat ("MoveX", move.x);
        animator.SetFloat ("MoveY", move.y);

        animator.SetFloat ("FaceX", faceDir.x);
        animator.SetFloat ("FaceY", faceDir.y);

        animator.SetFloat ("Speed", move.sqrMagnitude);

    }
    void FixedUpdate ()
    {
        rb2D.MovePosition (rb2D.position + move * speed * Time.fixedDeltaTime);
    }
}
