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

    public void KeyButton(string key)
    {
        if (characterInputCount < keyInputField.characterLimit)
        {
            keyInputField.text += key;
            characterInputCount++;

        }
    }

    public void EnterButton()
    {
        if (keyInputField.text == ambulanceNr)
        {
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
