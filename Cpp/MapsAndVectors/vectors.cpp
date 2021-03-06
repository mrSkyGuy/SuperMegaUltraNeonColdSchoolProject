// Векторы

#include <vector>
#include <iostream>
using namespace std;

int main() {
    vector <int> list;  // Объявление вектора с целыми числами
    list = {1, 2, 3, 4, 5};  // Заполнить вектор можно, как обычный массив
    
    int num = list[0];  // Получение первого элемента вектора. Такая конструкция прокатит не совсеми версиями с++
    int num2 = list.at(0);  // Такая запись более «безопасна»

    list.push_back(11);  // Добавление числа 11 в конец вектора
    list.insert(list.begin(), 12);  // Добавление в начала вектора число 12. Первым аргументом принимает адрес
                                    // Если хотим в третий индекс засунуть значение, можем использовать list.begin() + 2
                                    // list.end() тоже можно использовать
    list.pop_back();  // Удаление последнего элемента вектора

    for (auto i: list)  // Такой записью можно вывести элементы ЛЮБОГО массива. Здесь просто, 
        cout << i << endl; //                чтобы знали. В других ЯП это может быть foreach
    
    for (auto i = list.begin(); i != list.end(); i++)  // Еще один способ пробежаться циклом по вектору
        cout << *i << endl;

    cout << *(--list.end());  // Вывод последнего элемента вектора
    cout << list.size();  // Вывод кол-ва элементов вектора
    
    list.clear();  // Опустошить вектор


    return 0;
}