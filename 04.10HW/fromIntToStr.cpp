#include <iostream>
#include <string>
#include <algorithm>
using namespace std;


const string strNums[10] = {
      "0", "1", "2", "3", "4", 
      "5", "6", "7", "8", "9"
  };


string to_str(int n) {
    string result = "";

    int x = n;  // Чтобы не изменять исходное число
    do {
        result += strNums[abs(x % 10)];
        x /= 10;
    } while (x);

    result = n >= 0 ? result : result + "-"; 
    reverse(result.begin(), result.end());

    return result;
}


int main() {
  int number;
  cin >> number;
  cout << to_str(number);

  return 0;
}
