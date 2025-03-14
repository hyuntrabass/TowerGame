using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField]
    int _monster = 0;
    [SerializeField]
    int _player = 0;
    public bool Monster { get { return _monster != 0; } }
    public bool Player { get { return _player != 0; } }

    GameObject _target = null;
    public GameObject Target { get { return _target; } }

    GameObject _collidedMonster = null;
    public GameObject CollMonster { get { return _collidedMonster; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        if (layer == (int)Define.Layer.Monster)
        {
            _monster++;
            _collidedMonster = collision.gameObject;
        }

        if (layer == (int)Define.Layer.Box || layer == (int)Define.Layer.Player)
        {
            _player++;
            _target = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        if (layer == (int)Define.Layer.Monster)
        {
            _collidedMonster = collision.gameObject;
        }

        if (layer == (int)Define.Layer.Box || layer == (int)Define.Layer.Player)
        {
            _target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        if (layer == (int)Define.Layer.Monster)
        {
            _monster--;
            if (_monster <= 0)
            {
                _collidedMonster = null;
            }
        }

        if (layer == (int)Define.Layer.Box || layer == (int)Define.Layer.Player)
        {
            _player--;
            if (_player <= 0)
            {
                _target = null;
            }
        }
    }
}
