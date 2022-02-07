#include <iostream>
#include <fstream>
#include <vector>
using namespace std;


vector <int> getSplitedNums(string str) {
    vector <int> result;

    int k = 0;
    for (int i = 0; i < str.size();i++) {
        if (isalpha(str[i])) {
            string s = str.substr(k, i - k);
            if (!s.empty()) {
                result.push_back(stoi(s));
            }
            k = i + 1;
        } else if (i == str.size() - 1) {
            string s = str.substr(k, i - k + 1);
            if (!s.empty()) {
                result.push_back(stoi(s));
            }
        }
    }

    return result;
}


int getMaxOddNum(vector <int> nums) {
    int max = 0;
    for (auto num: nums) {
    	if (num % 2 == 1) {
    		if (num > max) {
            	max = num;
        	}
    	}
        
    }
    return max;
}


int main() {
    ifstream fin;
    string s;
    fin.open("input.txt");
    getline(fin, s);

    vector <int> nums = getSplitedNums(s);
    int max = getMaxOddNum(nums);

    cout << max;

    return 0;
}