using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float _maxHp = 2f;
    float _hp = 0f;

    Slider _hpSlider = null;
    GameObject _hpPanel = null;

    public float Hp
    {
        get { return _hp; }
        set { _hp = value; _hpSlider.value = _hp / _maxHp; }
    }

    protected virtual void Start()
    {
        _hp = _maxHp;
        _hpSlider = transform.GetComponentInChildren<Slider>(true);
        if (_hpSlider == null)
        {
            Debug.Log("Couldn't find Component");
        }

        _hpPanel = transform.Find("HPPanel").gameObject;
        if (_hpPanel == null)
        {
            Debug.Log("Couldn't find HPPanel");
        }
    }

    protected virtual void Update()
    {
        if (_hpSlider.isActiveAndEnabled == false && _hp / _maxHp < 1f)
        {
            _hpPanel.gameObject.SetActive(true);
        }

        if (_hp < 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 5f);
    }
}
