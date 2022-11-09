using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace RTS
{
	public class Unit : MonoBehaviour, ISelectable, IHoverable, IRightClick
    {
	    public Transform movePosition;
	    [Header("Unit Settings")]
	    [SerializeField] float walkSpeed = 2f;
	    [SerializeField] float runSpeed = 10f;
	    [SerializeField] Color hoverColor = Color.gray, selectColor = Color.white;
	    [Header("Unit Components")]
	    [SerializeField] Animator animator;
	    [SerializeField] QuickOutline.Outline outline;
	    
	    bool isSelected;
	    
	    [ContextMenu("Move")]
	    void testMove()
	    {
	    	Move(movePosition.position);
	    }
	    [ContextMenu("Run")]
	    void testrun()
	    {
	    	Run(movePosition.position);
	    }
	    
	    public void Move(Vector3 position)
	    {
	    	transform.DOKill();
	    	transform.DOLookAt(position, .2f);
	    	transform.DOMove(position, walkSpeed).SetSpeedBased(true).OnComplete(Idle);
	    	animator.CrossFade("Walk", .25f);
	    }
	    
	    public void Run(Vector3 position)
	    {
	    	transform.DOKill();
	    	transform.DOLookAt(position, .2f);
	    	transform.DOMove(position, runSpeed).SetSpeedBased(true).OnComplete(Idle);
	    	animator.CrossFade("Run", .25f);
	    }
	    
	    public void Idle()
	    {
	    	transform.DOKill();
	    	animator.CrossFade("Idle", .25f);
	    }
	    
	    public void Select()
	    {
	    	outline.OutlineColor = selectColor;
	    	outline.enabled = true;
	    	isSelected = true;
	    }
	    
	    public void UnSelect()
	    {
		    outline.enabled = false;
		    isSelected = false;
		    outline.OutlineWidth = 4f;
	    }
	    
	    public void Hover()
	    {
		    if(!isSelected)
		    {
			    outline.OutlineColor = hoverColor;
			    outline.enabled = true;
		    }
		    else
		    {
		    	outline.OutlineWidth = 8f;
		    }
	    }
	    
	    public void UnHover()
	    {
		    if(!isSelected)
		    {
			    outline.enabled = false;
		    }
		    else
		    {
		    	outline.OutlineWidth = 4f;
		    }
	    }
	    
	    public void RightClick(Vector3 position)
	    {
	    	Run(position);
	    }
    }
}
