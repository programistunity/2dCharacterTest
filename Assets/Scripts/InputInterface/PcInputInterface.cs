using System;
using UnityEngine;

public class PcInputInterface : IInputInterface
{
	public void Init(WindowBase window)
	{
	}

	public void Update()
    {
	    if (Move != null)
	    {
		    Move(Input.GetAxis("Horizontal"));
	    }

	    if (Jump!=null&&Input.GetKeyDown(KeyCode.Space))
	    {
		    Jump();
	    }
    }

    public Action<float> Move { get; set; }
    public Action Jump { get; set; }

}
