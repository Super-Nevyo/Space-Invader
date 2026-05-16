using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private List<GameObject>[] _world;
    private int _chunkDistance = 1;
    private int _startDistance = -5;
    [SerializeField] private GameObject _tempContainer;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject background;


    private List<GameObject>[] _startSet = new List<GameObject>[15];
    private List<GameObject>[] _set1 = new List<GameObject>[5];
    private List<GameObject>[] _set2 = new List<GameObject>[7];
    private List<GameObject>[] _set3 = new List<GameObject>[3];


    private void OnEnable()
    {
        StartCoroutine(EnableAfter());
    }
    private void OnDisable()
    {
        GameManager.instance.SpawnNewLevel -= PickASetAndSpawn;
    }

    private void Start()
    {
        _startSet = new List<GameObject>[] { new List<GameObject>(1) { floor}, new List<GameObject>(1) { floor}, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor } };
        _set1 = new List<GameObject>[] { new List<GameObject>(1) { floor, enemy }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor, enemy }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor } };
        _set2 = new List<GameObject>[] { new List<GameObject>(1) { floor, obstacle }, new List<GameObject>(1) { floor, enemy }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor, enemy }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor, obstacle } };
        _set3 = new List<GameObject>[] { new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor }, new List<GameObject>(1) { floor } };
        _world = _startSet;
        SpawnObjects();
        SpawnObjects();
        for (int i = 0; i < 10; i++)
        {
            PickASetAndSpawn();
                }
    }
    private void PickASetAndSpawn()
    {
        int i = Random.Range(0, 3);
        if (i == 0) { _world = _set1; Debug.Log("1"); }
        if (i == 1) { _world = _set2; Debug.Log("2"); }
        if (i == 2) { _world = _set3; Debug.Log("3"); }
            SpawnObjects();
    }

    private void SpawnObjects()
    {
        GameObject temp = Instantiate(_tempContainer, new Vector2(_startDistance, -2), Quaternion.identity, null);
        GameManager.instance.AddToSetList(temp);
        for (int i = 0; i < _world.Length; ++i)
        {
            if((_startDistance + _chunkDistance * i) % 8 == 0) { Instantiate(background, new Vector2(_startDistance + _chunkDistance * i, 2), Quaternion.identity, temp.transform); Instantiate(background, new Vector2(_startDistance + _chunkDistance * i, -6), Quaternion.identity, temp.transform); }
            for (int j = 0; j < _world[i].Count; ++j)
            {
                Instantiate(_world[i][j],new Vector2(_startDistance + _chunkDistance * i, -2) , Quaternion.identity , temp.transform);
            }
        }
        _startDistance += _chunkDistance * _world.Length;
    }
    private IEnumerator EnableAfter()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.SpawnNewLevel += PickASetAndSpawn;
    }
}


