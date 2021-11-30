#include <iostream>
using namespace std;


// поле в котором буду храниться информация о расположении фигур на доске
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


void printTable() {  // вывод "актуальной доски"
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


class Figure {  // класс родитель всех фигур
    public:
        string name;  // имя фигуры на английском языке
        int color;  // цвет цифрой, где 1 - белый, а 0 - черный
        int row, column;  // расположение на доске

    Figure(string name, int color, int row, int column) {
        this->name = name;
        this->color = color;
        this->row = row;
        this->column = column;
    }
    
};


class Pawn: Figure {  // собственно, сама фигура. В нашем случае, пешка
    public:
        void move(int x, int y) {  // метод позволяющий двигать фигурой по доске, если это возможно. ->
            if (this->canMove(x, y)) {  // Это проверяется в методе canMove
                
            }
        }

    private:
        bool isFirstStep = true;  // у пешок, как мы знаем, есть возможность сделать ход на 2 ячейки вперед, 
        // но только тогда, когда мы двигаем этой пешкой впервые. Именно для этого и нужно это поле

        bool canMove(int x, int y) {  // Метод проверяет на возможность хода
            bool isCellEmpty = bool(field[x][y].empty());
            if (this->row == x && (y - this->column == 1 || (y - this->column == 2 && isFirstStep))) {

            }
        }
};


int main() {
    printTable();  // вывод доски

    return 0;
}