using UnityEngine;
using TMPro;

public class NoteUIController : MonoBehaviour
{
    private bool isNoteOpen = false;
    [SerializeField] private GameObject paperObject;
    [SerializeField] private TMP_Text noteText;
    public static NoteUIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    public void DisplayNote(string noteStr)
    {
        noteText.text = noteStr;
        paperObject.SetActive(true);
        isNoteOpen = true;
    }

    public void CloseNote()
    {
        isNoteOpen = false;
        paperObject.SetActive(false);
    }

    public bool IsNoteOpen()
    {
        return isNoteOpen;
    }
}
