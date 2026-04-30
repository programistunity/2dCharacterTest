using System;
using UnityEngine;

public interface IInputInterface
{
	void Init(WindowBase window);
	void Update();

	Action<float> Move { get; set; }
	Action Jump { get; set; }
}
