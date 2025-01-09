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


    public AnimationStateController animationStateController;
    public bool juergenHasBeenGreeted = false;
    public bool sendAmbulance = false;

    public bool emilyStandsUp = false;

    public int fastMethodSteps = 0;

    private void Start()
    {
        actions.Add("Hallo", hello);
        actions.Add("Coding", coding);
        actions.Add("Stop", interrupt);
        actions.Add("Hallo Jürgen", hallojuergen);
        actions.Add("Alles okay", emily_allesokay);     // Ist alles Okay?
        actions.Add("dein Name", emily_deinname);       // Wie ist dein Name? Oder: Wie heißt du? wollen wir mehr optionen haben?
        //actions.Add("ihr Name", emily_ihrname);
        actions.Add("aufstehen", emily_aufstehen);      //Kannst du aufstehen?
        actions.Add("Arme ausstrecken", emily_armeausstrecken); //Kannst du deine Arme ausstrecken?
        actions.Add("linkes Bein", emily_linkesbein);   // Kannst du dich auf dein linkes Bein stellen?
        actions.Add("Heute ist Mittwoch, es soll ein sonniger Tag werden", emily_nachsprechen); //geht das?
        actions.Add("Bushaltestelle", krankenwagen_wo); //Wir sind an einer Bushaltestelle neben einem Park -- genauer?
        actions.Add("eine Frau", krankenwagen_wer); // Eine Frau mit dem Namen Emily?
        actions.Add("Schlaganfall", krankenwagen_was); //Sie hat vermutlich einen Schlaganfall
        actions.Add("gelehmt", krankenwagen_symptomGelaehmt);
        actions.Add("balance", krankenwagen_symptomeBalance);
        actions.Add("Sprachschwierigkeiten", krankenwagen_symptomeSprachschwierigkeiten); //Anders?
        actions.Add("Ja", krankenwagen_bestaetigung);

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
            dialogueManager.QueueAndPlayDialogueById(0);    //L�sst vorherige Audios und Untertitel auslaufen
        }

    }

    private void coding()
    {
        if (testForDialogueManager())
        {
            dialogueManager.QueueAndPlayDialogueById(1);    //L�sst vorherige Audios und Untertitel auslaufen
        }
    }

    private void interrupt()
    {
        if (testForDialogueManager())
        {
            dialogueManager.InterruptAndPlayDialogueById(2);    //Unterbricht aktuelle und darauffolgende Audios + Untertitel und spielt nur diesen aus
        }
    }

    private void hallojuergen()
    {
        juergenHasBeenGreeted = true;       //sets transition variable to true
                                            //sitting animation transitions to waving animation
        Debug.Log("Jürgen heard you");

    }

    private void emily_allesokay()
    {
        fastMethodSteps += 1;

        Debug.Log("asked emily if she's okay");
        
        //dialogueManager.QueueAndPlayDialogueById();
    }

    private void emily_deinname()
    {
        if (fastMethodSteps == 1)
        {

            Debug.Log("asked for emilys name");
            //emily should reply with her name, maybe a little slurred

            fastMethodSteps += 1;
        }
    }
    private void emily_aufstehen()
    {
        if (fastMethodSteps == 2)
        {

            Debug.Log("asked emily to stand up and smile");
            //emily sitting animation transitions to her standing up, SLOWLY!!!
            //then, she smiles awkwardly (only one side of her face moves up)

            emilyStandsUp = true;

            fastMethodSteps += 1;
        }
        /*dialogueManager.QueueAndPlayDialogueById(XYZ);
          
        XYZ: "Auch wenn Betroffene oft behaupten es gehe ihnen gut,
        oder es sei nicht so schlimm und ihnen ist nur ein bisschen schwindelig,
        kann das dennoch ein Anzeichen für einen Schlaganfall sein.
        ...
        wenn das durchgelaufen ist, erst DANN:

        dialogueManager.QueueAndPlayDialogueById(XYZ+1)

        XYZ+1: "Fordere Emily auf ihre Arme auszustrecken, die Handflächen dabei nach oben zu halten,
         auf dem linken Bein zu stehen und das rechte etwas anzuwinkeln" (Arms/Arme)"

        funktioniert das so?
        */



    }

    private void emily_armeausstrecken()
    {
        if (fastMethodSteps == 3)
        {

            Debug.Log("asked emily to extend her arms");
            //emily standing animation transitions to her raising her arms, one more than the other
            //emily makes noises of confusion?

            //wie wollen wir das mit den Handflächen nach oben handlen? einfach ein weiterer Schritt? sonst wird der voice command zu lang i think

            fastMethodSteps += 1;
        }
    }

    private void emily_linkesbein()
    {
        if (fastMethodSteps == 4)
        {

            Debug.Log("asked emily to stand on her left leg and bend the other one");
            //emily is struggling to follow your instructions, not able to raise right leg, losing balance
            //how to animate?


            fastMethodSteps += 1;
        }

        //A Done

        /* 
        "Das sieht nicht gut aus. Hast du gesehen, wie schwer ihr das gefallen ist? Sie hat es kaum geschafft das rechte Bein vom Boden zu erheben, geschweige denn die Balance zu halten.

        Auch das spricht leider dafür, dass Emily einen Schlaganfall haben könnte.

        Wende bitte auch den dritten Schritt der Fast Methode an (Speech/Sprache). Lass sie ein oder zwei einfache Sätze sagen und schau, ob sie damit Schwierigkeiten hat, oder nicht:

        Lass sie sagen: "Heute ist Mittwoch, es soll ein sonniger Tag werden"
        */
    }

    private void emily_nachsprechen()
    {


        if (fastMethodSteps == 5)
        {

            Debug.Log("asked emily to speak after you");
            //Emily speaks after you, slow and broken sentences

            fastMethodSteps += 1;
            Debug.Log(fastMethodSteps);
        }

        /*"Das sieht wirklich gar nicht gut aus für Emily.
         Sie hat sichtlich Schwierigkeiten überhaupt mehr als ein oder zwei Worte flüssig zu sprechen und ich bin auch nicht sicher, ob sie dich wirklich verstanden hat.

        Du solltest den Krankenwagen rufen. Nimm dazu Emily's handy, welches neben ihr auf dem Boden liegt, und wähle die 112. */

    }

    private void krankenwagen_wo()
    {
        //Before: 112 has been called, player has been asked where they are

        if (fastMethodSteps == 6)
        {

            Debug.Log("told paramedics location");

            fastMethodSteps += 1;
            Debug.Log(fastMethodSteps);
        }

        //After: paramedics ask whos hurt

    }

    private void krankenwagen_wer()
    {

        if (fastMethodSteps == 7)
        {

            Debug.Log("told paramedics whos hurt");

            fastMethodSteps += 1;
            Debug.Log(fastMethodSteps);
        }

        //After: paramedics ask what happened

    }

    private void krankenwagen_was()
    {

        if (fastMethodSteps == 8)
        {

            Debug.Log("told paramedics what happened");

            fastMethodSteps += 1;
            Debug.Log(fastMethodSteps);
        }

        //After: paramedics ask for details: Bist du sicher, was sind symptome?

    }

    private void krankenwagen_symptomeBalance()
    {

        if (fastMethodSteps > 8) //9
        {

            Debug.Log("told paramedics about balance problems");

            fastMethodSteps += 1; // 10 or 11 or 12
            Debug.Log(fastMethodSteps);
        }

        //After: paramedics ask for 3 details in total

    }

    private void krankenwagen_symptomGelaehmt()
    {

        if (fastMethodSteps > 8)
        { //10

            Debug.Log("told paramedics about partial paralysis");

            fastMethodSteps += 1; // 10 or 11 or 12
            Debug.Log(fastMethodSteps);
        }

        //After: paramedics ask for 3 details in total

    }

    private void krankenwagen_symptomeSprachschwierigkeiten()
    {

        if (fastMethodSteps > 8)
        { //11

            Debug.Log("told paramedics about pproblems with speaking");
            ;

            fastMethodSteps += 1; // 10 or 11 or 12
            Debug.Log(fastMethodSteps);
        }

        //After: paramedics tell you to wait and to distract emily etc. then they ask if you understood. say yes

    }



    private void krankenwagen_bestaetigung()
    {
        if (fastMethodSteps == 12)
        {

            Debug.Log("confirmed that you understood paramedics");

            sendAmbulance = true;

        }
        //After: paramedics tell you they are on their way and hang up.
    }


    //Still missing: Steps after calling the ambulance

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
