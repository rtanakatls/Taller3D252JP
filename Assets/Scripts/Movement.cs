using UnityEngine;

public class Movement : MonoBehaviour
{
    protected Rigidbody rb;
    [SerializeField] protected float movementSpeed;

    protected void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected void Update()
    {
        Move();
    }

    protected virtual void Move()
    {

    }

}
