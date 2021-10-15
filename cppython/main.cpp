#include <iostream>
#include <vector>
#include <string>

using namespace std;


string capitalize(string str) {
    /* capitalize("python") -> "Python" */


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
    return 0;
}

