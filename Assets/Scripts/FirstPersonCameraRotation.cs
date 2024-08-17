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
