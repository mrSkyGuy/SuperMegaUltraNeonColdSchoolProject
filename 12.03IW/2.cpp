#include <iostream>
#include <map>
#include <string>
using namespace std;

int main() {
    map <string, string> dick;

    while (true) {
        string country, city;
        cin >> country;
        if (country == ".") {
            break;
        }
        cin >> city;
        dick[country] = city;
    }

    string request;
    cin >> request;
    cout << dick[request];

    return 0;
}