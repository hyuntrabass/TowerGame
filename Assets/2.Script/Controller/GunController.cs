using System.Drawing;
using UnityEngine;

public class GunController : MonoBehaviour
{
    GameObject _bulletPrefab;
    [SerializeField]
    int _bulletCount = 12;
    [SerializeField]
    float _spreadAngle = 20f;

    float _timer = 0f;
    [SerializeField]
    float _attackSpeed = 1f;

    const float _angleOffset = -32f;

    Texture2D _cursorClicked = null;
    Texture2D _cursorIdle = null;
    bool _isClicked = false;
    Vector2 _cursorHotspot = new Vector2(256f, 256f);

    void Start()
    {
        _bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        if (_bulletPrefab == null)
        {
            Debug.Log("Can't find Resource");
        }
        _cursorClicked = Resources.Load<Texture2D>("Textures/Cursor/Clicked");
        if (_cursorClicked == null)
        {
            Debug.Log("Can't find Resource");
        }
        _cursorIdle = Resources.Load<Texture2D>("Textures/Cursor/Idle");
        if (_cursorIdle == null)
        {
            Debug.Log("Can't find Resource");
        }

        SetCursor(true);
        SetCursor(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            AimAtTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            SetCursor(true);
        }
        else
        {
            GameObject target = MonsterManager.Instance.FindNearestMonster(transform.position);
            if (target == null)
            {
                return;
            }

            AimAtTarget(target.transform.position);
            
            SetCursor(false);
        }

        if (_timer > _attackSpeed)
        {
            _timer = 0f;
            Fire();
        }
        _timer += Time.deltaTime;
    }

    void AimAtTarget(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += _angleOffset;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Fire()
    {
        for (int i = 0; i < _bulletCount; i++)
        {
            float randomAngle = Random.Range(-_spreadAngle, _spreadAngle) - _angleOffset;
            Quaternion bulletRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + randomAngle);

            GameObject bullet = Instantiate(_bulletPrefab, transform.position, bulletRotation, transform);
        }
    }

    void SetCursor(bool isClicked)
    {
        if (isClicked == _isClicked)
        {
            return;
        }

        if (isClicked)
        {
            Cursor.SetCursor(_cursorClicked, _cursorHotspot, CursorMode.Auto); 
        }
        else
        {
            Cursor.SetCursor(_cursorIdle, _cursorHotspot, CursorMode.Auto); 
        }

        _isClicked = isClicked;
    }
}
