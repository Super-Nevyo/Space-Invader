using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private List<GameObject>[] _world = new List<GameObject>[5];
    [SerializeField] private List<GameObject> _tempList;
    private int _chunkDistance = 1;
    private int _startDistance = -5;
    [SerializeField] private GameObject _tempContainer;
    [SerializeField] private GameObject example1;
    [SerializeField] private GameObject example2;
    [SerializeField] private GameObject example3;


    private List<GameObject>[] _example = new List<GameObject>[5];
    //{ new List<GameObject>(2) { example1, example2 },
    //    new List<GameObject>(3) { example1, example2, example3 },
    //    new List<GameObject>(1) { example1 },
    //    new List<GameObject>(2) { example1, example3 },
    //    new List<GameObject>(1) { example1 } };
    private int[] _testArray = new int[3] { 1, 2, 3 };
    private List<int> _testList = new List<int>(3) { 1, 2, 3 };


    private void Start()
    {
        _example = new List<GameObject>[] { new List<GameObject>(2) { example1, example2 }, new List<GameObject>(3) { example1, example2, example3 }, new List<GameObject>(1) { example1 }, new List<GameObject>(2) { example1, example3 }, new List<GameObject>(1) { example1 } };
        //_example[0] = new List<GameObject> { example1, example2 };
        //_example[1] = new List<GameObject> { example1, example2, example3 };
        //_example[2] = new List<GameObject> { example1 };
        //_example[3] = new List<GameObject> { example1, example3 };
        //_example[4] = new List<GameObject> { example1 };
        for (int i = 0; i < _world.Length; i++)
        {
            _world[i] = _tempList;
        }

        SpawnObjects();
        _tempList.RemoveAt(_tempList.Count-1);
        for (int i = 0; i < _world.Length; i++)
        {
            _world[i] = _tempList;
        }
        SpawnObjects();
        _tempList.RemoveAt(_tempList.Count-1);
        for (int i = 0; i < _world.Length; i++)
        {
            _world[i] = _tempList;
        }
        SpawnObjects();
        _world = _example;
        SpawnObjects();
        SpawnObjects();
        SpawnObjects();
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        GameObject temp = Instantiate(_tempContainer);
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
