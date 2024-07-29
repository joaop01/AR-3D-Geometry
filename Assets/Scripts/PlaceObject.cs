using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObject : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    private GameObject spawnedObject;

    private ARRaycastManager aRRaycastManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private InputAction m_pressAction;

    bool isPressed;

    private void Awake()
    {
        m_pressAction = new InputAction("touch", binding: "<Pointer>/press");

        m_pressAction.started += ctx =>
        {
            if (ctx.control.device is Pointer device) { device.position.ReadValue(); }
        };

        m_pressAction.performed += ctx =>
        {
            if (ctx.control.device is Pointer device)
            { 
                device.position.ReadValue();
                isPressed = true;
            }
        };

        m_pressAction.canceled += _ => isPressed = false;

        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void OnEnable() { m_pressAction.Enable(); }

    private void OnDisable() { m_pressAction.Disable(); }

    private void OnDestroy() { m_pressAction.Dispose(); }

    void Update()
    {
        if (Pointer.current == null || !isPressed) return;

        if (aRRaycastManager.Raycast(Pointer.current.position.ReadValue(), hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;

            if (spawnedObject == null)
            {
                pose.position.y += 0.1f;
                spawnedObject = Instantiate(prefab, pose.position, pose.rotation);
            }

            else
            {
                pose.position.y += 0.1f;
                spawnedObject.transform.position = pose.position;
                spawnedObject.transform.rotation = pose.rotation;
            }
        }
    }
}
