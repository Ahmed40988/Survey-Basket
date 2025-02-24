#include<iostream>
#include <vector>
using namespace std;
int findsm(const vector<int>& nums)
{
	int l = 0, r = nums.size() - 1;
	int minindex = 0;
	while (l < r)
	{
		int mid = l + (r - l) / 2;
		if (nums[mid] != mid)
		{
			if(mid<minindex)
			minindex = mid;
		}
		if (nums[mid] > mid)
		{
			r = mid;
		}
		else
			l = mid + 1;
	}
	return minindex;
}
int main()
{
	vector<int> nums = { 0,1,2,6,9,11,15 };
	cout << findsm(nums);
	
}











































































#pragma region fib(n)
//#include<iostream>
//using namespace std;
//int dp[500];
//int count1 = 0;
//int fb(int n)
//{
//	++count1;
//	if (dp[n] != -1)
//		return dp[n];
//
//	return dp[n] = fb(n - 1) + fb(n - 2);
//}
//int main()
//{
//	int n;
//	cin >> n;
//	dp[1] = 1;
//	dp[0] = 0;
//	for (int i = 2; i < 500; i++)
//		dp[i] = -1;
//	cout << fb(n) << endl;
//	cout << count1;
//}
#pragma endregion
#pragma region numofsteps(a,b)
//#include<iostream>
//using namespace std;
//int a, b;
//int dp[500];
//int solve(int start)
//{
//	if (start > b)return 0;
//	if (start == b) return 1;
//	if (dp[start] != -1)return dp[start];
//	return  dp[start]=solve(start + 1) + solve(start + 2) + solve(start + 3);
//}
//int main()
//{
//	    cin >> a >> b;
//			for (int i = 2; i < 500; i++)
//		      dp[i] = -1;
//		cout<<solve(a);
//		return 0;
//}
#pragma endregion
#pragma region numm of rotate
//#include <iostream>
//#include <vector>
//using namespace std;
//int findRotations(vector<int>& arr) {
//    int low = 0, high = arr.size() - 1;
//
//    while (low < high) {
//        int mid = (high + low) / 2;
//
//        // Check if the middle element is the minimum
//        if (arr[mid] > arr[high])
//            low = mid + 1;
//        else
//            high = mid;
//    }
//    return low; // The index of the minimum element represents the number of rotations
//}
//int main() {
//    vector<int> arr = { 15, 1, 3, 8, 12 };
//    cout << "Number of rotations: " << findRotations(arr) << endl;
//    return 0;
//}
#pragma endregion
#pragma region unsorted
//#include <iostream>
//#include <vector>
//using namespace std;
//
//// دالة لحساب عدد العناصر غير المرتبة وطباعة هذه العناصر
//int countAndPrintMisplacedElements(const vector<int>& arr, int left, int right) {
//    if (left > right) {
//        return 0; // لا توجد عناصر في هذا النطاق
//    }
//
//    int mid = left + (right - left) / 2;
//    int misplaced = 0;
//
//    // تحقق إذا كان العنصر في مكانه الصحيح
//    if (arr[mid] != mid + 1) {
//        cout << "العنصر " << arr[mid] << " في الموضع " << mid + 1 << " ليس في مكانه الصحيح." << endl;
//        misplaced = 1; // عدّ هذا العنصر
//    }
//
//    // تابع البحث في النصفين
//    misplaced += countAndPrintMisplacedElements(arr, left, mid - 1);
//    misplaced += countAndPrintMisplacedElements(arr, mid + 1, right);
//
//    return misplaced;
//}
//
//int main() {
//    vector<int> arr = { 1, 2, 4, 3, 5, 6 }; // مصفوفة بها أرقام ليست في أماكنها الصحيحة
//    int n = arr.size();
//
//    cout << "العناصر غير المرتبة:" << endl;
//    int misplacedCount = countAndPrintMisplacedElements(arr, 0, n - 1);
//
//    cout << "\nعدد العناصر غير المرتبة: " << misplacedCount << endl;
//
//}
#pragma endregion
#pragma region all
//#include <iostream>
//#include <cmath>
//#include <vector>
//#include <algorithm>
//using namespace std;
//
//namespace Algorithms
//{
//    void selectionSort(vector<int>& list)
//    {
//        const int end = list.size();
//        for (int i = 0; i < end - 1; ++i)
//        {
//            int minIndex = i;
//            for (int j = i + 1; j < end; ++j)
//            {
//                if (list[minIndex] > list[j])
//                    minIndex = j;
//            }
//            swap(list[minIndex], list[i]);
//        }
//    }
//    void BubbleSort(int array[], int N)
//    {
//        for (int i = 0; i < N - 1; i++)
//        {
//            for (int j = 0; j < N - i - 1; j++)
//            {
//                if (array[j] > array[j + 1]) {
//                    swap(array[j], array[j + 1]);
//                }
//            }
//        }
//    }
//    void exchangeSort(vector<int>& list)
//    {
//        const int end = list.size();
//        for (int i = 0; i < end - 1; ++i)
//            for (int j = i + 1; j < end; ++j)
//                if (list[j] < list[i])
//                    swap(list[j], list[i]);
//    }
//
//    void insertionSort(vector<int>& list)
//    {
//        const int end = list.size();
//        int i, j, temp;
//        for (i = 1; i < end; ++i)
//        {
//            temp = list[i];
//            j = i;
//            //! Check if in range and previous element is greater than current
//            while (j > 0 && temp < list[j - 1])
//            {
//                //! Shifting
//                list[j] = list[j - 1];
//                --j;
//            }
//            list[j] = temp;
//        }
//    }
//
//    void merge(vector<int>& list, int left, int middle, int right)
//    {
//        int len1 = middle - left + 1;
//        int len2 = right - middle;
//        vector<int> Llist(len1), Rlist(len2);
//        // TODO: just for copy
//        for (int i = 0; i < len1; ++i)
//            Llist[i] = list[i + left];
//        for (int i = 0; i < len2; ++i)
//            Rlist[i] = list[i + middle + 1];
//        int i = 0, j = 0, k = left;
//        while (i < len1 && j < len2)
//        {
//            if (Llist[i] < Rlist[j])
//                list[k++] = Llist[i++];
//            else
//                list[k++] = Rlist[j++];
//        }
//        // Ensure there are no remaining elements in the subarrays
//        while (i < len1)
//            list[k++] = Llist[i++];
//        while (j < len2)
//            list[k++] = Rlist[j++];
//    }
//
//    void mergeSort(vector<int>& list, int left, int right)
//    {
//        if (left >= right)
//            return;
//        int middle = left + (right - left) / 2;
//        //! just for merge the array into subarrays
//        mergeSort(list, left, middle);
//        mergeSort(list, middle + 1, right);
//        merge(list, left, middle, right);
//    }
//
//    int partition(vector<int>& list, int left, int right)
//    {
//        int pivot = list[left];
//        int i = left;
//        int j = right;
//
//        while (i < j)
//        {
//            while (list[j] > pivot)
//            {
//                j--;
//            }
//            while (list[i] <= pivot && i < j)
//            {
//                i++;
//            }
//            if (i < j)
//            {
//                swap(list[i], list[j]);
//            }
//        }
//        swap(list[left], list[j]);
//
//        return j;
//    }
//
//    void quickSort(vector<int>& list, int left, int right)
//    {
//        if (left < right)
//        {
//            int pivot = partition(list, left, right);
//            quickSort(list, left, pivot - 1);
//            quickSort(list, pivot + 1, right);
//        }
//    }
//}
//
//namespace SearchAlgorithms
//{
//    template <class type>
//    int linearSearch(const vector<type>& list, const type& target)
//    {
//        int end = list.size();
//        for (int i = 0; i < end; ++i)
//            if (list[i] == target)
//                return i;
//        return -1;
//    }
//
//    template <class type>
//    int binarySearch(const vector<type>& list, const type& target)
//    {
//        int right = list.size() - 1;
//        int left = 0;
//        while (left <= right)
//        {
//            //* Avoids overflow that can occur with (left + right) / 2
//            int middle = left + (right - left) / 2;
//            if (list[middle] == target)
//                return middle;
//            else if (list[middle] > target)
//                right = middle - 1;
//            else
//                left = middle + 1;
//        }
//        return -1;
//    }
//}
#pragma endregion


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
//#include<iostream>
//using namespace std;
//int partition(int arr[], int ibegain, int jend)
//{
//	int i = ibegain;
//	int j = jend;
//	int pov = i;
//	while (true)
//	{
//		while (arr[pov] <= arr[j] && pov != j)
//			--j;
//		if (pov == j)
//			break;
//		else if (arr[pov] > arr[j])
//		{
//			swap(arr[j], arr[pov]);
//			pov = j;
//		}
//		while (arr[pov] >= arr[i] && pov != i)
//			++i;
//		if (pov == i)
//			break;
//		else if (arr[pov] < arr[i])
//		{
//			swap(arr[i], arr[pov]);
//			pov = i;
//		}
//	};
//	return pov;
//}
//void quiksort(int arr[], int left, int right)
//{
//	if (left < right)
//	{
//		int pov = partition(arr, left, right);
//		quiksort(arr, left, pov - 1);
//		quiksort(arr,pov +1,right);
//	}
//}
//int main()
//{
//	int arr[]{ 50,20,60,10,30,40 };
//	quiksort(arr, 0, 5);
//	for (int i = 0; i <6; i++)
//	cout << arr[i] << " ";
//	return 0;
//}




