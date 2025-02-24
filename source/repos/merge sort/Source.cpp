////Merge sort
 
//#include<iostream>
//using namespace std;
//void merge(int arr[], int left, int mid, int right)
//{
//	int len1 = mid - left + 1;
//	int len2 = right - mid;
//	int* l = new int[len1];
//	int* r = new int[len2];
//	// copy each subArray with their values
//	for (int i = 0; i < len1; i++)
//		l[i] = arr[i + left];
//		for (int j = 0; j < len2; j++)
//			r[j] = arr[j + mid+1];
//
//	// Sort the vector using subArrays
//		int i = 0, j = 0, k = left;
//	while ( i < len1 && j < len2)
//	{
//		if (l[i] < r[j])
//			arr[k++] = l[i++];
//		else
//			arr[k++] = r[j++];		
//	}
//
//     // check if the subArrays still have some numbers
//	while (i < len1)
//		arr[k++] = l[i++];
//	while (j < len2)
//		arr[k++] = r[j++];
//}
//void mergesort(int arr[], int left, int right)
//{
//	//condition 
//	if (left >= right)
//		return;
//
//	// recurgin
//	int mid = (left + right) / 2;
//	mergesort(arr, left, mid);
//	mergesort(arr,mid + 1, right);
//
//	merge(arr, left, mid, right);
//}
//int main()
//{
//	int arr[]{ 7,3,-1,100,4,8,0,-3 };
//	int l = 0;
//	int r = sizeof(arr) / sizeof(arr[0])-1; //=4*4/4=4 element=3 indexs from 0 to 3
//	mergesort(arr, l, r);
//
//	for (int i = 0; i <=r; i++)
//		cout << arr[i] << " ";
//	return 0;
//}

// quik sort
#include<iostream>
using namespace std;
int partition(int arr[], int ibegain, int jend)
{
	int i = ibegain;
	int j = jend;
	int pov = i;
	while (true)
	{
		while (arr[pov] <= arr[j] && pov != j)
			--j;
		if (pov == j)
			break;
		else if (arr[pov] > arr[j])
		{
			swap(arr[j], arr[pov]);
			pov = j;
		}
		while (arr[pov] >= arr[i] && pov != i)
			++i;
		if (pov == i)
			break;
		else if (arr[pov] < arr[i])
		{
			swap(arr[i], arr[pov]);
			pov = i;
		}
	};
	return pov;
}
void quiksort(int arr[], int left, int right)
{
	if (left < right)
	{
		int pov = partition(arr, left, right);
		quiksort(arr, left, pov - 1);
		quiksort(arr,pov +1,right);
	}
}
//int main()
//{
//	int arr[]{ 50,20,60,10,30,40 };
//	quiksort(arr, 0, 5);
//	for (int i = 0; i <6; i++)
//	cout << arr[i] << " ";
//	return 0;
//}



