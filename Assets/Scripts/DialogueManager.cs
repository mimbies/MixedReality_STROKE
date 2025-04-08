using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    
    [SerializeField] private bool playSomething;       // Untertitel-Textfeld
    int playSomethingi = 3;

    [System.Serializable]
    public class Dialogue
    {
        public int id;                     // Eindeutige ID des Dialogs
        public string text;                // Text f�r die Untertitel
        public AudioClip audioClip;        // Audio-Datei
    }

    [Header("UI Komponenten")]
    [SerializeField] private TextMeshProUGUI canvasSubtitleText;       // Untertitel-Textfeld
    [SerializeField] private GameObject subtitleBackground;       // Untertitel-Hintergrund
    [SerializeField] private TextMeshPro inWorldSubtitleText;       // Untertitel-Textfeld
    [SerializeField] private GameObject subtitleHolder;       // Untertitel-Textfeld
    [SerializeField] private bool useInWorldSubtitles;

    [SerializeField] private AudioSource audioSource; // AudioSource f�r Dialoge

    [Header("Dialog-Daten")]
    [SerializeField] private List<Dialogue> dialogues = new List<Dialogue>(); // Liste aller Dialoge

    private Coroutine currentCoroutine; // Speichert die aktuell laufende Coroutine
    private Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();

    private void Awake()
    {
        // Textfeld zu Beginn deaktivieren
        if(!useInWorldSubtitles) {

            canvasSubtitleText.gameObject.SetActive(false);
            subtitleBackground.SetActive(false);
        }
    }

    private void Update(){
        if(playSomething){
            InterruptAndPlayDialogueById(playSomethingi);
            playSomethingi += 1;
            playSomething = false;
        }
        if(useInWorldSubtitles){
            inWorldSubtitleText.gameObject.transform.LookAt(Camera.main.transform);
            float x = inWorldSubtitleText.gameObject.transform.eulerAngles.x;
            float y = inWorldSubtitleText.gameObject.transform.eulerAngles.y;
            float z = inWorldSubtitleText.gameObject.transform.eulerAngles.z;
            inWorldSubtitleText.gameObject.transform.rotation = Quaternion.Euler(new Vector3(x , y + 180f, z));
            Vector3 currentEuler = subtitleHolder.transform.eulerAngles;
            subtitleHolder.transform.rotation = Quaternion.Euler(0f, currentEuler.y, 0f);
        }
    }

    public void InterruptAndPlayDialogueById(int id)
    {
        // Suche Dialog anhand der ID
        Dialogue dialogue = dialogues.Find(d => d.id == id);
        if (dialogue != null)
        {
            // Aktive Coroutine stoppen, falls vorhanden
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            // Aktives Audio File stoppen, falls vorhanden
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            if (dialogueQueue.Count > 0)
            {
                dialogueQueue.Clear();
            }

            // Neue Coroutine starten und speichern
            currentCoroutine = StartCoroutine(PlayDialogueCoroutine(dialogue));
        }
        else
        {
            Debug.LogWarning($"Dialog mit ID {id} nicht gefunden.");
        }
    }

    public void QueueAndPlayDialogueById(int id)
    {
        // Suche Dialog anhand der ID
        Dialogue dialogue = dialogues.Find(d => d.id == id);
        if (dialogue != null)
        {
            dialogueQueue.Enqueue(dialogue);

            // Start bei leerer Queue
            if (currentCoroutine == null)
            {
                PlayNextInQueue();
            }
        }
        else
        {
            Debug.LogWarning($"Dialogue with ID {id} not found!");
        }
    }

    private void PlayNextInQueue()
    {
        if (dialogueQueue.Count > 0)
        {
            Dialogue nextDialogue = dialogueQueue.Dequeue();
            currentCoroutine = StartCoroutine(PlayDialogueCoroutine(nextDialogue));
        }
    }

    // Coroutine f�r die Anzeige des Textes und das Abspielen des Audios
    private IEnumerator PlayDialogueCoroutine(Dialogue dialogue)
    {
        // Audio starten
        if (dialogue.audioClip != null)
        {
            audioSource.clip = dialogue.audioClip;
            audioSource.Play();
        }

        if(!useInWorldSubtitles) {
            // Text anzeigen
            canvasSubtitleText.SetText(dialogue.text);
            canvasSubtitleText.gameObject.SetActive(true);
            subtitleBackground.SetActive(true);
        } else {
            inWorldSubtitleText.SetText(dialogue.text);
        }

        // Warte auf das Ende des Audios oder setze einen Standard-Wert
        float waitTime = dialogue.audioClip != null ? dialogue.audioClip.length : 6f;
        yield return new WaitForSeconds(waitTime);

        if(!useInWorldSubtitles) {
            // Text ausblenden
            canvasSubtitleText.gameObject.SetActive(false);
            subtitleBackground.SetActive(false);
        }

        // Coroutine abschlie�en
        currentCoroutine = null;

        PlayNextInQueue();
    }
}