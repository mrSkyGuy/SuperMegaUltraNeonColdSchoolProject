#include <iostream>
#include <fstream>
using namespace std;

int main() {
    ifstream fin;
    ofstream fout;
    string s;

    fin.open("input.txt");
    getline(fin, s);

    int c = 0;
    int dotCount = 0;
    int max = 0;
    bool end = false;

    while (!end) {
        for (auto symb: s) {
            if (symb == '.') {
                dotCount++;
                // cout << "tochka v stroke" << endl;
            } if (dotCount == 2) {
                // cout << "cha budem obrezat" << endl;
                if (c > max) max = c;
                c = 0;
                dotCount = 0;

                int i = s.find(".");
                cout << "  | " << i << endl;
                if (i == 0) {
                    // cout << "tochki zakoncilis" << endl;
                    end = true;
                    break;
                }
                // cout << "  | bilo " << s << endl; 
                s = s.substr(i + 1, s.size() - (i + 1));
                // cout << "  | stalo " << s << endl;
                break;
            } 
            c++;
        }
    }

    if (c > max) max = c;
    cout << max;

    return 0;
}