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

    //KREISLAUF
    public bool kollapsScene = false;
    public int kollapsCounter = 0;
    public bool nearbyGuy = false;

    public GameObject objectToSetActive;


    //KREISLAUF ENDE

    //SCHLAGANFALL
    public bool nearbyEmily = false;
    public bool juergenHasBeenGreeted = false;
    public bool sendAmbulance = false;
    public bool emilyStandsUp = false;
    public bool emilyExtendsArms = false;

    public bool emilyOneLeg = false;

    public int fastMethodSteps = 0;

    //SCHLAGANFALL ENDE 

    private void Start()
    {
        //KREISLAUF:
        actions.Add("Hallo", ansprechenK);
        //actions.Add("Alles okay", ansprechenK);
        actions.Add("Geht es ihnen gut", ansprechenK);
        actions.Add("Wie geht es ihnen", ansprechenK);
        actions.Add("Alles ok", ansprechenK);
        actions.Add("Alles ok bei ihnen", ansprechenK);
        actions.Add("Sind Sie in Ordnung", ansprechenK);
        actions.Add("Entschuldigung", ansprechenK);

        //actions.Add("Wie geht es ihnen", fragenK);
        actions.Add("Wie geht es dir", fragenK);
        //actions.Add("Geht es ihnen gut", fragenK);
        actions.Add("Geht es dir gut", fragenK);
        actions.Add("Geht es ihnen besser", fragenK);
        actions.Add("Geht es dir besser", fragenK);

        actions.Add("Supermarkt", ambulanceWhereK);
        actions.Add("Vor dem Supermarkt", ambulanceWhereK);
        actions.Add("vor dem großen Supermarkt", ambulanceWhereK);
        actions.Add("großen Supermarkt", ambulanceWhereK);
        actions.Add("großer Supermarkt", ambulanceWhereK);
        actions.Add("auf einer Bank", ambulanceWhereK);
        actions.Add("Bank vor einem Supermarkt", ambulanceWhereK);
        actions.Add("Bank vor Supermarkt", ambulanceWhereK);
        actions.Add("neben einem Parkplatz", ambulanceWhereK);
        actions.Add("Parkplatz vor dem Supermarkt", ambulanceWhereK);
        actions.Add("Parkplatz vor einem Supermarkt", ambulanceWhereK);
        actions.Add("Parkplatz beim Supermarkt", ambulanceWhereK);
        actions.Add("Parkplatz", ambulanceWhereK);

        actions.Add("Jemand hat einen Hitzeschlag", ambulanceWhatK);
        actions.Add("Jemand hat einen Hitzschlag", ambulanceWhatK);
        actions.Add("Ein Mann hat einen Hitzschlag", ambulanceWhatK);
        actions.Add("Eine Person hat einen Hitzschlag", ambulanceWhatK);
        actions.Add("Jemand ist überhitzt", ambulanceWhatK);
        actions.Add("Ein Mann ist fast bewusstlos", ambulanceWhatK);
        actions.Add("Jemand ist benommen", ambulanceWhatK);
        actions.Add("Ein Mann ist benommen", ambulanceWhatK);
        actions.Add("Eine Person ist benommen", ambulanceWhatK);
        actions.Add("Hitzeschlag", ambulanceWhatK);
        actions.Add("Hitzschlag", ambulanceWhatK);
        actions.Add("Kreislaufkollaps", ambulanceWhatK);
        actions.Add("Eine Person hat einen Kreislaufkollaps", ambulanceWhatK);
        actions.Add("Jemand hat einen Kreislaufkollaps", ambulanceWhatK);
        actions.Add("Ein Mann hat einen Kreislaufkollaps", ambulanceWhatK);
        actions.Add("Eine Person hat einen Kreislaufzusammenbruch", ambulanceWhatK);
        actions.Add("Jemand hat einen Kreislaufzusammenbruch", ambulanceWhatK);
        actions.Add("Ein Mann hat einen Kreislaufzusammenbruch", ambulanceWhatK);
        actions.Add("Kreislauf zusammengebrochen", ambulanceWhatK);
        actions.Add("Sein Kreislauf ist zusammengebrochen", ambulanceWhatK);

        actions.Add("Nur eine", ambulanceHowManyK);
        actions.Add("Nur einer", ambulanceHowManyK);
        actions.Add("Nur ein Mann", ambulanceHowManyK);
        actions.Add("Eine einzige", ambulanceHowManyK);
        actions.Add("Nur eine einzige", ambulanceHowManyK);
        actions.Add("Einer", ambulanceHowManyK);
        actions.Add("Eine", ambulanceHowManyK);
        actions.Add("Ein einziger", ambulanceHowManyK);
        actions.Add("Eine Einzelne", ambulanceHowManyK);
        actions.Add("Ein Einzelner", ambulanceHowManyK);
        actions.Add("Nur ein einziger", ambulanceHowManyK);
        actions.Add("Nur eine Einzelne", ambulanceHowManyK);
        actions.Add("Nur ein Einzelner", ambulanceHowManyK);
        actions.Add("Eine Person", ambulanceHowManyK);
        actions.Add("Nur eine Person", ambulanceHowManyK);

        actions.Add("Ja gerade so", ambulanceBewusstseinK);
        actions.Add("Ja", ambulanceBewusstseinK);
        actions.Add("So halb", ambulanceBewusstseinK);
        actions.Add("Ich glaube schon", ambulanceBewusstseinK);
        actions.Add("Ich denke schon", ambulanceBewusstseinK);
        actions.Add("Ja Ich glaube schon", ambulanceBewusstseinK);
        actions.Add("Ja Ich denke schon", ambulanceBewusstseinK);
        actions.Add("Ja er atmet", ambulanceBewusstseinK);



        actions.Add("Interrupt", interrupt);
        actions.Add("Stop", interrupt);
        actions.Add("Halts Maul", interrupt);







        //KREISLAUF ENDE




        // SCHLAGANFALL:
        actions.Add("Hallo Jürgen", hallojuergen);

        actions.Add("Alles okay", emily_allesokay);
        // Ist alles Okay?
        actions.Add("dein Name", emily_deinname);       // Wie ist dein Name? Oder: Wie heißt du? wollen wir mehr optionen haben?
        actions.Add("ihr Name", emily_deinname);
        actions.Add("wie heißt du", emily_deinname);

        actions.Add("aufstehen", emily_aufstehen);      //Kannst du aufstehen?
        actions.Add("Kannst du aufstehen", emily_aufstehen);
        actions.Add("Kannst du lächeln", emily_aufstehen);
        actions.Add("Kannst du aufstehen und lächeln", emily_aufstehen);

        actions.Add("Arme ausstrecken", emily_armeausstrecken); //Kannst du deine Arme ausstrecken?
        actions.Add("Kannst du deine Arme ausstrecken", emily_armeausstrecken); //Kannst du deine Arme ausstrecken?
        actions.Add("Arme", emily_armeausstrecken); //Kannst du deine Arme ausstrecken?

        actions.Add("linkes Bein", emily_linkesbein);   // Kannst du dich auf dein linkes Bein stellen?
        actions.Add("Kannst du dich auf dein linkes Bein stellen", emily_linkesbein);
        actions.Add("Kannst du dich auf ein Bein stellen", emily_linkesbein);

        actions.Add("sonniger Tag", emily_nachsprechen);
        actions.Add("Heute ist Mittwoch, es soll ein sonniger Tag werden", emily_nachsprechen);

        actions.Add("Bushaltestelle", krankenwagen_wo); //ungenau?
        actions.Add("Park", krankenwagen_wo);

        actions.Add("eine Frau", krankenwagen_wer);
        actions.Add("emily", krankenwagen_wer); // Eine Frau mit dem Namen Emily?

        actions.Add("Schlaganfall", krankenwagen_was); //Sie hat vermutlich einen Schlaganfall

        actions.Add("gelehmt", krankenwagen_symptomGelaehmt);

        actions.Add("balance", krankenwagen_symptomeBalance);
        actions.Add("ballons", krankenwagen_symptomeBalance);

        actions.Add("Sprachschwierigkeiten", krankenwagen_symptomeSprachschwierigkeiten); //Anders?
        actions.Add("Probleme beim Sprechen", krankenwagen_symptomeSprachschwierigkeiten);

        actions.Add("Jaa", krankenwagen_bestaetigung);
        actions.Add("verstanden", krankenwagen_bestaetigung);
        actions.Add("Habe ich", krankenwagen_bestaetigung);
        actions.Add("Ich habe verstanden", krankenwagen_bestaetigung);

        //SCHLAGANFALL ENDE

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

    //KREISLAUF:

    public void changeNearbyGuy()
    {
        nearbyGuy = true;
    }

    public void setKollapsSceneTrue()
    {
        if (kollapsScene == false)
        {
            kollapsScene = true;
        }
    }

    private void ansprechenK()
    {
        if (testForDialogueManager() && kollapsScene && nearbyGuy && kollapsCounter == 0)
        {
            dialogueManager.QueueAndPlayDialogueById(7);
            dialogueManager.QueueAndPlayDialogueById(8);
            dialogueManager.QueueAndPlayDialogueById(9);
            dialogueManager.QueueAndPlayDialogueById(10);

            objectToSetActive.SetActive(true);
            Debug.Log("setback drop zone active");
            kollapsCounter++;

        }
    }

    private void fragenK()
    {
        if (testForDialogueManager() && kollapsScene && kollapsCounter == 1)
        {
            dialogueManager.QueueAndPlayDialogueById(16);
            dialogueManager.QueueAndPlayDialogueById(17);
            kollapsCounter++;

        }
    }

    private void ambulanceWhereK()
    {
        if (testForDialogueManager() && kollapsScene && kollapsCounter == 2)
        {
            dialogueManager.QueueAndPlayDialogueById(19);
            kollapsCounter++;

        }
    }

    private void ambulanceWhatK()
    {
        if (testForDialogueManager() && kollapsScene && kollapsCounter == 3)
        {
            dialogueManager.QueueAndPlayDialogueById(20);
            kollapsCounter++;

        }
    }

    private void ambulanceHowManyK()
    {
        if (testForDialogueManager() && kollapsScene && kollapsCounter == 4)
        {
            dialogueManager.QueueAndPlayDialogueById(21);
            kollapsCounter++;

        }
    }

    private void ambulanceBewusstseinK()
    {
        if (testForDialogueManager() && kollapsScene && kollapsCounter == 5)
        {
            dialogueManager.QueueAndPlayDialogueById(22);
            dialogueManager.QueueAndPlayDialogueById(23);
            dialogueManager.QueueAndPlayDialogueById(24);
            dialogueManager.QueueAndPlayDialogueById(25);

        }
    }

    //KREISLAUF ENDE





    //SCHLAGANFALL:
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
            dialogueManager.InterruptAndPlayDialogueById(0);    //Unterbricht aktuelle und darauffolgende Audios + Untertitel und spielt nur diesen aus
        }
    }

    private void hallojuergen()
    {
        juergenHasBeenGreeted = true;       //sets transition variable to true
                                            //sitting animation transitions to waving animation
        Debug.Log("Jürgen heard you");

    }

    public void changeNearbyEmily()
    {

        nearbyEmily = true;
    }

    private void emily_allesokay()
    {
        if (nearbyEmily)
        {
            if (fastMethodSteps == 0)
            {
                fastMethodSteps = 1;

                Debug.Log("asked emily if she's okay");

                dialogueManager.QueueAndPlayDialogueById(8); //Emily says shes okay

                dialogueManager.QueueAndPlayDialogueById(9); //Narrator prompts you to ask for her name
            }
        }
    }

    private void emily_deinname()
    {
        if (fastMethodSteps == 1)
        {

            Debug.Log("asked for emilys name");

            fastMethodSteps = 2;

            dialogueManager.QueueAndPlayDialogueById(10); //Emily replies with her name

            dialogueManager.QueueAndPlayDialogueById(11); // Narrator prompts you to tell emily to stand up
        }
    }
    private void emily_aufstehen()
    {
        if (fastMethodSteps == 2)
        {

            Debug.Log("asked emily to stand up (and smile)");
            //she smiles a (only one side of her face moves up)

            emilyStandsUp = true;

            fastMethodSteps = 3;



            dialogueManager.QueueAndPlayDialogueById(12); // Narrator explains that Emily seems unwell

            dialogueManager.QueueAndPlayDialogueById(13); //N prompts you to ask emily to extend her arms
        }




    }

    private void emily_armeausstrecken()
    {
        if (fastMethodSteps == 3)
        {

            Debug.Log("asked emily to extend her arms");
            //emily standing animation transitions to her raising her arms, one more than the other

            emilyExtendsArms = true;

            fastMethodSteps = 4;

            dialogueManager.QueueAndPlayDialogueById(14);

        }
    }

    private void emily_linkesbein()
    {
        if (fastMethodSteps == 4)
        {

            Debug.Log("asked emily to stand on her left leg and bend the other one");
            //emily is struggling to follow your instructions, not able to raise right leg, losing balance
            //how to animate?

            emilyOneLeg = true;


            fastMethodSteps = 5;

            dialogueManager.QueueAndPlayDialogueById(15); //N prompts you to ask emily to speak after you
        }


    }

    private void emily_nachsprechen()
    {


        if (fastMethodSteps == 5)
        {

            Debug.Log("asked emily to speak after you");

            fastMethodSteps = 6;

            dialogueManager.QueueAndPlayDialogueById(16); //Emily speaks after you, slow and broken sentences

            dialogueManager.QueueAndPlayDialogueById(17); //N explains that EMily might suffer from a stroke.
                                                          // Prompts you to pick up her phone

            dialogueManager.QueueAndPlayDialogueById(18); // prompt to call 911 - implement somewhere with phone maybe..

        }


    }

    private void krankenwagen_wo()
    {
        //Before: 112 has been called, player has been asked where they are

        if (fastMethodSteps == 6)
        {

            Debug.Log("told paramedics location");

            fastMethodSteps = 7;

            dialogueManager.QueueAndPlayDialogueById(20); //Paramedics ask whos hurt


        }



    }

    private void krankenwagen_wer()
    {

        if (fastMethodSteps == 7)
        {

            Debug.Log("told paramedics whos hurt");

            fastMethodSteps = 8;

            dialogueManager.QueueAndPlayDialogueById(21); //paramedics ask what happened
        }



    }

    private void krankenwagen_was()
    {

        if (fastMethodSteps == 8)
        {

            Debug.Log("told paramedics what happened");

            fastMethodSteps = 9;

            dialogueManager.QueueAndPlayDialogueById(23);

        }



    }

    private void krankenwagen_symptomeBalance()
    {

        if (fastMethodSteps > 8 && fastMethodSteps < 12) //9
        {

            Debug.Log("told paramedics about balance problems");

            fastMethodSteps += 1; // 10 or 11 or 12

            if (fastMethodSteps == 12)
            {
                dialogueManager.QueueAndPlayDialogueById(22); //paramedics ask you confirm you understood
            }

        }



    }

    private void krankenwagen_symptomGelaehmt()
    {

        if (fastMethodSteps > 8 && fastMethodSteps < 12)
        { //10

            Debug.Log("told paramedics about partial paralysis");

            fastMethodSteps += 1; // 10 or 11 or 12

            if (fastMethodSteps == 12)
            {
                dialogueManager.QueueAndPlayDialogueById(22); //paramedics ask you confirm you understood
            }

        }



    }

    private void krankenwagen_symptomeSprachschwierigkeiten()
    {

        if (fastMethodSteps > 8 && fastMethodSteps < 12)
        { //11

            Debug.Log("told paramedics about pproblems with speaking");
            ;

            fastMethodSteps += 1; // 10 or 11 or 12

            if (fastMethodSteps == 12)
            {
                dialogueManager.QueueAndPlayDialogueById(22); //paramedics ask you confirm you understood
            }
        }



    }



    private void krankenwagen_bestaetigung()
    {
        if (fastMethodSteps == 12)
        {

            Debug.Log("confirmed that you understood paramedics");

            sendAmbulance = true;

            dialogueManager.QueueAndPlayDialogueById(24);
            dialogueManager.QueueAndPlayDialogueById(25);

        }

    }


    //SCHLAGANFALL ENDE

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
