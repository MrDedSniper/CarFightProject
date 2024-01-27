using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public GameObject atachedVehicle;
    public GameObject CameraFolder;
    public Transform[] camLocations;
    public int locationIndicator = 2;

    //public controller controllerRef;
    
    [Range(0, 1)] public float smoothTime = 0.5f;

    private void Start()
    {
        atachedVehicle = GameObject.FindGameObjectWithTag("PlayerCar");
        CameraFolder = atachedVehicle.transform.Find("Camera").gameObject;
        camLocations = CameraFolder.GetComponentsInChildren<Transform>();

        //controllerRef = atachedBVehicle.GetComponent<controller>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (locationIndicator >= 4 || locationIndicator < 2) locationIndicator = 2;
            else locationIndicator++;
        }

        transform.position = camLocations[locationIndicator].position * (1 - smoothTime) +
                             transform.position * smoothTime;
        transform.LookAt(camLocations[1].transform);

       // smoothTime = (controllerRef.KPH >= 150) ? Mathf.Abs((controllerRef.KPH / 150)) - 0.85f : 0.45f;
    }
}
