using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;
    public static AudioManager instance;
    private void Awake(){
        instance = this;
    }

    public void PlayAudio(int index){
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }

    public void PlayAudioOneShot(int index){
        audioSource.PlayOneShot(audioClips[index]);
    }

    public bool IsAudioPlaying(){
        return audioSource.isPlaying;
    }

    public float ReturnAudioClipLength(){
        return audioSource.clip.length;
    }
}
