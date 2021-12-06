#include <iostream>
#include <map>
#include <vector>
#include <algorithm>

using namespace std;


bool check(string country, string city) {
    for (char i: country) {
        for (char j: city) {
            if (i == j) {
                return true;
            }
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

        if (check(country, city)) {
            cities[city] = country;
            cities_keys.push_back(city);
        }
    }

    vector <string> res;
    for (auto city: cities_keys) {
        res.push_back(cities[city]);
    }

    sort(res.begin(), res.end());
    for (auto country: res) {
        cout << country << endl;
    }

    return 0;
}