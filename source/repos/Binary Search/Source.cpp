//#include<iostream>
//using namespace std;
// void BinarySearch(int key,int arr[],int n)
//{
//	 int l = 0, r = n - 1, location = 0;
//	 while (l <= r && location == 0)
//	 {
//	 int mid = (l + r) / 2;
//		 if (key == arr[mid])
//		 {
//			 location = mid;
//			 cout<< location;
//			 break;
//		 }
//		 else if (key < arr[mid])
//		 {
//			 r = mid - 1;
//			 continue;
//		 }
//		 else
//			 l = mid + 1;
//	 }
//
//}
// int main()
// {
//
//	 int arr[5];
//	 for (int i = 0; i < 5; i++)
//		 cin >> arr[i];
//	 cout << "=======================" << endl;
//	 int x;
//	 cout << "Enter num for Search : ";
//	 cin >> x;
//   BinarySearch(x, arr, 5);
//}