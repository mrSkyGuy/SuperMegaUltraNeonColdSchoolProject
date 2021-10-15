#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
using namespace std;


string capitalize(string str) {
    /* capitalize("zaur") -> "Zaur" */


    string result = "";

    for (int i = 0; i < str.size(); i++) {
        if (isalpha(str[i])) {
            result += (i == 0) ? toupper(str[i]) : tolower(str[i]);
        } else {
            result += str[i];
        }
    }

    return result;
}


vector <string> split(string str, char sep) {
    /* split("Имя Фамилия Отчество", ' ') -> {"Имя", "Фамилия", "Отчество"} */


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
    vector <string> list;
    string st;
    while (true) {
        getline(cin, st);
        if (st != "") {
            list.push_back(st);
        } else {
            break;
        }
    }

    vector <vector<string>> okList;
    for (int i = 0; i < list.size(); i++) {
        vector <string> fio = split(list[i], ' ');

        for (string &name: fio) {
            name = capitalize(name);
        }
        okList.push_back(fio);
    }

    sort(okList.begin(), okList.end());  // сортировка

    // Вывод фио
    for (vector <string> &fio: okList) {
        for (string &name: fio) {
            cout << name << " ";
        }
        cout << endl;
    }

    return 0;
}