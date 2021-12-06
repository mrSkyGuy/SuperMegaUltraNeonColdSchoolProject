#include <iostream>
#include <map>

using namespace std;

int main() {
    map<string, string> cities;
    map<string, string> countries;
    string st;
    string country, city;
    for (;;) {
        getline(cin, st);
        if (st.empty()) break;
        for (int i = 0; i < st.size(); i++) {
            if (st[i] == ' ') {
                country = st.substr(0, i);
                city = st.substr(i + 1, st.size());
            }
            cities[country] = city;
            countries[city] = country;
        }
    }
    getline(cin, st);
    if (cities.find(st) != cities.end()) {
        cout << cities[st];

    } else {
        cout << countries[st];
    }
    return 0;
}