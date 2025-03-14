using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    static MonsterManager s_Instance;
    public static MonsterManager Instance { get { Init(); return s_Instance; } }

    float _timer;
    GameObject _zombiePrefab;
    GameObject _parent;

    List<GameObject> _monsters = new List<GameObject>();

    static void Init()
    {
        if (s_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<MonsterManager>();
            }

            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<MonsterManager>();
        }
    }

    void Start()
    {
        _parent = new GameObject("Monsters");
        _zombiePrefab = Resources.Load<GameObject>("Prefabs/ZombieMelee");
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 1f)
        {
            _timer = 0f;
            GameObject monster = Instantiate(_zombiePrefab, _parent.transform);
            monster.transform.position = new Vector3(10f, -3f, 0f);
            _monsters.Add(monster);
        }
    }

    public GameObject FindNearestMonster(Vector3 position)
    {
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject monster in _monsters)
        {
            float distance = Vector2.Distance(position, monster.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = monster;
            }
        }

        return nearest;
    }

    public void UnregisterMonster(GameObject monster)
    {
        _monsters.Remove(monster);
    }
}
