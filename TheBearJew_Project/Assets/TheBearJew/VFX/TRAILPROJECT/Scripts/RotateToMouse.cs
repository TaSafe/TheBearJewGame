using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public Camera camera;
    public float MaximumLenght;
    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 direction;
    private Quaternion rotation;


    
    void Update()
    {
        if(camera != null)
        {
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            rayMouse = camera.ScreenPointToRay(mousePos);
            if(Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, MaximumLenght))
            {
                RotateToMouseDirection(gameObject,hit.point);
            }else
            {
                var pos = rayMouse.GetPoint(MaximumLenght);
                RotateToMouseDirection(gameObject,pos);
            }
        }else
        {
            Debug.Log("No Camera");
        }   
    }
    void RotateToMouseDirection(GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }
    public Quaternion GetRotation()
    {
        return rotation;
    }
}
