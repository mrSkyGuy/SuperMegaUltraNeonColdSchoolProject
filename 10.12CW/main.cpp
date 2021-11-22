#include <iostream>
#include <string>
using namespace std;


int main() {
    // Строки, как строки. String
    string st = "Hello, world";  // Создание строки
    // Основное преимущество string над массивом символов - это то, что не нужно объявлять заранее размер
    int len = st.size();  // Размер строки. Работает только для string

    string st2;
    // Чтобы осуществить ввод по Enter, нужно использовать специальную конструкцию
    getline(cin, st2);  // Нужно импортировать класс string (2 строка), чтобы работала ф-ия

    // Строки можно сравнивать (>, < и тд) по алфавитному порядку
    // Строки можно складывать
    st2 = st2 + "123";
    // Строки можно складывать и с символом
    st2 = st2 + '.';

    // Можно пробегаться по строке используя циклы: обычный и "адресовой"
    for (char &i: st2) {
        cout << i;
    }

    // ф-ии строк
    string s3 = st.substr(2, 5);  // "Случайная строка" -> "участ" (со второго символа взять 5 символов)
    st.erase(1, 2);  // Удаляет 2 символа, начиная с первого
    st.insert(4, "123");  // Вставить в 4 индекс
    st.clear();  // Очистка строки
    st.empty();  // Проверка на пустоту
    st.find("123");  // Поиск индекса строки "123"
    st.find("123", 4);  // Поиск индекса строки "123" начиная с 4 символа
    st.rfind("123");  // Поиск индекса строки "123" справа
    st.replace(2, 5, "12233");  // Начиная со второго символа заменяем до пятого на "12233"
    stoi(st);  //Аналогично с char
    stol(st);  //Аналогично с char
    stof(st);  //Аналогично с char

    return 0;
}