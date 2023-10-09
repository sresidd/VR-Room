using UnityEngine;
using UnityEngine.InputSystem;
using UnitySimpleLiquid;

public class LiquidBehavior : MonoBehaviour
{
    [SerializeField] private LiquidContainer liquidContainer;
    [SerializeField] private InputActionProperty openCap;
    AudioManager audioManager;

    private bool hasAudioPlayed = false;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        openCap.action.started += ContextMenu => liquidContainer.IsOpen = true;
        openCap.action.canceled += ContextMenu => liquidContainer.IsOpen = false;
    }

}
