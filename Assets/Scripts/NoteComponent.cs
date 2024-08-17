using UnityEngine;

public class NoteComponent : MonoBehaviour
{
    public NoteSO noteSO;

    public string GetNoteContent()
    {
        return noteSO != null ? noteSO.GetNoteContent() : string.Empty;
    }
}
