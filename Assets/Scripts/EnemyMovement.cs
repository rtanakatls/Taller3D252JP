using System.Collections;
using UnityEngine;

public class EnemyMovement : Movement
{
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected float changeDirectionCooldown;


    protected override void Init()
    {
        base.Init();
        StartCoroutine(ChangeDirection());
    }

    protected IEnumerator ChangeDirection()
    {
        while (true)
        {
            direction *= -1;
            direction.Normalize();
            yield return new WaitForSeconds(changeDirectionCooldown);
        }
    }

    protected override void Move()
    {
        rb.linearVelocity = new Vector3(direction.x * movementSpeed, rb.linearVelocity.y, direction.z * movementSpeed);
    }

}
