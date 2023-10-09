using UnityEngine;
using System.Collections.Generic;

public class Wearable : MonoBehaviour
{
    [SerializeField] private Material OriginalItemMaterial;
    [SerializeField] private Material HoverItemMaterial;
    [SerializeField] protected List<MeshRenderer> items;
    private AudioManager audioManager;
    protected bool hasWorn = false;

    public bool ReturnWearableState() => hasWorn;

    private int wearableIndex = 0;


    void Start(){
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void CheckWearableIndex()
    {
        wearableIndex++;
        if (wearableIndex != 2) return;
        Invoke(nameof(PlayAud),2f);
    }

    private void PlayAud()
    {
        audioManager.PlayAudio(6);

        Invoke(nameof(PlayNextAudio), 7f);
    }

    private void PlayNextAudio(){
        audioManager.PlayAudio(7);
    }

    public void OnHoverEnter(){
        if(hasWorn) return;
        foreach(MeshRenderer item in items) item.material = HoverItemMaterial;
    }

    public void OnHoverExit(){
        if(hasWorn) return;
        foreach(MeshRenderer item in items) item.material = OriginalItemMaterial;
    }

    public void WearItem(){
        hasWorn = true;
        foreach(MeshRenderer item in items) item.material = OriginalItemMaterial;
        GameEvents.instance.Wearable();
    }
}
