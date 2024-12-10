using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;   //Local eingestellte Sprache

public class VoiceRecognition : MonoBehaviour
{

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    DialogueManager dialogueManager;

    private void Start()
    {
        actions.Add("Hallo", hello);
        actions.Add("Coding", coding); 
        actions.Add("Stop", interrupt);



        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += speechDistributer;
        keywordRecognizer.Start(); //nur Starten und stoppen bei Gebrauch um Laufzeit zu verbessern

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void speechDistributer(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }


    private void hello()
    {
        if (testForDialogueManager())
        {
            dialogueManager.QueueAndPlayDialogueById(0);    //Lässt vorherige Audios und Untertitel auslaufen
        }
    }

    private void coding()
    {
        if (testForDialogueManager())
        {
            dialogueManager.QueueAndPlayDialogueById(1);    //Lässt vorherige Audios und Untertitel auslaufen
        }
    }

    private void interrupt()
    {
        if (testForDialogueManager())
        {
            dialogueManager.InterruptAndPlayDialogueById(2);    //Unterbricht aktuelle und darauffolgende Audios + Untertitel und spielt nur diesen aus
        }
    }

    private bool testForDialogueManager()
    {
        if (dialogueManager != null)
        {
            return true;
        }
        else
        {
            Debug.LogWarning("Kein DialogueManager in der Szene gefunden!");
            return false;
        }
    }
}
