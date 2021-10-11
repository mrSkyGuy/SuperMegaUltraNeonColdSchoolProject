// Рекурсия

#include <iostream>
using namespace std;

// Реализуем вывод n-го числа Фибоначи через рекурсию
int fib(int n) {
    if (n == 0) {
        return 0;
    }
    if (n == 1) {
        return 1;
    }
    return fib(n - 1) + fib(n - 2);
}

int main() {
    // Вывод перых 13 чисел Фибоначи
    for (int i = 1; i < 13; i++) {
        cout << fib(i) << " ";
    }
}