using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // this is a Serialized field fore testing
    [SerializeField] private GameState _currentState;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float allowedSetDistance;
    [SerializeField] private float totalTime;
    public static GameManager instance;
    private List<GameObject> _setContainer = new List<GameObject>();
    public System.Action<float> NewLeftBound;
    public System.Action SpawnNewLevel;
    private GameObject _player;
    private float _lastInputTime = 0;
    private int _score = 0;
    private float _startTime;
    private bool _waiting = false;
    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
    }
    

    void OnEnable()
    {
        inputManager.AnyInputEvent += AnyInputPressed;
    }

    void OnDisable()
    {
        inputManager.AnyInputEvent -= AnyInputPressed;
    }

    public void Starting()
    {
        if (_currentState == GameState.START)
        {
            UIMenuing.instance.StartCanvas();
        }
        else if (_currentState == GameState.MENUING)
        {

        }
        else if (_currentState == GameState.PLAYING)
        {
            Debug.Log("made it here");
            _score = 0;
            _startTime = Time.time;
            _player = GameObject.FindGameObjectWithTag("Player");
            NewLeftBound?.Invoke(0);
        }
        else if (_currentState == GameState.END)
        {
            UIMenuing.instance.EndCanvas(_score);
            while (_setContainer.Count > 0)
            _setContainer.RemoveAt(0);
            _waiting = true;
            StartCoroutine(WaitAfterEnd());
        }
    }

    void Update()
    {
        if (Time.time > _lastInputTime + 180)
        {
            Application.Quit();
        }
        if (_currentState == GameState.START)
        {

        }
        else if (_currentState == GameState.MENUING)
        {

        }
        else if (_currentState == GameState.PLAYING)
        {
            if (_setContainer.Count != 0) 
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
                UIPlaying.instance.UpdateTime(_startTime - Time.time + totalTime);
            }
            if(_startTime - Time.time + totalTime <= 0)
            {
                EndTheGame();
            }
        }
        else if (_currentState == GameState.END)
        {

        }
    }

    private void AnyInputPressed()
    {
        _lastInputTime = Time.time;
        if (_currentState == GameState.START)
        {
            UIMenuing.instance.MenuCanvas();
            _currentState = GameState.MENUING;
        }
        else if (_currentState == GameState.MENUING)
        {
            _currentState = GameState.PLAYING;
            SceneManager.LoadScene("Level");
        }
        else if (_currentState == GameState.PLAYING)
        {

        }
        else if (_currentState == GameState.END)
        {
            if (!_waiting)
            {
                UIMenuing.instance.StartCanvas();
                _currentState = GameState.START;
            }
        }
    }
    public void AddToSetList(GameObject NewAddition)
    {
        _setContainer.Add(NewAddition);
    }
    public void AddToScore(int Score)
    {
        _score += Score;
        UIPlaying.instance.UpdateScore(_score);
        Debug.Log("score " + _score);
    }

    public void EndTheGame()
    {
        _currentState = GameState.END;
        SceneManager.LoadScene("StartScene");
    }

    private IEnumerator WaitAfterEnd()
    {
        _waiting = true;
        yield return new WaitForSeconds(3f);
        _waiting = false;
    }
}
