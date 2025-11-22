using UnityEngine;

public class RotateObject : MonoBehaviour
{

    private void Update()
    {
        transform.Rotate(-Vector3.forward * Time.deltaTime * 360);
    }
}
