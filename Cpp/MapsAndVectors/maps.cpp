// map - словарь (dict если в python)

#include <iostream>
#include <map>
using namespace std;

int main() {
    map <string, int> nums;  // в стрелочках указывается тип данных ключа и значения
    nums = {  // Так заполняется map
        {"zero", 0},  // zero ключ, 0 значение
        {"one", 1},
        {"two", 2}
    };

    cout << nums["zero"];  // вывод значения с ключом "zero"

    nums.insert(make_pair("three", 3));  // Добавление пары ключ, значение 
    nums["four"] = 4;  // Так тоже можно

    return 0;
}