using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoundaries : MonoBehaviour
{
    public GameObject topRightCorner;
    public GameObject bottomLeftCorner;

    public float offset;
    // Start is called before the first frame update
    void Update()
    {
        SetupBoundaries();
    }

    // Update is called once per frame
    void SetupBoundaries(){

        var cam = Camera.main;

        Vector3 point = new Vector3();

        point = Camera.main.ScreenToWorldPoint(new Vector3(cam.pixelWidth + offset, cam.pixelHeight + offset, Camera.main.nearClipPlane));
        topRightCorner.transform.position = point;

        point = Camera.main.ScreenToWorldPoint(new Vector3(0 - offset, 0 - offset, Camera.main.nearClipPlane));
        bottomLeftCorner.transform.position = point;
    }
}
