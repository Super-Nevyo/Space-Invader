using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public System.Action<float> MoveEvent;
    public System.Action JumpEvent;
    public System.Action ShootEvent;
    public System.Action AnyInputEvent;
    public System.Action QuitEvent;

    public void OnJump()
    {
        JumpEvent?.Invoke();
    }
    public void OnShoot()
    {
        ShootEvent?.Invoke();
    }
    public void OnMove(InputValue value)
    {
        MoveEvent?.Invoke(value.Get<float>());
    }
    public void OnAnyButton(InputValue value)
    {
        AnyInputEvent?.Invoke();
    }
    public void OnQuitGame(InputValue value)
    {
        QuitEvent?.Invoke();
        Application.Quit();
    }
    

}
