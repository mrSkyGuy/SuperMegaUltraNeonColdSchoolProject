#include <iostream>

using namespace std;


// Указатель - переменная, которая хранит адрес некоторой ячейки памяити
// У указателя тот же тип данных, что и у объекта, на который он указыает

int main() {
//    int arr[5];
//    cout << arr;  // Ссылка на ячейку памяти массива


//    int *p = nullptr;  // Указатель, который ни на что не ссылается
//    int i = 10;
//    p = &i;  // Указали ссылку на переменную "i"
//    cout << p;  // Вывели адрес на переменную "i"
//    cout << *p;  // Разыименование. Вывелось содержимое переменной "i"


    // & - Получение адреса
    // * - Получение значения по адресу


//    p = &(i + i);  // Некорректная запись, тк мы можем получить адрес только у переменной


    // Мы можем переключаться на другие ячейки с помощью + и -
//    cout << *(p + 2);


    int arr[10] = {1, 2, 3, 4, 5, 6};
//    int *p = &arr[0];
//    int *q = &arr[9];
//    cout << *(p + 2);  // Вывелось 3 (arr[0 + 2])
//    // arr[k] -> *(p + k)
//    cout << *(q - 7);  // Вывелось 3 (arr[9 - 7])

    // Присваиваем элементам массива значения от 1 до 10
    for (int *p = arr; p <= arr + 9; p++) {
        *p = (p - arr) + 1;
        cout << *p << endl;
    }



    return 0;
}