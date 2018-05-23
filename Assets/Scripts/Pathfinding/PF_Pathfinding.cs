using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PF_Pathfinding : MonoBehaviour {

	PF_Grid grid;

	void Awake () {
		grid = GetComponent<PF_Grid> ();
	}
	
	public Vector3[] FindPath(Vector3 startPos, Vector3 targetPos)
	{
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

		Node startNode = grid.GetNodeFromWorldPoint (startPos); 
		Node targetNode = grid.GetNodeFromWorldPoint (targetPos); 

		if (!targetNode.walkable) 
		{
			targetNode = GetClosestValidNode (targetNode, targetPos);
		}

		if (startNode.walkable && targetNode.walkable) {
			Heap<Node> openSet = new Heap<Node> (grid.MaxSize); //Nodes to be evaluated (check if it is the closest to target)
			HashSet<Node> closedSet = new HashSet<Node> ();//Nodes already evaluated (is used to add his neighbours to the openSet)
			openSet.Add (startNode);

			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst ();

				/*//Find the node with the lowest fCost (picks the one with the lowest hcost if there are several)
				for (int i = 1; i < openSet.Count; i++)
				{
					if (openSet [i].fCost < currentNode.fCost || openSet [i].fCost == currentNode.fCost && openSet [i].hCost < currentNode.hCost) 
					{
						currentNode = openSet [i];
					}
				}

				//The node is eveluated, so we pass it to the closedSet
				openSet.Remove (currentNode);*/
				closedSet.Add (currentNode);

				//Checks if the node is the target
				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}

				//Check the neighbours of the current node to add them to the open set 
				//where they will be evaluted in the next loop
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					// pass the node if it's not walkable or has already been evaluted
					if (!neighbour.walkable || closedSet.Contains (neighbour)) {
						continue;
					}

					//update or set gCost and hCost of the node
					int newMovementCostToNeighbour = currentNode.gCost + GetDistance (currentNode, neighbour);
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains (neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance (neighbour, targetNode);
						//set the currentNode as "parent" (it's a variable) of the neighbour node 
						//(the "parenting" hierachy will help trace the path from the target to the start)
						neighbour.parent = currentNode;

						//Add to open set if it's not already in
						if (!openSet.Contains (neighbour)) {
							openSet.Add (neighbour);
							openSet.UpdateItem (neighbour);
						}
					}
				}
			}
		}

		if (pathSuccess) {
			waypoints = RetracePath (startNode, targetNode);
			return RetracePath (startNode, targetNode);
		} else
			return null;
	}

	Vector3[] RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;

		while (currentNode != startNode) 
		{
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}
		path.Add (startNode);
		Vector3[] waypoints = SimplifyPath (path);
		Array.Reverse (waypoints);
		return waypoints;
		//grid.path = path;
	}

	Vector3[] SimplifyPath(List<Node> path)
	{
		grid.path = path;
		List<Vector3> waypoints = new List<Vector3>();
		Vector3 directionOld = new Vector3 (0,0,0);

		for (int i = 1; i < path.Count; i++) 
		{
			Vector3 directionNew = new Vector3 (path [i - 1].gridX - path [i].gridX, path [i - 1].gridY - path [i].gridY, path [i - 1].gridZ - path [i].gridZ);
			if (directionNew != directionOld) 
			{
				waypoints.Add (path [i-1].worldPosition);
			}
			directionOld = directionNew;
		}

		return waypoints.ToArray();
	}

	int GetDistance(Node nodeA, Node nodeB)
	{
		int[] diffs = {
			Mathf.Abs (nodeA.gridX - nodeB.gridX),
			Mathf.Abs (nodeA.gridY - nodeB.gridY),
			Mathf.Abs (nodeA.gridZ - nodeB.gridZ)
		};
		Array.Sort (diffs);

		return 3 * diffs [0] + 2 * (diffs [1] - diffs [0]) + 1 * (diffs [2] - diffs [1] - diffs [0]);
	}

	Node GetClosestValidNode(Node invalidNode, Vector3 targetPos)
	{
		Node newNode = invalidNode;
		List<Node> neighbours = grid.GetNeighbours (invalidNode);
		float distance = Mathf.Infinity;

		for (int i = 0; i < neighbours.Count; i++) 
		{
			float newDist = (neighbours [i].worldPosition - targetPos).magnitude;
			if (neighbours [i].walkable && newDist < distance) 
			{
				newNode = neighbours [i];
				distance = newDist;
			}
		}

		return newNode;
	}



}
