#include <iostream>
#include <cmath>
#include <vector>
#include <algorithm>
using namespace std;

namespace Algorithms
{
  void selectionSort(vector<int> &list)
  {
    const int end = list.size();
    for (int i = 0; i < end - 1; ++i)
    {
      int minIndex = i;
      for (int j = i + 1; j < end; ++j)
      {
        if (list[minIndex] > list[j])
          minIndex = j;
      }
      swap(list[minIndex], list[i]);
    }
  }

  void bubbleSort(vector<int> &list)
  {
    const int end = list.size();
    for (int i = 0; i < end - 1; ++i)
    {
      bool swapped = false;
      for (int j = 0; j < end - i - 1; ++j)
      {
        if (list[j] > list[j + 1])
        {
          swap(list[j], list[j + 1]);
          swapped = true;
        }
      }
      if (!swapped)
        break;
    }
  }

  void exchangeSort(vector<int> &list)
  {
    const int end = list.size();
    for (int i = 0; i < end - 1; ++i)
      for (int j = i + 1; j < end; ++j)
        if (list[j] < list[i])
          swap(list[j], list[i]);
  }

  void insertionSort(vector<int> &list)
  {
    const int end = list.size();
    int i, j, temp;
    for (i = 1; i < end; ++i)
    {
      temp = list[i];
      j = i;
      //! Check if in range and previous element is greater than current
      while (j > 0 && temp < list[j - 1])
      {
        //! Shifting
        list[j] = list[j - 1];
        --j;
      }
      list[j] = temp;
    }
  }

  void merge(vector<int> &list, int left, int middle, int right)
  {
    int len1 = middle - left + 1;
    int len2 = right - middle;
    vector<int> Llist(len1), Rlist(len2);
    // TODO: just for copy
    for (int i = 0; i < len1; ++i)
      Llist[i] = list[i + left];
    for (int i = 0; i < len2; ++i)
      Rlist[i] = list[i + middle + 1];
    int i = 0, j = 0, k = left;
    while (i < len1 && j < len2)
    {
      if (Llist[i] < Rlist[j])
        list[k++] = Llist[i++];
      else
        list[k++] = Rlist[j++];
    }
    // Ensure there are no remaining elements in the subarrays
    while (i < len1)
      list[k++] = Llist[i++];
    while (j < len2)
      list[k++] = Rlist[j++];
  }

  void mergeSort(vector<int> &list, int left, int right)
  {
    if (left >= right)
      return;
    int middle = left + (right - left) / 2;
    //! just for merge the array into subarrays
    mergeSort(list, left, middle);
    mergeSort(list, middle + 1, right);
    merge(list, left, middle, right);
  }

  int partition(vector<int> &list, int left, int right)
  {
    int pivot = list[left];
    int i = left;
    int j = right;

    while (i < j)
    {
      while (list[j] > pivot)
      {
        j--;
      }
      while (list[i] <= pivot && i < j)
      {
        i++;
      }
      if (i < j)
      {
        swap(list[i], list[j]);
      }
    }
    swap(list[left], list[j]);

    return j;
  }

  void quickSort(vector<int> &list, int left, int right)
  {
    if (left < right)
    {
      int pivot = partition(list, left, right);
      quickSort(list, left, pivot - 1);
      quickSort(list, pivot + 1, right);
    }
  }
}

namespace SearchAlgorithms
{
  template <class type>
  int linearSearch(const vector<type> &list, const type &target)
  {
    int end = list.size();
    for (int i = 0; i < end; ++i)
      if (list[i] == target)
        return i;
    return -1;
  }

  template <class type>
  int binarySearch(const vector<type> &list, const type &target)
  {
    int right = list.size() - 1;
    int left = 0;
    while (left <= right)
    {
      //* Avoids overflow that can occur with (left + right) / 2
      int middle = left + (right - left) / 2;
      if (list[middle] == target)
        return middle;
      else if (list[middle] > target)
        right = middle - 1;
      else
        left = middle + 1;
    }
    return -1;
  }
}