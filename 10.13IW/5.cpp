#include <iostream>
#include <string>

using namespace std;

int main() {
    string s;
    getline(cin, s);
    for (int i = 0; i < s.size(); i ++) {
        if (s.find(s[i]) != s.rfind(s[i])) {
            cout << s[s.find(s[i])];
            return 0;
        }
    }
    return 0;
}