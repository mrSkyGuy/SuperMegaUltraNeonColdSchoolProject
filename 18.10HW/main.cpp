#include <iostream>
#include <string>
#include <vector>
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


vector <string> split(string str) {
    /* split("Имя Фамилия Отчество") -> {"Имя", "Фамилия", "Отчество"} */


    vector <string> result;

    int k = 0;
    for (int i = 0; i < str.size(); i++) {
        if (isspace(str[i])) {
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


vector <string> sorted(vector <string> lst) {
    vector <string> result;



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

    vector <string> okList;
    for (int i = 0; i < list.size(); i++) {
        bool isCapitalize = true;  // Проверка на соблюдение регистра
        bool isTh3Words = true;  // Проверка на то, чтобы в ФИО было 3 слова
        vector <string> fio = split(list[i]);
        if (fio.size() != 3) {
            isTh3Words = false;
            // cout << fio[1] << endl;
        }
        for (string &name: fio) {
            if (capitalize(name) != name) {
                isCapitalize = false;
                // cout << fio[1] << endl;
            }
        }
        if (isTh3Words && isCapitalize) {
            okList.push_back(list[i]);
        }
    }

    for (string &fio: okList) {
        cout << fio << endl;
    }

    // Осталось реализовать ф-ию sorted() и задача будет решена

    return 0;
}