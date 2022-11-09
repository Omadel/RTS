using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RTS
{
	public class SelectController : MonoBehaviour
	{
		[SerializeField] LayerMask unitLayer;
		
		List<ISelectable> currentSelection = new List<ISelectable>();
		List<IHoverable> currentHoverable = new List<IHoverable>();
		
		bool isShiftPressed;
		
		protected void Update()
		{
			Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
			if (!Physics.Raycast(ray, out RaycastHit hit, 100, unitLayer))
			{
				ClearHover();
				return;
			}
			if(!hit.transform.TryGetComponent<IHoverable>(out IHoverable hoverable))
			{
				ClearHover();
				return;
			}
			Hover(hoverable);
		}
		
		void ClearHover()
		{
			foreach (IHoverable hoverable in currentHoverable)
			{
				hoverable?.UnHover();
			}
			currentHoverable.Clear();
		}
		
		void Hover(IHoverable hoverable)
		{
			if(currentHoverable.Contains(hoverable)) return;
			currentHoverable.Add(hoverable);
			hoverable.Hover();
		}
		
		public void ClearSelection()
		{
			foreach (ISelectable selectable in currentSelection)
			{
				selectable?.UnSelect();
			}
			currentSelection.Clear();
		}
		
		public void SelectSelectable(ISelectable selectable)
		{
			ClearSelection();
			AddToSelection(selectable);
		}
		
		public void AddToSelection(ISelectable selectable)
		{
			selectable.Select();
			currentSelection.Add(selectable);
			ClearHover();
		}
        
		void OnShift(InputValue input)
		{
			isShiftPressed = input.Get<float>() > 0;
		}
        
		void OnLeftClick(InputValue input)
		{
			if(input.Get<float>() > 1) return;
			Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
			if (!Physics.Raycast(ray, out RaycastHit hit, 100, unitLayer))
			{
				if(!isShiftPressed) ClearSelection();
				return;
			}
			if(!hit.transform.TryGetComponent<ISelectable>(out ISelectable selectable))
			{
				if(!isShiftPressed) ClearSelection();
				return;
			}
			
			if(!isShiftPressed) SelectSelectable(selectable);
			else AddToSelection(selectable);
			
		}
		
		void OnRightClick(InputValue input)
		{
			if(currentSelection == null) return;
			Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
			if (!Physics.Raycast(ray, out RaycastHit hit, 100)) return;
			foreach (IRightClick rightClick in currentSelection.Where(s=>s is IRightClick).ToList())
			{
				rightClick.RightClick(hit.point);
			}
		}
        
    }
}
