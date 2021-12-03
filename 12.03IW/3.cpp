#include <iostream>
#include <map>
#include <string>
#include <vector>
using namespace std;


vector <string> split(string str, char sep) {
    vector <string> result;

    int k = 0;
    for (int i = 0; i < str.size(); i++) {
        if (str[i] == sep) {
            string s = str.substr(k, i - k);
            if (!s.empty()) {
                result.push_back(s);
            }
            k = i + 1;
        } else if (i == str.size() - 1) {
            string s = str.substr(k, i - k + 1);
            if (!s.empty()) {
                result.push_back(s);
            }
        }
        
    }

    return result;
}


int main() {
    map <string, string> dick;

    while (true) {
        string countryCity;
        getline(cin, countryCity);
        if (countryCity == "") {
            break;
        }

        vector <string> VectorCountryCity = split(countryCity, ' ');
        string country = VectorCountryCity[0];
        string city = VectorCountryCity[1];

        dick[country] = city;
        dick[city] = country;
    }

    string request;
    cin >> request;
    cout << dick[request];

    return 0;
}