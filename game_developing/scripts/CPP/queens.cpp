#include <iostream>
using namespace std;

bool ok(int x[], int y) {
    for (int i = 0; i < y; ++i)
        if (x[i] == x[y] || abs(x[i] - x[y]) == y - i)
            return false;
    return true;
}

void put(int x[], int y) {
    if (y == 8) {
        for (int i = 0; i < 8; ++i) {
            for (int j = 0; j < 8; ++j) {
                if (x[i] == j) cout << "Q ";
                else cout << ". ";
            }
            cout << endl;
        }
        cout << endl;
        return;
    }
    for (int i = 0; i < 8; ++i) {
        x[y] = i;
        if (ok(x, y)) put(x, y + 1);
    }
}

int main() {
    int x[8] = {0};
    put(x, 0);
    return 0;
}