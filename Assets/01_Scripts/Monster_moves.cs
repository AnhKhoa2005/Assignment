using UnityEngine;

public class moveSquare : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform A;
    [SerializeField] Transform B;
    Vector3 target;
    SpriteRenderer flip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = A.position;
        flip = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(this.transform.position, A.position) <= 0.1)
        {
            target = B.position;
            flip.flipX = true;
        }
        if (Vector2.Distance(this.transform.position, B.position) <= 0.1)
        {
            target = A.position;
            flip.flipX = false;
        }
        this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
    }
}
