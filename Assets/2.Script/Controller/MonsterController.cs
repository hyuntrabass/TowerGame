using TreeEditor;
using UnityEngine;

public class MonsterController : CharacterController
{
    Rigidbody2D _rigid = null;

    Detector _frontColl = null;
    Detector _backColl = null;
    Detector _upColl = null;

    [SerializeField]
    float _speed = 3f;
    [SerializeField]
    float _reverseSpeed = 1f;
    [SerializeField]
    float _damage = 0.05f;

    const float _attackSpeed = 1f;
    float _attackTimer = 0f;

    protected override void Start()
    {
        base.Start();

        _rigid = GetComponent<Rigidbody2D>();
        if (_rigid == null)
        {
            Debug.Log("Couldn't find Component");
        }

        _frontColl = transform.Find("FrontDetect").GetComponent<Detector>();
        if (_frontColl == null)
        {
            Debug.Log("Couldn't find Component");
        }
        _backColl = transform.Find("BackDetect").GetComponent<Detector>();
        if (_backColl == null)
        {
            Debug.Log("Couldn't find Component");
        }
        _upColl = transform.Find("UpDetect").GetComponent<Detector>();
        if (_upColl == null)
        {
            Debug.Log("Couldn't find Component");
        }
    }

    protected override void Update()
    {
        base.Update();

        if (_frontColl.Monster == true && 
            _backColl.Monster == false)
        {
            Move(Vector3.up, _speed);
            Move(Vector3.left, _speed);
            _rigid.gravityScale = 0f;
        }
        else
        {
            _rigid.gravityScale = 1f;
        }

        if (NeedsToRetreat())
        {
            Move(Vector3.right, _reverseSpeed);
        }
        else if (_frontColl.Monster == false && _frontColl.Player == false)
        {
            Move(Vector3.left, _speed);
        }

        if (_attackTimer > 0f)
        {
            _attackTimer -= Time.deltaTime; 
        }

        if (_frontColl.Player && _attackTimer <= 0f)
        {
            _frontColl.Target.GetComponent<CharacterController>().Hp -= _damage;
            _attackTimer = _attackSpeed;
        }
    }

    void Move(Vector3 direction, float speed)
    {
        transform.position += direction * Time.deltaTime * speed;
    }

    bool NeedsToRetreat()
    {
        if (transform.position.x < -1.3f)
        {
            return true;
        }
        if (_frontColl.Monster)
        {
            return _frontColl.CollMonster.GetComponent<MonsterController>().NeedsToRetreat();
        }
        else if (_upColl.Monster)
        {
            return true;
        }

        return false;
    }

    protected override void Die()
    {
        MonsterManager.Instance.UnregisterMonster(gameObject);
        base.Die();
    }
}
