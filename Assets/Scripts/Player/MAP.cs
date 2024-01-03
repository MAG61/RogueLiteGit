using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class MAP<TKey, TValue> 
{
	[SerializeField]
	private List<TKey> keysList = new List<TKey>();
	public List<TKey> KeysList
	{
		get { return keysList; }
		set { keysList = value; }
	}

	[SerializeField]
	private List<TValue> valuesList = new List<TValue>();
	public List<TValue> ValuesList
	{
		get { return valuesList; }
		set { valuesList = value; }
	}


	public void SetValue(TKey key,TValue value)
    {
		for (int i = 0; i< keysList.Count; i++)
        {
			if (keysList[i].Equals(key)) valuesList[i] = value;
        }
    }

	public TValue GetValue(TKey key)
    {
		for (int i =0;i< keysList.Count; i++)
        {
			if (keysList[i].Equals(key)) return valuesList[i];
        }

		return default(TValue);
    }
}
