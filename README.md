# Readable Note System in Unity
This project is a Unity-based application that demonstrates note interactions in a first-person perspective (FPP) environment. It includes functionality for displaying and closing notes, controlling character movement, and managing camera rotation.

## Components

### NoteSO
`NoteSO` is a ScriptableObject used to store note content. It provides methods to get and set the note content.

```csharp
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
```

### NoteComponent
`NoteComponent` is a MonoBehaviour that connects the note to a GameObject. It retrieves the content from the associated `NoteSO`.

```csharp
using UnityEngine;

public class NoteComponent : MonoBehaviour
{
    public NoteSO noteSO;

    public string GetNoteContent()
    {
        return noteSO != null ? noteSO.GetNoteContent() : string.Empty;
    }
}
```

### NoteUIController
`NoteUIController` handles the display and closure of notes. It uses TMP_Text to show note content and controls the visibility of the note UI.
```csharp
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
```
### CharacterController
`CharacterController` manages the character movement and interaction with notes. It uses input from the keyboard and mouse to move and interact within the scene.
```csharp
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Vector3 input;
    private Rigidbody rb;
    private float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");

        // Note interaction
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out NoteComponent noteComponent))
                {
                    NoteUIController.instance.DisplayNote(noteComponent.GetNoteContent());
                    Debug.Log(hit.collider.name);
                }
            }
        }

        // Close note
        if (Input.GetKeyDown(KeyCode.Escape) && NoteUIController.instance.IsNoteOpen())
        {
            NoteUIController.instance.CloseNote();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.TransformDirection(input) * Time.fixedDeltaTime * speed);
    }
}
```

### FirstPersonCameraRotation
`FirstPersonCameraRotation` manages the camera rotation for a first-person perspective, allowing for vertical and horizontal movement with sensitivity and limits to prevent flipping.
```csharp
using UnityEngine;

/// <summary>
/// A simple FPP (First Person Perspective) camera rotation script.
/// Like those found in most FPS (First Person Shooter) games.
/// </summary>
public class FirstPersonCameraRotation : MonoBehaviour
{
    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }

    [Range(0.1f, 9f)][SerializeField] private float sensitivity = 2f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)][SerializeField] private float yRotationLimit = 88f;

    private Vector2 rotation = Vector2.zero;
    private const string xAxis = "Mouse X";
    private const string yAxis = "Mouse Y";

    private void Update()
    {
        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
    }
}
```
