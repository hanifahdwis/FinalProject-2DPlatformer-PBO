using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CharacterBase : MonoBehaviour
{
    // Base class buat player dan enemy 
    protected Rigidbody2D body;
    protected BoxCollider2D boxCollider;
    protected Animator anim;
    protected float horizontalInput;
    [SerializeField] protected float speed;
    [SerializeField] protected LayerMask groundLayer;

    protected void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    protected abstract void HandleMovement();

    protected bool IsGrounded()
    {
        //cek player ditanah
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }
}
