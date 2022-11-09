using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
	public interface ISelectable
	{
		public void Select();
		public void UnSelect();
    }
	public interface IHoverable
	{
		public void Hover();
		public void UnHover();
    }
	public interface IRightClick
	{
		public void RightClick(Vector3 position);
    }
}
