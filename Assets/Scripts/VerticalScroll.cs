using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Tooltip("Game units per second")]
    [SerializeField] private float scrollRate = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var yMove = scrollRate * Time.deltaTime;
        transform.Translate(new Vector2(0f, yMove));
    }
}
