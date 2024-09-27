using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Threading;
using UnityEngine.Events;
using System.Linq;
using UnityEditor.PackageManager;

public class dialogue : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public TextMeshProUGUI NameText;

    public GameObject DialogueBox;

    public Color32 specialCharacterColor;

    [System.Serializable]
    public class DialogueOption
    {
        public dialogueObject dialogueObject;

        public UnityEvent OnDialogueEnd;
    }

    private bool specialColor = false;
    private bool bold = false;
    private bool italic = false;

    private bool canSkip;

    public List<DialogueOption> Dialogue;

    private int index;

    public int dialogueScreenIndex = 0;

    private bool didRevealText;

    private bool isDoingDialogue;





    public void StartDialogue()
    {

        if (isDoingDialogue)
        {
            if (canSkip)
            {
                NextLine();
                return;
            }
            else
            {
                didRevealText = true;
                return;
            }
        }

        NameText.text = string.Empty;
        DialogueText.text = string.Empty;
        index = 0;

        StartCoroutine(EnableCanvas());




        if (dialogueScreenIndex == Dialogue.ToArray().Length)
        {
            dialogueScreenIndex = 0;
        }



        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {

        yield return null;

        canSkip = false;

        NameText.text = Dialogue[dialogueScreenIndex].dialogueObject.dialogue[index].Name;

        specialColor = false;

        foreach (char c in Dialogue[dialogueScreenIndex].dialogueObject.dialogue[index].GetText().ToCharArray())
        {

            if(c.ToString() == "#")
            {
                specialColor = !specialColor;

                if (specialColor)
                {
                    DialogueText.text += $"<color=#{ColorUtility.ToHtmlStringRGB(specialCharacterColor)}>";
                }
                else
                {
                    DialogueText.text += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.white)}>";
                }
            }
            else if (c.ToString() == "$")
            {
                bold = !bold;

                if (bold)
                {
                    DialogueText.text += "<b>";
                }
                else
                {
                    DialogueText.text += "</b>";
                }
            }
            else if (c.ToString() == "_")
            {
                italic = !italic;

                if (italic)
                {
                    DialogueText.text += "<i>";
                }
                else
                {
                    DialogueText.text += "</i>";
                }
            }
            else
            {
                DialogueText.text += c;
            }





            if (!didRevealText)
            {
                yield return new WaitForSeconds(Dialogue[dialogueScreenIndex].dialogueObject.dialogue[index].Delay);
            }


        }

        canSkip = true;
        didRevealText = false;
    }

    public void NextLine()
    {

        if (index < Dialogue[dialogueScreenIndex].dialogueObject.dialogue.ToArray().Length - 1)
        {
            index++;

            DialogueText.text = string.Empty;



            StartCoroutine(TypeLine());


        }
        else
        {
            dialogueEnd();
        }
    }




    void dialogueEnd()
    {
        DialogueBox.SetActive(false);
        isDoingDialogue = false;

        if (Dialogue[dialogueScreenIndex].OnDialogueEnd != null)
        {
            Dialogue[dialogueScreenIndex].OnDialogueEnd.Invoke();
        }

        dialogueScreenIndex += 1;
    }


    IEnumerator EnableCanvas() 
    {     
        yield return new WaitForSeconds(0.1f);

        DialogueBox.SetActive(true);
        isDoingDialogue = true;
    }
}