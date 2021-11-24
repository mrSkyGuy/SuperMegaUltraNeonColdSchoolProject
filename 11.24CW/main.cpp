#include <iostream>
using namespace std;


void printTable() {
    cout << "  +----+----+----+----+----+----+----+----+" << endl;
    for (int i = 7; i >= 0; i--) {
        cout << i << " |    |    |    |    |    |    |    |    |" << endl;
        cout << "  +----+----+----+----+----+----+----+----+" << endl;
    }
    cout << "     0    1    2    3    4    5    6    7" << endl;
}


int main() {
    printTable();

    return 0;
}