using Unity.VisualScripting;
using UnityEngine;

public class Trap_Shuriken : MonoBehaviour
{
    [SerializeField] float speedTrap;
    [SerializeField] Transform A, B;
    Vector3 Target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = A.position;
        Target = B.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(this.transform.position, Target) < 0.1f)
        {
            if (Target == A.position)
            {
                Target = B.position;
            }
            else
            {
                Target = A.position;
            }
        }
        this.transform.position = Vector2.MoveTowards(this.transform.position, Target, speedTrap * Time.deltaTime);
    }
}
