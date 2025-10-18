using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class RotateToCamera : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (Camera.main != null)
            {
                Quaternion cameraRotation = Camera.main.transform.rotation;
                //modifica la rotazione lungo y per ruotare tutto verso la camera
                var rotation = transform.rotation.eulerAngles;
                rotation.y = cameraRotation.eulerAngles.y;
                transform.rotation = Quaternion.Euler(rotation);
            }
        }
    }
}
