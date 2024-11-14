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

    [SerializeField]
    private Transform testCube;

    private void Start()
    {
        actions.Add("vorne", forward);
        actions.Add("oben", up);
        actions.Add("unten", down);
        actions.Add("zurück", back);
        actions.Add("links", left);
        actions.Add("rechts", right);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += speechDistributer;
        keywordRecognizer.Start(); //nur Starten und stoppen bei Gebrauch um Laufzeit zu verbessern
    }

    private void speechDistributer(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void forward()
    {
        testCube.Translate(1, 0, 0);
    }

    private void back()
    {
        testCube.Translate(-1, 0, 0);
    }

    private void up()
    {
        testCube.Translate(0, 1, 0);
    }

    private void down()
    {
        testCube.Translate(0, -1, 0);
    }
    private void left()
    {
        testCube.Translate(0, 0, 1);
    }

    private void right()
    {
        testCube.Translate(0, 0, -1);
    }
}
