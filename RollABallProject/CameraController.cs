// Created by samurake: All rights reserved!
// Just a script made for Unity3D Main Camera.
// Features: 3-rd person Camera, Collider, Zoom In/Out from Cam Target, free movement by holding right click. 

﻿using UnityEngine;
using System.Collections;

public class CameraControlV2 : MonoBehaviour {
    public Transform lookAt;
    private Transform camTransform;

    private float currentX = 0.0f;
    private float currentY = 10.0f;
    public float sensitivityX = 4.0f;
    public float sensitivityY = 4.0f;

    private float Y_ANGLE_MIN = 0.5f;
    private float Y_ANGLE_MAX = 50.0f;

    public float ZoomMinDistance = 1.2f;
    public float ZoomMaxDistance = 30.0f;
    public float ZoomDistanceSize = 0.7f;
    private float ZoomDistance = 8.0f;

    //AdvancedCollider Variables
    public float ColliderDistances = 4.0f;
    public bool DebugLines = false;

    //code for camera collider
    public LayerMask CamOcclusion;
    private Vector3 camMask;
    public float smooth = 0.5f;

    void Update()
    {
        //Right Click Camera Control
        if (Input.GetMouseButton(1))
        {
            currentX += Input.GetAxis("Mouse X") * sensitivityX;
            currentY += Input.GetAxis("Mouse Y") * sensitivityY;
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }
    }
    void FixedUpdate()
    {
        camTransform = transform;
        Vector3 dir = new Vector3(1, 1, -ZoomDistance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation*dir;
        camMask = camTransform.position;


        camTransform.LookAt(lookAt.position);//apply Right Click Camera Track
        smoothCamMethod();
        CamCollidePreventer(lookAt.position);
        //CamCollidePreventerAdvanced(lookAt.position);//prototype,not done.

        //Camera Zooming Sequence(update on distance above)
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ZoomDistance=ZoomDistance - ZoomDistanceSize;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ZoomDistance = ZoomDistance + ZoomDistanceSize;
        }
        ZoomDistance = Mathf.Clamp(ZoomDistance, ZoomMinDistance, ZoomMaxDistance);
    }
    void smoothCamMethod()
    {
        smooth = 0.2f;
        transform.position = Vector3.Lerp(transform.position, camTransform.position, Time.deltaTime * smooth);
    }

    //Working on a new method to calculate camera position after colliding with objects.
    void CamCollidePreventerAdvanced(Vector3 targetFollow)
    {
        RaycastHit hit;
        Ray RayHitUp = new Ray(targetFollow, Vector3.up);
        Ray RayHitDown = new Ray(targetFollow, Vector3.down);
        Ray RayHitForward = new Ray(targetFollow, Vector3.forward);
        Ray RayHitBackward = new Ray(targetFollow, Vector3.back);
        Ray RayHitLeft = new Ray(targetFollow, Vector3.left);
        Ray RayHitRight = new Ray(targetFollow, Vector3.right);

        //Debug Code
        if(DebugLines == true)
        {
            Debug.DrawRay(camTransform.position, Vector3.up * ColliderDistances);
            Debug.DrawRay(camTransform.position, Vector3.down * ColliderDistances);
            Debug.DrawRay(camTransform.position, Vector3.forward * ColliderDistances);
            Debug.DrawRay(camTransform.position, Vector3.back * ColliderDistances);
            Debug.DrawRay(camTransform.position, Vector3.left * ColliderDistances);
            Debug.DrawRay(camTransform.position, Vector3.right * ColliderDistances);
        }

        if(Physics.Raycast(RayHitUp,out hit, ColliderDistances))
        {
            if(hit.collider.tag != "Pick Up Layer" && DebugLines == false)
            {

            }
            //do nothing
        }
        if(Physics.Raycast(RayHitDown, out hit, ColliderDistances))
        {
            if (hit.collider.tag != "Pick Up Layer" && DebugLines == false)
            {

            }
            //do nothing
        }
        if(Physics.Raycast(RayHitForward, out hit, ColliderDistances))
        {
            if (hit.collider.tag != "Pick Up Layer" && DebugLines == false)
            {

            }
            //do nothing
        }
        if(Physics.Raycast(RayHitBackward, out hit, ColliderDistances))
        {
            if (hit.collider.tag != "Pick Up Layer" && DebugLines == false)
            {

            }
            //do nothing
        }
        if(Physics.Raycast(RayHitLeft, out hit, ColliderDistances))
        {
            if (hit.collider.tag != "Pick Up Layer" && DebugLines == false)
            {

            }
            //do nothing
        }
        if(Physics.Raycast(RayHitRight, out hit, ColliderDistances))
        {
            if (hit.collider.tag != "Pick Up Layer" && DebugLines == false)
            {

            }
            //do nothing
        }
    }

    void CamCollidePreventer(Vector3 targetFollow)
    {
        int PkUpLayer = LayerMask.NameToLayer("Pick Up Layer");
        RaycastHit wallHit = new RaycastHit();
        CamOcclusion.value = ~(1 << PkUpLayer);
        if (Physics.Linecast(targetFollow, camMask, out wallHit, CamOcclusion))
        {
            smooth = 0.2f;
            camTransform.position = new Vector3(wallHit.point.x + wallHit.normal.x * 0.3f, camTransform.position.y - 0.2f, wallHit.point.z + wallHit.normal.z * 0.15f);
        }
    }
}
