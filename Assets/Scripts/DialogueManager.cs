using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private string[] tutorialDialogues;
    [SerializeField] private GameObject dialogueImage;
    [SerializeField] private TMP_Text dialogue;
    [SerializeField] private float typingSpeed = 0.01f;
    private int index = 0;
    [SerializeField] private Button nextButton;

    AudioManager Audio;

    private void Start(){
        Audio = FindObjectOfType<AudioManager>();
        Audio.PlayAudio(index);
        StartCoroutine(Types());
    }
    IEnumerator Types()
    {
        foreach(char letter in tutorialDialogues[index].ToCharArray()){
            dialogue.text += letter;
            if (dialogue.text == tutorialDialogues[index])
            {
                nextButton.interactable = true;
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {

        nextButton.interactable = false;

        if(index < tutorialDialogues.Length - 1){
            index++;
            Audio.PlayAudio(index);

            dialogue.text = "";
            StartCoroutine(Types());
        }
        else
        {
            dialogue.text = "";
            nextButton.interactable = false;
            dialogueImage.SetActive(false);
        }
    }
}
