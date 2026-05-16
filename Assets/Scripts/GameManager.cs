using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // this is a Serialized field fore testing
    [SerializeField] private GameState _currentState;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float allowedSetDistance;
    public static GameManager instance;
    private List<GameObject> _setContainer = new List<GameObject>();
    public System.Action<float> NewLeftBound;
    public System.Action SpawnNewLevel;
    private GameObject _player;
    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
        //_currentState = GameState.START;
    }
    

    void OnEnable()
    {
        inputManager.AnyInputEvent += AnyInputPressed;
    }

    void OnDisable()
    {
        inputManager.AnyInputEvent -= AnyInputPressed;
    }

    private void Start()
    {
        if (_currentState == GameState.START)
        {

        }
        else if (_currentState == GameState.MENUING)
        {

        }
        else if (_currentState == GameState.PLAYING)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            NewLeftBound?.Invoke(0);
        }
        else if (_currentState == GameState.END)
        {

        }
    }

    void Update()
    {
        if (_currentState == GameState.START)
        {

        }
        else if (_currentState == GameState.MENUING)
        {

        }
        else if (_currentState == GameState.PLAYING)
        {
            if (_setContainer[0].transform.position.x < _player.transform.position.x - allowedSetDistance)
            {
                Destroy(_setContainer[0]);
                _setContainer.RemoveAt(0);
                NewLeftBound?.Invoke(_setContainer[0].transform.position.x);
            }
            if (_setContainer[_setContainer.Count - 1].transform.position.x < _player.transform.position.x + allowedSetDistance)
            {
                
                SpawnNewLevel?.Invoke();
            }
        }
        else if (_currentState == GameState.END)
        {

        }
    }

    private void AnyInputPressed()
    {
        if (_currentState == GameState.START)
        {

        }
        else if (_currentState == GameState.MENUING)
        {

        }
        else if (_currentState == GameState.PLAYING)
        {

        }
        else if (_currentState == GameState.END)
        {

        }
    }
    public void AddToSetList(GameObject NewAddition)
    {
        _setContainer.Add(NewAddition);
    }
}
