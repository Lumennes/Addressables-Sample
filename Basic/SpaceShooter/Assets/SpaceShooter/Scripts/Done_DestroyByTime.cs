using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Done_DestroyByTime : MonoBehaviour
{
	public float lifetime;

    void Start()
	{
        Invoke(nameof(Release), lifetime);
	}

    void Release()
    {
        Addressables.ReleaseInstance(gameObject);
    }
}
