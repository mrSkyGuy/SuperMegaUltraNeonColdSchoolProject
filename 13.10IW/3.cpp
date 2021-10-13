#include <iostream>
#include <string>

using namespace std;

int main() {
    string s;
    getline(cin, s);
    cout << s.substr(0, s.find('h')) << s.substr(s.rfind('h') + 1, s.size() - 1);

    return 0;
}