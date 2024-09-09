using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_behav : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    private float min = 55f;
    private float max = 95f;
    private float scrollSpeed = 10f; // Vitesse de changement du FOV

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = max; // Initialiser avec la valeur max
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        cam.fieldOfView -= scroll * scrollSpeed;

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, min, max);
    }
}
