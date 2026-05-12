using UnityEngine;

public class GameManager : MonoBehaviour
{
    // this is a Serialized field fore testing
    [SerializeField] private GameState _currentState;
    public static GameManager instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
        //_currentState = GameState.START;
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

        }
        else if (_currentState == GameState.END)
        {

        }
    }

    void Update()
    {
        
    }
}
