using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Moon_Manager : MonoBehaviour {
  
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    void Start () {
      sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        
        animator.SetBool("moonOpen", true); //animation
      
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
          sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

   IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            for (int i = 0; i < 10; i++)
            // Change "10" to change the speed of the writing
            {
                yield return null;
            }
        }
    }
    void EndDialogue()
    {
        animator.SetBool("moonOpen", false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  
  }
