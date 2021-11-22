#include <iostream>

using namespace std;

int main() {
    string s;
    cin >> s;
    cout << s[2] << endl;
    cout << s[s.size() - 2] << endl;
    cout << s.substr(0, 5) << endl;
    cout << s.substr(0, s.size() - 2) << endl;

    for (int i = 0; i < s.size(); i += 2) {
        cout << s[i];
    }
    cout << endl;

    for (int i = 1; i < s.size(); i += 2) {
        cout << s[i];
    }
    cout << endl;

    for (int i = s.size() - 1; i >= 0; i--) {
        cout << s[i];
    }
    cout << endl;

    for (int i = s.size() - 1; i >= 0; i -= 2) {
        cout << s[i];
    }
    cout << endl;

    cout << s.size();

    return 0;
}