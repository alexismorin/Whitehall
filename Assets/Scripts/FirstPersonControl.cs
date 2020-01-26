using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonControl : MonoBehaviour {

    [SerializeField]
    Transform cameraObject;
    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField]
    float lookSpeed = 1f;

    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    public Vector2 clampInDegrees = new Vector2 (360, 180);
    public bool lockCursor;
    public Vector2 sensitivity = new Vector2 (2, 2);
    public Vector2 smoothing = new Vector2 (3, 3);
    public Vector2 targetDirection;
    public Vector2 targetCharacterDirection;

    public GameObject characterBody;

    public Animator rig;

    int i;

    CharacterController characterController;

    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = characterBody.GetComponent<CharacterController> ();
    }

    void Update () {
        // Allow the script to clamp based on a desired target value.
        //     var targetOrientation = Quaternion.Euler (targetDirection);
        //    var targetCharacterOrientation = Quaternion.Euler (targetCharacterDirection);

        // Get raw mouse input for a cleaner reading on more sensitive mice.
        var mouseDelta = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

        // Scale input against the sensitivity setting and multiply that against the smoothing value.
        mouseDelta = Vector2.Scale (mouseDelta, new Vector2 (sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // Interpolate mouse movement over time to apply smoothing delta.
        _smoothMouse.x = Mathf.Lerp (_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp (_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Find the absolute mouse movement value from point zero.
        _mouseAbsolute += _smoothMouse;

        // Clamp and apply the local x value first, so as not to be affected by world transforms.
        if (clampInDegrees.x < 360)
            _mouseAbsolute.x = Mathf.Clamp (_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        // Then clamp and apply the global y value.
        if (clampInDegrees.y < 360)
            _mouseAbsolute.y = Mathf.Clamp (_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        transform.localRotation = Quaternion.AngleAxis (-_mouseAbsolute.y, Vector3.right);

        // If there's a character body that acts as a parent to the camera
        if (characterBody) {
            var yRotation = Quaternion.AngleAxis (_mouseAbsolute.x, Vector3.up);
            characterBody.transform.localRotation = yRotation;
        } else {
            var yRotation = Quaternion.AngleAxis (_mouseAbsolute.x, transform.InverseTransformDirection (Vector3.up));
            transform.localRotation *= yRotation;
        }

        Vector2 moveInput = Vector3.zero;
        moveInput.x = Input.GetAxis ("Horizontal");
        moveInput.y = Input.GetAxis ("Vertical");

        rig.SetFloat ("x", moveInput.x);
        rig.SetFloat ("y", moveInput.y);

        Vector3 relativeVelocity = transform.InverseTransformDirection (characterController.velocity);
        print (relativeVelocity);
        rig.SetFloat ("velX", relativeVelocity.x / 5f);
        rig.SetFloat ("velY", relativeVelocity.z / 5f);

        Vector3 moveVector = moveInput.x * transform.right + moveInput.y * transform.forward;

        characterController.SimpleMove (moveVector * moveSpeed);

        if (Input.GetKeyDown (KeyCode.Space)) {
            i++;
            ScreenCapture.CaptureScreenshot (Application.dataPath + "/../Image" + i.ToString () + ".png");
        }

    }
}