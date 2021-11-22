#include <iostream>
#include <string>
using namespace std;

int main() {
    string s;
    getline(cin, s);

    int count = 0;
    for (char &i: s) {
        if (isspace(i)) {
            count++;
        }
    }
    cout << count + 1;


    return 0;
}