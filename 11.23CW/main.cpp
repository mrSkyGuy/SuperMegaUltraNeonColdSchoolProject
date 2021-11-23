#include <iostream>
#include <random>
#include <ctime>
using namespace std;


string getRandName() {
    const string consontans = "qwrtpsdfghjklzxcvbnm";
    const string vowels = "eyuioa";
    string name = "";

    srand(time(0));
    int slogCount = rand() % 5 + 2;

    for (int i = 0; i < slogCount; i++) {
        int index;
        if (i % 2) {
            index = rand() % 19;
            name += consontans[index];
        } else {
            index = rand() % 5;
            name += vowels[index];
        }
    }

    return name;
}


class Unit {
    private:
        string id;
        int hp = 100;
    
    public:
        Unit(string Id) {
            this->id = Id;
            this->hp = hp;
        }

        Unit() {
            this->id = getRandName();
            this->hp = hp;
        }

        void print_info() {
            cout << "ID: " << this->id << endl << "HP" << this->hp << endl;
        }
};


int main() {
    Unit abobus = Unit();
    abobus.print_info();

    return 0;
}