using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObject : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    public GameObject spawnedObject;

    private ARRaycastManager aRRaycastManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private InputAction m_pressAction;

    public GameObject panel;

    private GameObject buttonCustom;

    private GameObject buttonTransform;

    private GameObject buttonColor;

    private GameObject buttonLine;

    private RectTransform buttonCustomRectTransform;

    private RectTransform buttonTransformRectTransform;

    private RectTransform buttonColorRectTransform;

    private RectTransform buttonLineRectTransform;

    private RectTransform panelRectTransform;

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

        panel = GameObject.Find("Panel");

        panelRectTransform = panel.GetComponent<RectTransform>();

        panel.gameObject.SetActive(false);
        panel.transform.GetChild(0).gameObject.SetActive(false);
        panel.transform.GetChild(1).gameObject.SetActive(false);
        panel.transform.GetChild(2).gameObject.SetActive(false);
        panel.transform.GetChild(3).gameObject.SetActive(false);

        buttonCustom = GameObject.Find("ButtonCustom");

        buttonTransform = GameObject.Find("ButtonTransform");

        buttonColor = GameObject.Find("ButtonColor");

        buttonLine = GameObject.Find("ButtonLine");

        buttonCustomRectTransform = buttonCustom.GetComponent<RectTransform>();

        buttonTransformRectTransform = buttonTransform.GetComponent<RectTransform>();

        buttonColorRectTransform = buttonColor.GetComponent<RectTransform>();

        buttonLineRectTransform = buttonLine.GetComponent<RectTransform>();
    }

    private void OnEnable() { m_pressAction.Enable(); }

    private void OnDisable() { m_pressAction.Disable(); }

    private void OnDestroy() { m_pressAction.Dispose(); }

    void Update()
    {
        if (Pointer.current == null || !isPressed) return;

        if (panel.gameObject.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(panelRectTransform, Pointer.current.position.ReadValue())) return;

        if (buttonCustom.gameObject.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(buttonCustomRectTransform, Pointer.current.position.ReadValue())) return;

        if (buttonTransform.gameObject.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(buttonTransformRectTransform, Pointer.current.position.ReadValue())) return;

        if (buttonColor.gameObject.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(buttonColorRectTransform, Pointer.current.position.ReadValue())) return;

        if (buttonLine.gameObject.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(buttonLineRectTransform, Pointer.current.position.ReadValue())) return;

        if (aRRaycastManager.Raycast(Pointer.current.position.ReadValue(), hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(prefab, pose.position, pose.rotation);
            }

            else
			{
				spawnedObject.transform.position = pose.position;
                spawnedObject.transform.Translate(new Vector3(0, spawnedObject.GetComponent<Renderer>().bounds.min[1]*(-1), 0), Space.World);

				GameObject lineObject = spawnedObject.GetComponent<UIPanelCustomization>().spawnedLine;
				if(lineObject != null)
				{
					lineObject.transform.position = spawnedObject.transform.position;
					lineObject.transform.rotation = spawnedObject.transform.rotation;
				}
            }
        }
    }
}
