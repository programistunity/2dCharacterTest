using UnityEngine;

public class InputController : MonoBehaviour
{
	public static InputController Instance { get; set; }
	public bool IsMobileInput;
	public IInputInterface InputInterface;

	public void Awake()
	{
		Instance = this;

		
		if (IsMobileInput)
		{
			InputInterface=new MobileInputInterface();
			var window = UiController.Instance.OpenWindow<WindowMobileInput>();
			InputInterface.Init(window);
		}
		else
		{
			InputInterface=new PcInputInterface();
			InputInterface.Init(null);
		}
	}

	void Start()
	{
		if (IsMobileInput)
		{
			var window = UiController.Instance.OpenWindow<WindowMobileInput>();
			InputInterface.Init(window);
		}
	}

	void Update()
	{
		InputInterface.Update();
	}
}
