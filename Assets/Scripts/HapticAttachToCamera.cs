using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Microsoft.MixedReality.Toolkit.Input;

public class HapticAttachToCamera : MonoBehaviour
{
    public HapticPlugin hapticDevice;
    public HapticGrabber hapticGrabber;
    public GameObject stylusOffsetRoot;
    public Vector3 stylusOffsetedPosition;

    public bool smoothFollow;

    private Vector3 newRotationEuler, velocity;

    // Start is called before the first frame update
    void Start()
    {
        stylusOffsetRoot.transform.localPosition = stylusOffsetedPosition;
        velocity = Vector3.zero;
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {

        if (hapticDevice.Buttons[0] == 1 && hapticDevice.Buttons[1] == 1)
            alignGazeWithStylus();

        if (smoothFollow && hapticDevice.Buttons[0] == 0 && hapticDevice.touching == null && hapticGrabber.touching == null)
            UpdatePosition();
    }

    void UpdatePosition()
    {
        this.gameObject.transform.position = Vector3.SmoothDamp(this.gameObject.transform.position, stylusOffsetRoot.transform.position, ref velocity, 0.75f);
    }

    void alignGazeWithStylus()
    {
        newRotationEuler = new Vector3(this.gameObject.transform.eulerAngles.x, stylusOffsetRoot.transform.eulerAngles.y, this.gameObject.transform.eulerAngles.z);
        this.transform.eulerAngles = newRotationEuler;
    }

}
