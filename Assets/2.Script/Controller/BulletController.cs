using UnityEngine;

public class BulletController : MonoBehaviour
{
    float _speed = 40f;
    float _lifeTime = 2f;
    float _damage = 0.3f;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)Define.Layer.Monster)
        {
            collision.GetComponent<CharacterController>().Hp -= _damage;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
