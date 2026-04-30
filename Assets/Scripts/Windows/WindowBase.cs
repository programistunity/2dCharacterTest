using UnityEngine;

public abstract class WindowBase : MonoBehaviour
{
	public  virtual void OnOpen()
	{

	}

	public virtual void Close()
	{
		Destroy(gameObject);
	}
}
