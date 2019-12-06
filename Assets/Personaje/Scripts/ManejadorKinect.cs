using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorKinect : MonoBehaviour
{
    private static KinectManager _kinect = null;
    public static KinectManager GetKinectManager()
    {
        if(_kinect == null)
        {
            _kinect = KinectManager.Instance;
        }
        return _kinect;
    }
}
