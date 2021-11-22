#include <iostream>
#include <string>
#include <fstream>

using namespace std;

int main() {
    ifstream fin;
    string symbs;
    int count = 0;

    fin.open("input.txt");
    getline(fin, symbs);
    
    for (int i = 2; i < symbs.size();) {
        if (
                (symbs[i - 2] == 'A' || symbs[i - 2] == 'C' || symbs[i - 2] == 'E') &&
                ((symbs[i - 1] == 'A' || symbs[i - 1] == 'D' || symbs[i - 1] == 'F') && symbs[i - 1] != symbs[i - 2]) &&
                ((symbs[i] == 'A' || symbs[i] == 'B' || symbs[i] == 'F') && symbs[i] != symbs[i - 1])
            ) {
            count++;
        }
        i++;
    }
    cout << count;

    return 0;
}