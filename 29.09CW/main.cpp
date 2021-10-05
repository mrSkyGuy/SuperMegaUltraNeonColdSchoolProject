#include <iostream>
#include <cmath>
using namespace std;


int distance(int x1, int y1, int x2, int y2) {
    return sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
}


int perimeter (int a, int b, int c) {
    return a + b + c;
}


int squre(int a, int b, int c) {
    int p2 = perimeter(a, b, c) / 2;
    return sqrt(p2 * (p2 - a) * (p2 - b) * (p2 - c));
}


int main() {
    int x1, y1, x2, y2, x3, y3;
    cin >> x1 >> y1 >> x2 >> y2 >> x3 >> y3;

    int a = distance(x1, y1, x2, y2), b = distance(x1, y1, x3, y3), c = distance(x2, y2, x3, y3);

    cout << "Perimeter: " << perimeter(a, b, c) << endl;
    cout << "Squre: " << squre(a, b, c) << endl;


    return 0;
}
