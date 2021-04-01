using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField]
    private float runSpeed = 5f;

    [SerializeField]
    private float jumpSpeed = 5f;

    [SerializeField]
    private float climbSpeed = 5f;

    [SerializeField]
    private Vector2 deathKick = new Vector2(10f, 10f);

    // State
    private bool isAlive = true;

    // Cached component references
    private Rigidbody2D _myRigidBody;
    private Animator _myAnimator;
    private CapsuleCollider2D _myBodyCollider2D;
    private BoxCollider2D _myFeetCollider2D;
    private float _gravityScaleAtStart;

    // Message then methods

    // Start is called before the first frame update
    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        _gravityScaleAtStart = _myRigidBody.gravityScale;
        _myFeetCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive)
        {
            return;
        }

        Run();
        Jump();
        FlipSprite();
        ClimbLadder();
        Die();
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

    private void ClimbLadder()
    {
        if (!_myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            _myAnimator.SetBool("Climbing", false);
            _myRigidBody.gravityScale = _gravityScaleAtStart;
            return;
        }

        var controlThrow = Input.GetAxis("Vertical");
        var climbVelocity = new Vector2(_myRigidBody.velocity.x, controlThrow * climbSpeed);
        _myRigidBody.velocity = climbVelocity;
        _myRigidBody.gravityScale = 0f;

        var playerHasVerticalSpeed = Mathf.Abs(_myRigidBody.velocity.y) > Mathf.Epsilon;
        _myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void Jump()
    {
        if (!_myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            var jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            _myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void Die()
    {
        if (_myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            _myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
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
