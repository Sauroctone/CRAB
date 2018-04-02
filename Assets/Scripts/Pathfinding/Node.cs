using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {
	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;
	public int gridZ;

	public int gCost; //distance from starting node
	public int hCost; //distance from target node
	public Node parent;
	int heapIndex;

	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, int _gridZ)
	{
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
		gridZ = _gridZ;
	}

	//We never set fCost, so when we just say what to do when we get it, 
	//we want it to always be equal to the sum of gCost and fCost
	public int fCost {
		get { 
			return gCost + hCost;
		}
	}

	public int HeapIndex {
		get {
			return heapIndex;
		}

		set { 
			heapIndex = value;
		}
	}

	public int CompareTo(Node nodeToCompare)
	{
		int compare = fCost.CompareTo (nodeToCompare.fCost);
		if (compare == 0) 
		{
			compare = hCost.CompareTo (nodeToCompare.hCost);
		}

		return -compare;
	}
}
