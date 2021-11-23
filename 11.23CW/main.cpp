#include <iostream>
#include <random>
#include <ctime>
using namespace std;


string getRandName() {
    const string consontans = "qwrtpsdfghjklzxcvbnm";
    const string vowels = "eyuioa";
    string name = "";

    srand(time(0));
    int slogCount = rand() % 3 + 2;

    for (int i = 0; i < slogCount; i++) {
        int index;
        index = rand() % 19;
        name += consontans[index];

        index = rand() % 5;
        name += vowels[index];
    }

    return name;
}


class Unit {
    private:
        string id;
    
    public:
        int hp = 100;
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


class Player: Unit {
    private:
        int lvl = 1;
        int xp = 0;
    
    public:
        int damage = 10;

        void getDamage(Player enemy) {
            this->hp -= enemy.damage;
        }

        void lvlUp() {
            this->lvl++;
        }

        void getXp(int XP) {
            this->xp += XP;
        }
};


int main() {
    Unit abobus = Unit();
    abobus.print_info();

    return 0;
}