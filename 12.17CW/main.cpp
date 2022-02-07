#include <iostream>
#include <iomanip>
#include <cmath>
using namespace std;

int main() {
    int n;
    cin >> n;
    std::cout << std::setprecision(17);
    if (n >= 0) {
        for (int i = 0; i <= n; i++) {
            cout << pow(2, i) << " ";
        }
    } else {
        for (int i = 0; i >= n; i--) {
            cout << pow(2, i) << " ";
        }
    }

    return 0;
}