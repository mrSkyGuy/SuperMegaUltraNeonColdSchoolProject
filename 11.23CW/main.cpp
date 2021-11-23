#include <iostream>
#include <random>
#include <ctime>
using namespace std;


string getRandName() {
    const string consontans = "qwrtpsdfghjklzxcvbnm";
    const string vowels = "eyuioa";
    string name = "";

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
    public:
        string id;
        int hp = 100;
        Unit(string Id) {
            this->id = Id;
            
        }

        Unit() {
            this->id = getRandName();
            
        }

        void print_info() {
            cout << "ID: " << this->id << endl << "HP" << this->hp << endl;
        }
};


class Player: public Unit {
    public:
        int lvl = 1;
        int xp = 0;
    
    
        int damage = 10;

        // Player() {
        //     // this->damage = damage;
        //     this->lvl = lvl;
        //     this->xp = xp;
        // }

        void getDamage(Player &enemy) {
            hp -= enemy.damage;
        }

        void lvlUp() {
            this->lvl++;
        }

        void getXp(int XP) {
            this->xp += XP;
        }

        void doDamage(Player enemy) {
            enemy.getDamage(*this);

            cout << "Player " << enemy.id << " got damage from " << this->id 
                 << " " << this->damage << " points" << endl;
        }
};


int main() {
    srand(time(0));

    Player mainHero = Player();
    Player mainEnemy = Player();
    cout << mainEnemy.damage;
    while (mainHero.hp > 0 && mainEnemy.hp > 0) {
        cout << mainHero.hp << endl;
        
        mainHero.doDamage(mainEnemy);
        mainEnemy.doDamage(mainHero);
        cout << mainHero.hp << endl;
        break;
    }

    return 0;
}