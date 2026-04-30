using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiController : MonoBehaviour
{
	public static UiController Instance { get; set; }
	public Dictionary<string,WindowBase> Windows=new Dictionary<string, WindowBase>();
	
	void Awake()
    {
	    if (Instance != null)
	    {
			Destroy(gameObject);
			return;
		}
	    Instance = this;
    }

	public void CloseWindow(WindowBase window)
	{
		var windowName = window.GetType().FullName;
		if (Windows.ContainsKey(windowName))
		{
			Windows.Remove(windowName);
		}

	}

	public void CloseWindow()
	{
		foreach (var window in Windows)
		{
			Destroy(window.Value.gameObject);
		}
		Windows.Clear();
	}


	public void CloseWindow<TWindow>() where TWindow : WindowBase
	{
		var windowName = typeof(TWindow).FullName;
		if (Windows.ContainsKey(windowName))
		{
			Windows[windowName].Close();
		}
	}

	public WindowBase OpenWindow<TWindow>( ) where TWindow : WindowBase
	{
		var windowName = typeof(TWindow).FullName;
		if (Windows.ContainsKey(windowName))
		{
			return Windows[windowName];
		}
		
		var loadedWindow = (GameObject)Resources.Load("Windows/" + windowName);
		loadedWindow = Instantiate(loadedWindow, transform);
		var controller = loadedWindow.GetComponent<WindowBase>();
		controller.OnOpen();
		Windows.Add(windowName,controller);
		
		return controller;
	}

	public WindowBase GetWindow<TWindow>() where TWindow : WindowBase
	{
		var windowName = typeof(TWindow).FullName;
		if (Windows.ContainsKey(windowName))
		{
			return Windows[windowName];
		}

		return null;
	}

}
