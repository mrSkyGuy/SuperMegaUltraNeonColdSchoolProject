#include <iostream>
#include <string>
using namespace std;


const string strNums[10] = {
      "0", "1", "2", "3", "4", 
      "5", "6", "7", "8", "9"
  };


string reverse(string s) {
    string res = "";
    int len = s.length();
    for (int i = len - 1; i >= 0; i--) {
        res += s[i];
    }
    
    return res;
}


string to_str(int n) {
    string result = "";

    int x = n;  // Чтобы не изменять исходное число
    do {
        result += strNums[abs(x % 10)];
        x /= 10;
    } while (x);

    result = n >= 0 ? result : result + "-"; 
    result = reverse(result);

    return result;
}


int main() {
  int number;
  cin >> number;
  cout << to_str(number);

  return 0;
}
