#include <iostream>
#include <map>
#include <vector>
#include <algorithm>

using namespace std;

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
        cities[city] = country;
        cities_keys.push_back(city);
        // countries[city] = country;
    }

    sort(cities_keys.begin(), cities_keys.end());

    // bubble sort
    // int N = cities_keys.size();
    // for (int i = 0, b; i < N - 1; i++) {
    //     for (int j = N - 1; j > i; j--)
    //     {
    //         if (cities_keys[j] < cities_keys[j - 1]) {
    //             string b = cities_keys[j - 1];
    //             cities_keys[j - 1] = cities_keys[j];
    //             cities_keys[j] = b;
    //         }
    //     }
    // }

    for (auto city: cities_keys) {
        cout << city << " (" << cities[city] << ")" << endl;
    }


    return 0;
}