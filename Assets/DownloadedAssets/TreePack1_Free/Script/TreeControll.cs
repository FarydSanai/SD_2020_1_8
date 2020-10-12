using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph.Drawing.Colors;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

public class TreeControll : MonoBehaviour 
{
	public Animator treeAni;

	[Range(0.0f, 1.0f)]
	public float windPower = 0.5f;

	public bool showLeaf = true;
//	bool leafState;
	public List<GameObject> leafs = new List<GameObject>();


	public float SetWindPower
	{
		set 
		{
			windPower = value;

			if (!treeAni.Equals (null)) 
				treeAni.SetFloat ("Wind",windPower);
		}
	}

	public bool ShowLeaf
	{
		set 
		{
			showLeaf = value;
			LeafHideShow (showLeaf);
		}
	}


	void Start()
	{
//		leafState = showLeaf;
		LeafHideShow (showLeaf);
		//foreach (GameObject leaf in leafs)
		//{
		//	SpriteRenderer sprRen = leaf.GetComponent<SpriteRenderer>();
		//	sprRen.color = new Color(1f,0f,0.7f,1f);
		//}
	}
	
//	void Update () 
//	{
//		if (!showLeaf.Equals (leafState)) {
//			leafState = showLeaf;
//			LeafHideShow (showLeaf);
//		}
//
//		if (!treeAni.Equals (null)) 
//			treeAni.SetFloat ("Wind",windPower);
//	}


	public void LeafHideShow(bool value)
	{
		showLeaf = value;

		if (!leafs.Count.Equals (0)) 
		{
			int count = leafs.Count;
		
			for(int i = 0 ; i < count ; ++i)
			{
				leafs [i].SetActive (showLeaf);
			}
		}
	}

}
