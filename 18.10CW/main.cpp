#include <iostream>
#include <fstream>
#include <string>

using namespace std;

int main() {
    ifstream fin;  // Считать данные из файла
    ofstream fout;  // Перезаписать данные из файла

    string s;
    int a;

    fin.open("file_name.txt");
    fout.open("file_name2.txt");

    fin >> a;  // Считать до пробельного символа, чтобы до Enter, нужно использовать следующую конструкцию:
    getline(fin, s);
    fout << a;

    fin.close();  // Необязательно
}