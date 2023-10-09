using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty pinchAnimationAction;
    [SerializeField] private InputActionProperty gripAnimationAction;
    [SerializeField] private Animator animator;
    private float triggerValue;
    private float gripValue;
    private string trigger = "Trigger";
    private string grip = "Grip";

    void Update()
    {
        triggerValue = pinchAnimationAction.action.ReadValue<float>();
        gripValue = gripAnimationAction.action.ReadValue<float>();
        animator.SetFloat(trigger, triggerValue);
        animator.SetFloat(grip, gripValue);
    }
}
