using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerHelper
{
	public static bool CheckLayer(this GameObject gameObject, string layer)
	{
		return gameObject.layer == LayerMask.NameToLayer(layer);
	}

	public static bool CheckLayer(this Collider collider, string layer)
	{
		return collider.gameObject.layer == LayerMask.NameToLayer(layer);
	}
}
