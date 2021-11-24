#include <iostream>
using namespace std;


string field[8][8] = {
    {"12", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
    {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
    {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
    {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
    {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
    {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
    {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "},
    {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "}
};

const int white = 1, black = 0;


void printTable() {
    cout << "  +----+----+----+----+----+----+----+----+" << endl;
    for (int i = 7; i >= 0; i--) {
        cout << i << " |";

        for (int j = 0; j < 8; j++) {
            cout << " " << field[i][j] << " |";
        }

        cout << endl << "  +----+----+----+----+----+----+----+----+" << endl;
    }
    cout << "     0    1    2    3    4    5    6    7" << endl;
}


class Figure {
    public:
        string name;
        int color;
        int row, column;

    Figure(string name, int color, int row, int column) {
        this->name = name;
        this->color = color;
        this->row = row;
        this->column = column;
    }
    
};


class Pawn: Figure {
    public:
        void move(int x, int y) {
            if (this->canMove(x, y)) {
                
            }
        }

    private:
        bool isFirstStep = true;

        bool canMove(int x, int y) {
            bool isCellEmpty = bool(field[x][y].empty());
            if (this->row == x && (y - this->column == 1 || (y - this->column == 2 && isFirstStep))) {

            }
        }
};


int main() {
    printTable();

    return 0;
}