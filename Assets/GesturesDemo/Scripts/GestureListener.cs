using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    public Text gestos;
	
	private bool swipeLeft;
	private bool swipeRight;
	
	public bool IsSwipeLeft()
	{
        print("izquierda?");
        if (swipeLeft)
		{
			swipeLeft = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsSwipeRight()
	{
        print("derecha?");
        if (swipeRight)
		{
			swipeRight = false;
			return true;
		}
		
		return false;
	}

	
	public void UserDetected(uint userId, int userIndex)
	{
        print("usuario detectado");
        // detect these user specific gestures
        KinectManager manager = KinectManager.Instance;
		
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);

		if(gestos != null)
		{
			gestos.text = "Swipe left or right to change the slides.";
		}
	}
	
	public void UserLost(uint userId, int userIndex)
	{
        print("usuario perdido");
        if (gestos != null)
		{
			gestos.text = "";
		}
	}

	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		// don't do anything here
	}

	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{

        print("gesto completo");
		string sGestureText = gesture + " detected";
		if(gestos != null)
		{
			gestos.text = sGestureText;
		}
		
		if(gesture == KinectGestures.Gestures.SwipeLeft)
			swipeLeft = true;
		else if(gesture == KinectGestures.Gestures.SwipeRight)
			swipeRight = true;

		return true;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
        // don't do anything here, just reset the gesture state
        print("gesto cancelado");
        return true;
	}
	
}
