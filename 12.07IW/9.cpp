#include <iostream>
#include <fstream>
using namespace std;

int main() {
    ifstream fin;
    string s;
    string boss = "BOSS";
    fin.open("input.txt");
    getline(fin, s);

    int c = 0;
    string sums = "";
    for (int i = 0; i < s.size(); i++) {
        if (s[i]) {
            sums += s[i % 4];
        } else {
            sums = "";
        }
        if ((sums == boss) && (s[i - 1] != 'J' && s[i + 1] != 'J')) {
            c++;
            sums = "";
        }

    }
    cout << sums << endl;
    cout << c;

    return 0;
}