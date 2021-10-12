#include <iostream>
using namespace std;

int main() {
    // Создать "строку" мы можем с помощью массива символов
    char name[20] = "";
    cout << "Введите имя" << "\n";
    cin.getline(name, 20);  // Получение ввода по строкам (через Enter), \0 добавляется автоматически
    cout << "Hello, " << name << "!" << endl;
}