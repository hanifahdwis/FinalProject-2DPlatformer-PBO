using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : CharacterBase
{

    private float wallJumpTime;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] protected float jumpForce;

    private void Update()
    {
        HandleMovement();
    }
    protected override void HandleMovement()
    {
        //cek input keyboard player buat jalan
        horizontalInput = 0;
        if (Keyboard.current.aKey.isPressed) horizontalInput -= (float)1.5;
        if (Keyboard.current.dKey.isPressed) horizontalInput += (float)1.5;

        //balik animasi dari player
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3((float)1.5, (float)1.5, (float)1.5);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3((float)-1.5, (float)1.5, (float)1.5);

        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        //ubah animasi player
        anim.SetBool("Walk", horizontalInput != 0);
        anim.SetBool("Grounded", IsGrounded());

        //cek player loncat
        if (wallJumpTime > 0.2f)
        {
            if (onWall() && !IsGrounded())
            {
                 body.gravityScale = 0;
                 body.linearVelocity = Vector2.zero;
            }
            else
            {
                 body.gravityScale = 2;
                 body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();
            }
        }
        else
        {
            wallJumpTime += Time.deltaTime;
        }
    }

    //cek input keyboard player buat loncat
    private void Jump()
    {
        if (IsGrounded())
        {
            //loncat dari tanah
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }
        else if (onWall() && !IsGrounded())
        {
            float direction = -Mathf.Sign(transform.localScale.x);
            //loncat dari tembok
            body.linearVelocity = new Vector2(direction * 2, 5);
            transform.localScale = new Vector3((float)1.5 * direction, (float)1.5, (float)1.5);
            anim.SetTrigger("Jump");
        }
        wallJumpTime = 0;
    }
    private bool onWall()
    {
        //cek player nempel tembok
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return hit.collider != null;
    }
    public bool canAttack()
    {
        //cek player bisa serang
        return horizontalInput == 0 && IsGrounded() && !onWall();
    }
}
