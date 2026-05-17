using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _camLeftBound;
    private float _camUpBound;
    private Transform _player;
    [SerializeField] float _camMoveSpeed;
    [SerializeField] float _camStopDistance;
    private GameManager gm;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void OnEnable()
    {
        StartCoroutine(EnableAfter());
    }
    private void OnDisable()
    {
        GameManager.instance.NewLeftBound -= NewLeftBound;
    }
    private void FixedUpdate()
    {
        //Debug.Log(_camLeftBound);
        if ( _player.position.x < _camLeftBound)
        {
            transform.position = Mathf.MoveTowards(transform.position.x, _camLeftBound, _camMoveSpeed * Time.deltaTime) * Vector3.right + new Vector3(0, transform.position.y, -10);
        }
        else
        {
            transform.position = Mathf.MoveTowards(transform.position.x, _player.position.x, _camMoveSpeed * Time.deltaTime) * Vector3.right + new Vector3(0, transform.position.y, -10);
        }
    }


    private void NewLeftBound(float NewBound)
    {
        _camLeftBound = NewBound + _camStopDistance;
    }
    private IEnumerator EnableAfter()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.NewLeftBound += NewLeftBound;
    }
}
