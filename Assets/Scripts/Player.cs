using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField]
    private float runSpeed = 5f;

    [SerializeField]
    private float jumpSpeed = 5f;

    // State

    // Cached component references
    private Rigidbody2D _myRigidBody;
    private Animator _myAnimator;
    private Collider2D _myCollider2D;

    // Message then methods

    // Start is called before the first frame update
    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _myCollider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        FlipSprite();
    }

    private void Run()
    {
        var controlThrow = Input.GetAxis("Horizontal");

        var playerVelocity = new Vector2(
            controlThrow * runSpeed,
            _myRigidBody.velocity.y
        );

        _myRigidBody.velocity = playerVelocity;

        var playerHorizontalSpeed = Mathf.Abs(_myRigidBody.velocity.x) > Mathf.Epsilon;
        _myAnimator.SetBool("Running", playerHorizontalSpeed);
    }

    private void Jump()
    {
        if (!_myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            var jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            _myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void FlipSprite()
    {
        var playerHorizontalSpeed = Mathf.Abs(_myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_myRigidBody.velocity.x), 1f);
        }
    }
}
