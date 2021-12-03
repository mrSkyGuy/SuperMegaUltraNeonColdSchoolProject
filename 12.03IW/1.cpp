#include <iostream>
#include <map>
#include <string>
using namespace std;

int main() {
    int n;
    map <string, string> dick;
    cin >> n;

    for (int i = 0; i < n; i++) {
        string country, city;
        cin >> country >> city;
        dick[country] = city;

    }
    string request;
    cin >> request;
    cout << dick[request];

    return 0;
}