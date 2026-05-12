using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public System.Action<float> MoveEvent;
    public System.Action JumpEvent;
    public System.Action ShootEvent;
    public System.Action AnyInputEvent;

    public void OnJump()
    {
        JumpEvent?.Invoke();
        AnyInputEvent?.Invoke();
        Debug.Log("Jump pressed");
    }
    public void OnShoot()
    {
        ShootEvent?.Invoke();
        AnyInputEvent?.Invoke();
        Debug.Log("Shoot pressed");
    }
    public void OnMove(InputValue value)
    {
        MoveEvent?.Invoke(value.Get<float>());
        AnyInputEvent?.Invoke();
        Debug.Log(value.Get<float>());
    }
}
