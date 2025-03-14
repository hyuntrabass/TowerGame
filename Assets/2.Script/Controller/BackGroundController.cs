using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField]
    float _scrollSpeed = 5f;

    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * _scrollSpeed;
        if (transform.position.x < -45f)
        {
            transform.position += Vector3.right * 37.9f * 3f;
        }
    }
}
