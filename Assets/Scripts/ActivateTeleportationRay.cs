using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ActivateTeleportationRay : MonoBehaviour
{
    [SerializeField] private GameObject rightRay;
    // [SerializeField] private GameObject leftRay;

    [SerializeField] private InputActionProperty rightActivate;
    // [SerializeField] private InputActionProperty rightActivate;

    void Start()
    {
        rightRay.SetActive(false);
        rightActivate.action.started += ContextMenu => EnableRightRay(true);
        rightActivate.action.canceled += ContextMenu => EnableRightRay(false);
    }

    private void EnableRightRay(bool isRayActive) => rightRay.SetActive(isRayActive);
}
