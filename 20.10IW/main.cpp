#include <iostream>
#include <string>
#include <fstream>

using namespace std;

int main() {
    ifstream fin;
    string symbs;
    int count = 0;
    int maxc = 0;
    // char temp;

    fin.open("input.txt");
    getline(fin, symbs);
    int k = 0;
    for (char &symb: symbs) {
        if (symb == 'A' || symb == 'C' || symb == 'D') count++;
        else {
            // cout << symb << endl;
            if (maxc < count) maxc = count;
            count = 0;
        }
        if (k == symbs.size() - 1) {
            // cout << symb << endl;
            if (maxc < count) maxc = count;
            count = 0;
        }
        
        k++;
    }
    cout << maxc;

    return 0;
}