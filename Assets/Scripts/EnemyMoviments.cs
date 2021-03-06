using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMoviments : MonoBehaviour
{
    [FormerlySerializedAs("_moveSpeed")] [SerializeField]
    private float moveSpeed = 1f;
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacintRight())
        {
            _rigidbody2D.velocity = new Vector2(moveSpeed, 0f);
            return;
        }

        _rigidbody2D.velocity = new Vector2(-moveSpeed, 0f);
    }

    private bool IsFacintRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_rigidbody2D.velocity.x)), 1f);
    }
}
