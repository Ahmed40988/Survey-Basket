#include<iostream>
using namespace std;
void ExchangeSort(int n, int arr[])
{
	for (int i = 0; i < n - 1; i++)
	{
		for (int j = i + 1; j < n; j++)
		{
			if (arr[j] < arr[i])
				swap(arr[j], arr[i]);
		}
	}
}

void SelectionSort(int n, int arr[])
{
		int j ;
	for (int i = 0; i < n - 1; i++)
	{
		int min_location = i;
		for ( j = i + 1; j < n; j++)
		{
			if (arr[j] < arr[min_location])
				min_location = j;
		}
				swap(arr[i], arr[min_location]);
	}
}
int main()
{
	int arr[100];
	int n = 0;
	cout << "Enter size number :";
	cin >> n;
	for (int i = 0; i < n; i++)
	{
		cout << "Enter number " << i + 1 << ":";
		cin >> arr[i];
	}
		cout << "=======================" << endl;
	//ExchangeSort(n, arr);
	//for (int i = 0; i < n; i++)
	//{
	//	cout << arr[i] << ",";
	//}	
	SelectionSort(n, arr);
	for (int i = 0; i < n; i++)
	{
		cout << arr[i] << ",";
	}
}