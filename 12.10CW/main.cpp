#include <iostream>
#include <cstring>
using namespace std;

int main() {
    string name1 = "Z@yЯ";  // Создание строки

    char name[20] = "";
    char s1[20] = "";
    cout << "Hi, " << name << endl;
    cout << "Длина имени: " << strlen(name);  // Работает только для массива символов (char)
    strcpy(s1, name);  // s1 = name[:]. Также работает только для char
    strncpy(s1, name, 4);  // s1 = name[:4]. Также работает только для char
    strcat(s1, name);  // s1 = s1 + name. Также работает только для char

    char *n = strchr(name, 'a');  // Индекс на 'a'. Аналог string.index() в python
    char c;
    isalnum(c);  // проверка на букву либо цифру
    isalpha(c);  // Проверка на букву
    isdigit(c);  // Проверка на цифру
    islower(c);  // Проверка на нижний регистр
    isupper(c);  // Проверка на верхний регистр
    isspace(c);  // Проверка на пробельный символ
    toupper(c);  // В верхний регистр
    tolower(c);  // В нижний регистр
    atof(s1);  // преобразование в double
    atoi(s1);  // преобразование в int
    atol(s1);  // преобразование в long int

    // Все эти ф-ии только для char
}