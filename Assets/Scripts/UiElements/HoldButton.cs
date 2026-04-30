using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
	public Action<bool> OnPressed;
	private bool _isPressed;

	void Update()
	{
		if (_isPressed && OnPressed != null)
		{
			OnPressed(_isPressed);
		}
	}

    public void OnPointerDown(PointerEventData eventData)
    {
	    _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
	{
		OnPressed(false);
		_isPressed = false;
    }
}
