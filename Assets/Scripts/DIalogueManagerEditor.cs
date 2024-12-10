using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueManager))]
public class DialogueManagerEditor : Editor
{
    private SerializedProperty dialoguesProperty;

    private void OnEnable()
    {
        // Get reference to the serialized 'dialogues' property
        dialoguesProperty = serializedObject.FindProperty("dialogues");
    }

    public override void OnInspectorGUI()
    {
        // Update serialized object representation
        serializedObject.Update();

        // Draw default Inspector, but hide the dialogues list
        DrawPropertiesExcluding(serializedObject, "dialogues");

        GUILayout.Space(10);
        GUILayout.Label("Dialogue Management", EditorStyles.boldLabel);

        // Custom rendering of the dialogues list
        RenderDialoguesList();

        // Add button to create a new dialogue
        if (GUILayout.Button("Add New Dialogue"))
        {
            AddNewDialogue();
        }

        // Apply changes back to the serialized object
        serializedObject.ApplyModifiedProperties();
    }

    private void RenderDialoguesList()
    {
        GUILayout.Label("Dialogues", EditorStyles.boldLabel);

        // Render each dialogue in the list
        for (int i = 0; i < dialoguesProperty.arraySize; i++)
        {
            SerializedProperty dialogue = dialoguesProperty.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical(GUI.skin.box);

            // Display ID as read-only
            SerializedProperty idProperty = dialogue.FindPropertyRelative("id");
            EditorGUILayout.LabelField("ID", idProperty.intValue.ToString());

            // Allow editing of the text and audioClip fields
            SerializedProperty textProperty = dialogue.FindPropertyRelative("text");
            SerializedProperty audioClipProperty = dialogue.FindPropertyRelative("audioClip");

            EditorGUILayout.PropertyField(textProperty);
            EditorGUILayout.PropertyField(audioClipProperty);

            // Optional: Add a delete button for each dialogue
            if (GUILayout.Button("Remove Dialogue"))
            {
                dialoguesProperty.DeleteArrayElementAtIndex(i);
                break; // Exit loop after modifying the list to avoid errors
            }

            EditorGUILayout.EndVertical();
        }
    }

    private void AddNewDialogue()
    {
        // Add a new element to the 'dialogues' array
        int newIndex = dialoguesProperty.arraySize;
        dialoguesProperty.InsertArrayElementAtIndex(newIndex);

        // Get the new element
        SerializedProperty newDialogue = dialoguesProperty.GetArrayElementAtIndex(newIndex);

        // Set default values for the new dialogue
        SerializedProperty idProperty = newDialogue.FindPropertyRelative("id");
        SerializedProperty textProperty = newDialogue.FindPropertyRelative("text");
        SerializedProperty audioClipProperty = newDialogue.FindPropertyRelative("audioClip");

        // Determine next available ID
        int nextId = 0;
        for (int i = 0; i < dialoguesProperty.arraySize - 1; i++)
        {
            var dialogue = dialoguesProperty.GetArrayElementAtIndex(i);
            var existingId = dialogue.FindPropertyRelative("id").intValue;
            nextId = Mathf.Max(nextId, existingId + 1);
        }

        idProperty.intValue = nextId; // Assign new ID
        textProperty.stringValue = ""; // Empty text by default
        audioClipProperty.objectReferenceValue = null; // No audio by default
    }
}