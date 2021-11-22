#include <iostream>
using namespace std;


const int N = 10;
int arr[N];


void bubbleSort() {
    for (int i = 0, b; i < N - 1; i++) {
        for (int j = N - 1; j > i; j--)
        {
            if (arr[j] < arr[j - 1])
            {
                b = arr[j - 1];
                arr[j - 1] = arr[j];
                arr[j] = b;
            }
        }
    }
}


void printSortedArray() {
    for (int &i: arr) {
        cout << i << " ";
    }
}


int main() {
    for (int &i: arr) {
        cin >> i;
    }

    bubbleSort();
    printSortedArray();

    return 0;
}