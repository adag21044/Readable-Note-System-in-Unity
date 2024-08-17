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

        // Notu tıklama işlemi
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

        // Notu kapatma işlemi
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
