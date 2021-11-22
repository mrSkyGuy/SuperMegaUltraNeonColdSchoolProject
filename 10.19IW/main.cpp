#include <iostream>
#include <string>
#include <fstream>

using namespace std;

int main() {
    int count = 1;
    long long int minsum = 20000;
    ifstream fin;
    fin.open("input.txt");
    string s;

    long long int temp, k = 0;
    while (!fin.eof()) {
        getline(fin, s);
        long long int n = stoi(s);

        if (!(k % 2)) {
            temp = n;
        } else {
            if ((temp % 7 == 0 && n % 17) || (n % 7 == 0 && temp % 17)) {
                count++;
                if (minsum > temp + n) minsum = temp + n;
                k = 0;
                continue;
            }
        }
        k++;
    }

    fin.close();

    cout << count << " " << minsum;

    return 0;
}