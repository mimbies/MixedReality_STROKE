using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneKeyInput : MonoBehaviour
{

    private string ambulanceNr = "112";

    [SerializeField] TMP_InputField keyInputField;

    private int characterInputCount;

    DialogueManager dialogueManager;

    public void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void KeyButton(string key)
    {
        
            keyInputField.text += key;
              Debug.Log(key);
            characterInputCount++;

        
    }

    public void EnterButton()
    {
        Debug.Log("entersenterbutton");
        if (keyInputField.text == ambulanceNr)
        {
            Debug.Log("hi");
            dialogueManager.QueueAndPlayDialogueById(19);

        }

    }

    public void CancelButton()
    {
        ResetInputField();
    }

    void ResetInputField()
    {
        keyInputField.text = null;
        characterInputCount = 0;
    }
}
