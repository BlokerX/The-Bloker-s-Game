using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraChangeView : MonoBehaviour
{
    /// <summary>
    /// Camera object.
    /// </summary>
    public Camera CameraObject;

    public RigidbodyFirstPersonController PersonController;

    public Vector3 FirstPersonCameraPosition = new Vector3(0, 0.6f, 0);

    public Vector3 FirstPersonCameraRotation = new Vector3(0, 0, 0);

    public Vector3 SecondPersonCameraPosition = new Vector3(0, 1.6f, -2.5f);

    public Vector3 SecondPersonCameraRotation = new Vector3(30, 0, 0);

    public Vector3 ThirdPersonCameraPosition = new Vector3(0, 1.5f, 3);

    public Vector3 ThirdPersonCameraRotation = new Vector3(15, 180, 0);

    public KeyCode CameraChangeKey = KeyCode.F5;

    public CameraPosition CameraState = CameraPosition.FirstPerson;

    void Update()
    {
        if (Input.GetKeyDown(CameraChangeKey))
        {
            var cameraTransform = CameraObject.GetComponent<Transform>();

            switch (CameraState)
            {
                case CameraPosition.FirstPerson:
                    CameraState = CameraPosition.SecondPerson;
                    cameraTransform.localPosition = SecondPersonCameraPosition;
                    cameraTransform.localEulerAngles = SecondPersonCameraRotation;
                    break;
                case CameraPosition.SecondPerson:
                    CameraState = CameraPosition.ThirdPerson;
                    cameraTransform.localPosition = ThirdPersonCameraPosition;
                    cameraTransform.localEulerAngles = ThirdPersonCameraRotation;
                    break;
                case CameraPosition.ThirdPerson:
                    CameraState = CameraPosition.FirstPerson;
                    cameraTransform.localPosition = FirstPersonCameraPosition;
                    cameraTransform.localEulerAngles = FirstPersonCameraRotation;
                    break;
            }
        }
    }

    public enum CameraPosition
    {
        FirstPerson,
        SecondPerson,
        ThirdPerson
    }
}
