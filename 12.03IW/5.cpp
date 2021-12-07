#include <iostream>
#include <map>
#include <vector>
#include <algorithm>
using namespace std;


string to_lower(string s) {
    for (char &c: s) {
        c = tolower(c);
    }
    return s;
}


bool check(string s, char c) {
    for (auto i: s) {
        if (c == i) {
            return true;
        }
    }
    return false;
}


int main() {
    map <string, string> cities;
    vector <string> cities_keys;
    // map<string, string> countries;

    string st;
    string country, city;

    while (true) {
        getline(cin, st);
    
        if (st.empty()) {
            break;
        }

        for (int i = 0; i < st.size(); i++) {
            if (st[i] == ' ') {
                country = st.substr(0, i);
                city = st.substr(i + 1, st.size());
            }
        }
        cities[to_lower(city)] = country;
        cities_keys.push_back(to_lower(city));
    }

    char symb;
    cin >> symb;

    for (auto city: cities_keys) {
        if (check(city, tolower(symb))) {
            cout << cities[city] << endl;
        }
    }

    return 0;
}