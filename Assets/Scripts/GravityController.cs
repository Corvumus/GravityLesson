using UnityEngine;
using UnityEngine.InputSystem;

public class GravityController : MonoBehaviour
{
    [SerializeField] private InputActionReference directionAction;
    [SerializeField] private InputActionReference forceAction;

    private float gravity = 9.81f;

    private void OnEnable()
    {
        directionAction.action.Enable();
        forceAction.action.Enable();

        directionAction.action.performed += OnDirection;
        forceAction.action.performed += OnForce;
    }

    private void OnDisable()
    {
        directionAction.action.performed -= OnDirection;
        forceAction.action.performed -= OnForce;

        directionAction.action.Disable();
        forceAction.action.Disable();
    }

    private void OnDirection(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        if (direction == Vector2.zero) return;

        ChangeGravity(direction.normalized * gravity);
    }

    private void OnForce(InputAction.CallbackContext context)
    {
        float force = context.ReadValue<float>();
        if (Mathf.Approximately(force, 0f)) return;

        gravity *= force > 0 ? 2f : 0.5f;
        ChangeGravity(Physics2D.gravity.normalized * gravity);
    }

    public void ChangeGravity(Vector2 gravity)
    {
        Physics2D.gravity = gravity;
        Debug.Log("Текущая гравитация: "+Physics2D.gravity);
    }
}
