using UnityEngine;

public class WheelController : MonoBehaviour
{
    float _rotationSpeed = 720f;

    void Update()
    {
        transform.Rotate(Vector3.back * _rotationSpeed * Time.deltaTime);
    }
}
