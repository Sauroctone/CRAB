using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This class can take elements of any class : T
public class Heap<T> where T : IHeapItem<T> {

	T[] items;
	int currentItemCount;

	public Heap(int maxHeapSize) 
	{
		items = new T[maxHeapSize];
	}

	public void Add(T item)
	{
		item.HeapIndex = currentItemCount;
		items [currentItemCount] = item;
		SortUp (item);
		currentItemCount++;
	}

	public T RemoveFirst()
	{
		T firstItem = items [0];
		currentItemCount--;
		items [0] = items [currentItemCount];
		items [0].HeapIndex = 0;
		SortDown (items [0]);
		return firstItem;
	}

	void SortDown(T item)
	{
		while (true) 
		{
			int childIndexLeft = item.HeapIndex * 2 + 1;
			int childIndexRight = item.HeapIndex * 2 + 2;
			int swapIndex = 0;

			if (childIndexLeft < currentItemCount) 
			{
				swapIndex = childIndexLeft;
				if (childIndexRight < currentItemCount) 
				{
					if (items [childIndexLeft].CompareTo (items [childIndexRight]) < 0) 
					{
						swapIndex = childIndexRight;
					}
				}

				if (item.CompareTo (items [swapIndex]) < 0) 
				{
					Swap (item, items [swapIndex]);
				} else 
				{
					return;
				}

			} 
			else 
			{
				return;
			}
		}
	}

	public void UpdateItem(T item)
	{
		SortUp (item);
	}

	public int Count {
		get { 
			return currentItemCount;
		}
	}

	public bool Contains(T item) {
		return Equals (items[item.HeapIndex], item);
	}

	void SortUp(T item) //swaps item with its parent as long as it has a higher priority
	{
		int parentIndex = (item.HeapIndex - 1) / 2;

		while (true) 
		{
			T parentItem = items [parentIndex];
			if (item.CompareTo (parentItem) > 0) {
				Swap (item, parentItem);
			} else {
				break;
			}
		}
	}

	void Swap(T itemA, T itemB)
	{
		items [itemA.HeapIndex] = itemB;
		items [itemB.HeapIndex] = itemA;

		int itemAIndex = itemA.HeapIndex;
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = itemAIndex;
	}
}

//We don't know if "T" has the int variable we want, so we tell him he can by adding the interface
public interface IHeapItem<T> : IComparable<T> //Icomparable allows us to use "CompareTo"
{
	int HeapIndex {
		get;
		set;
	}
}