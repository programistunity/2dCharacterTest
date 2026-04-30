using System;
using UnityEngine;

public class MobileInputInterface : IInputInterface
{
	public void Init(WindowBase window)
	{
		var mobileInputWindow = window as WindowMobileInput;
		mobileInputWindow.JumpButton.onClick.AddListener(()=> Jump());

		mobileInputWindow.MoveLeftButton.OnPressed+=(state)=>
		{
			if (state)
			{
				Move(-1);
			}
			else
			{
				Move(0);
			}
			
		};
		mobileInputWindow.MoveRightButton.OnPressed += (state) =>
		{
			if (state)
			{
				Move(1);
			}
			else
			{
				Move(0);
			}
		};
	}

	public void Update()
	{

	}

	public Action<float> Move { get; set; }
    public Action Jump { get; set; }
}
