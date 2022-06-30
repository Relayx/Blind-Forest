using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    RUN,
    JUMP,
    FALL,
    FLY
}



public class PlayerController : MonoBehaviour
{
    public Button2 JumpButton;

    private Rigidbody2D body;

    [HideInInspector]
    public PlayerStates state { get; private set; }

    private void Start() 
    {
        body = GetComponent<Rigidbody2D>();

        state = PlayerStates.RUN;
    }

    private void FixedUpdate() 
    {
        switch (state)
        {
            case PlayerStates.RUN:
                Run();
            break;

            case PlayerStates.JUMP:
                Jump();
            break;

            case PlayerStates.FALL:
                Fall();
            break;

            case PlayerStates.FLY:
                Fly();
            break;

            default:
                Debug.LogError("Error state");
            break;
        }
    }

// ------------------------> Run <-------------------------

    private void Run()
    {
        if (JumpButton.Pressed)
        {
            state = PlayerStates.JUMP;
        }
    }
// --------------------------------------------------------



// ------------------------> Jump <------------------------

    [Header("Jump")]
    public float JumpHeight;
    public float CancelRate;
    public float FallBorder;

    public float JumpRayDistance;
    public LayerMask GroundLayer;

    private bool isGrounded => Physics2D.Raycast(body.position, 
                                                 Vector2.down, 
                                                 JumpRayDistance, 
                                                 GroundLayer);

    private void Jump()
    {
        if (isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, 0f);
            float jumpForce = Mathf.Sqrt(JumpHeight * -2 * 
                                        (Physics2D.gravity.y * body.gravityScale));
            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (!JumpButton.Holded && body.velocity.y > 0f) 
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * CancelRate);
            if (JumpButton.Pressed)
            {
                state = PlayerStates.FLY;
            }
        }

        if (body.velocity.y < FallBorder) 
        {
            state = PlayerStates.FALL;
        }
    }

// --------------------------------------------------------



    [Header("Fall")]
    public float FallMaxVelocity;
    public float FallMultiplier;

// ------------------------> Fall <------------------------

    private void Fall()
    {
        if (isGrounded)
        {
            state = PlayerStates.RUN;
        }

        if (JumpButton.Pressed)
        {
            state = PlayerStates.FLY;
        }

        body.velocity += Vector2.up * Physics2D.gravity.y * 
                         body.gravityScale * FallMultiplier * Time.fixedDeltaTime;
        body.velocity = new Vector2(body.velocity.x,
            Mathf.Clamp(body.velocity.y, -FallMaxVelocity, float.MaxValue));
    }

// --------------------------------------------------------


    [Header("Fly")]
    public float FlySmooth;
    public float MaxDistToGround;

// ------------------------> Fly <-------------------------

    private void Fly()
    {
        if (Physics2D.Raycast(body.position, Vector2.down, MaxDistToGround, GroundLayer))
        {
            state = PlayerStates.FALL;
        }

        if (JumpButton.Released)
        {
            state = PlayerStates.FALL;
        }
        else
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * FlySmooth);
        }
    }

// --------------------------------------------------------

}
