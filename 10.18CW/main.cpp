#include <iostream>
#include <fstream>
#include <string>

using namespace std;
//    fin.eof() // Конец файла (bool)
int main() {
    ifstream fin;

    string s;
    fin.open("k7.txt");
    getline(fin, s);

    int k = 0, maxk = 0;
    for (int i = 0; i < s.size(); i++) {
        if (s[i] == 'C') {
            k++;
            if (k > maxk) maxk = k;
        }
        else {
            k = 0;
        }
    }
    cout << maxk;

    fin.close();
}