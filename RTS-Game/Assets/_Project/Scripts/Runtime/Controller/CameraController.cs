using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RTS
{
    public class CameraController : MonoBehaviour
	{
		[Header("Camera Transforms")]
		[SerializeField] Transform root;
		[SerializeField] Transform angle;
		[SerializeField] Transform zoom;
		
		[Header("Camera Settings")]
		[SerializeField] float panSpeed = 10f;
		[SerializeField] float rotationSpeed = 1f;
		
		Vector3 zoomCached;
		Vector2 delta;
		bool isPanEnabled, isRotationEnabled, isLeftClick;
	    
		protected void Start()
		{
			zoomCached = zoom.localPosition;
		}
	    
	    protected void Update()
	    {
		    PanCamera();   
		    RotateCamera();   
	    }
	    
		void PanCamera()
		{
			if(!isPanEnabled) return;
			var forward = zoom.forward;
			var right = zoom.right;
			forward.y = 0;
			right.y = 0;
			forward.Normalize();
			right.Normalize();
	    	
			var direction = -delta.x * right + -delta.y * forward;
	    	
			root.position += direction * Time.deltaTime * panSpeed;
		}
	    
		void RotateCamera()
		{
			if(!isRotationEnabled || !isLeftClick) return; 
			root.Rotate(0, delta.x * rotationSpeed, 0, Space.Self);
			angle.Rotate(-delta.y * rotationSpeed, 0, 0, Space.Self);
		}
	    
		void OnMouseDelta(InputValue input)
	    {
	    	delta = input.Get<Vector2>();
	    }
	    
		void OnEnableCameraPan(InputValue input)
		{
			isPanEnabled = input.Get<float>() > 0;
		}
	    
		void OnEnableCameraRotation(InputValue input)
		{
			isRotationEnabled = input.Get<float>() > 0;
		}
		
		void OnCameraZoom(InputValue input)
		{
			zoomCached.z += input.Get<float>()/120f;
			zoom.localPosition = zoomCached;
		}
		
		void OnLeftClick(InputValue input)
		{
			isLeftClick = input.Get<float>() > 0;
		}
    }
}
