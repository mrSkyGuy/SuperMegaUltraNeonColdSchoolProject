#include <iostream>
using namespace std;


int NOD(int n, int d) {
    while (n * d != 0) {
        if (n > d) {
            n %= d;
        } else {
            d %= n;
        }
    }
    return n + d;
}


class Fraction {
    public:
        int numerator;
        int denumerator;
    
    Fraction(int num, int denum) {
        if (num * denum > 0) this->numerator = abs(num);
        else this->numerator = -abs(num);
        this->denumerator = denum;
    }

    Fraction(int num) { Fraction(num, 1); }

    Fraction() { Fraction(1, 1); }

    friend std::ostream &operator << (std::ostream &out, Fraction &p) {
        int n = p.numerator, d = p.denumerator, nod;
        nod = NOD(p.numerator, p.denumerator);
        n /= nod;
        d /= nod;
        out << n << '/' << d;
        return out;
    }
};


Fraction operator + (Fraction x, Fraction y) {
    int n, d;
    n = x.numerator * y.denumerator + y.numerator * x.denumerator;
    d = x.denumerator * y.denumerator;
    return Fraction(n, d);
}


Fraction operator + (Fraction x, int y) {
    Fraction z(y, 1);
    return x + z;
}


Fraction operator + (int y, Fraction x) {
    Fraction z(y, 1);
    return x + z;
}


int main() {
    Fraction x(1, 2);
    Fraction y(3, 4);
    Fraction z(1, 1);
    z = x + y;
    cout << z;

    return 0;
}