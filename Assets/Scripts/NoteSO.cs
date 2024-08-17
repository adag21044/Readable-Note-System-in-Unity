using UnityEngine;

[CreateAssetMenu(fileName = "NewNoteSO", menuName = "Notes/NoteSO")]
public class NoteSO : ScriptableObject
{
    [TextArea] public string NoteContent;

    public string GetNoteContent()
    {
        return NoteContent;
    }

    public void SetNoteContent(string content)
    {
        NoteContent = content;
    }
}
