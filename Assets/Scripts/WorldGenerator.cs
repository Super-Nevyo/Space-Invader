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


    private List<GameObject>[] _startSet = new List<GameObject>[15];
    private List<GameObject>[] _set1 = new List<GameObject>[5];
    private List<GameObject>[] _set2 = new List<GameObject>[7];
    private List<GameObject>[] _set3 = new List<GameObject>[3];


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
        if (i == 0) _world = _set1;
        if (i == 1) _world = _set2;
        if (i == 2) _world = _set3;
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        GameObject temp = Instantiate(_tempContainer, new Vector2(_startDistance, -2), Quaternion.identity, null);
        for (int i = 0; i < _world.Length; ++i)
        {
            for (int j = 0; j < _world[i].Count; ++j)
            {
                Instantiate(_world[i][j],new Vector2(_startDistance + _chunkDistance * i, -2) , Quaternion.identity , temp.transform);
            }
        }
        _startDistance += _chunkDistance * _world.Length;
    }

}
