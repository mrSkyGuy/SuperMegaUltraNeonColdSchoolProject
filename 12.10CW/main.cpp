#include <iostream>
#include <cstring>
using namespace std;


int strnum(char n[10]) {
    char res[10] = "";
    int k = 0;
    for (int i = 0; i < strlen(n); i++) {
            if (isdigit(n[i])) {
                res[k] = n[i];
                k += 1;
            }
    }
    return atoi(res);
}


int main() {
    char n1[10] = "";
    char n2[10] = "";
    cin.getline(n1, 10);
    cin.getline(n2, 10);
    cout << strnum(n1) + strnum(n2);

    return 0;
}