using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    private int wearableIndex = 0;

    void Start(){

        if(audioManager == null) audioManager = FindObjectOfType<AudioManager>();

        GameEvents.instance.OnAcidBasePour += AcidBasePour;
        GameEvents.instance.OnColorChange += ColorChange;
        GameEvents.instance.OnWearable += Wearable;
    }

    private void ColorChange(bool acidColor, bool baseColor)
    {
        if(!acidColor) return;
        if(!baseColor) return;
        Debug.Log("Both Color Changed!");
        audioManager.PlayAudio(9);
    }

    private void AcidBasePour(bool acidPour, bool basePour)
    {
        if(!acidPour) return;
        if(!basePour) return;
        Debug.Log("Both Liquid Poured!");
        audioManager.PlayAudio(10);
    }

    private void Wearable(){
        wearableIndex++;
        if(wearableIndex != 2) return;
        StartCoroutine(FirstAudio(6));
    }

    private IEnumerator FirstAudio(int audioIndex){
        if(!audioManager.IsAudioPlaying()){
            audioManager.PlayAudio(audioIndex);
        }
        else{
            yield return new WaitForSeconds(audioManager.ReturnAudioClipLength());
            audioManager.PlayAudio(audioIndex);
        }

        StartCoroutine(SecondAudio(7));
    }
    private IEnumerator SecondAudio(int audioIndex){
        if(!audioManager.IsAudioPlaying()){
            audioManager.PlayAudio(audioIndex);
        }
        else{
            yield return new WaitForSeconds(audioManager.ReturnAudioClipLength());
            audioManager.PlayAudio(audioIndex);
        }
    }
}
