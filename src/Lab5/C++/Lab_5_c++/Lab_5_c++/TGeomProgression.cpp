#include "TGeomProgression.h"
#include <cmath>

using namespace std;

TGeomProgression::TGeomProgression() : b1(1.0), q(1.0) {}

TGeomProgression::TGeomProgression(double firstTerm, double ratio) : b1(firstTerm), q(ratio) {}

TGeomProgression::TGeomProgression(const TGeomProgression& other) : b1(other.b1), q(other.q) {}

void TGeomProgression::Input() {
	cout << "Enter the first term (b1): ";
	cin >> b1;
	cout << "Enter the common ratio (q): ";
	cin >> q;
}

void TGeomProgression::Output() const {
	cout << "Geometric Progression: b1 = " << b1 << ", q = " << q << endl;
}

double TGeomProgression::GetNthTerm(int n) const {
	return b1 * pow(q, n - 1);
}

double TGeomProgression::GetSum(int n) const {
	if (q == 1.0) {
		return b1 * n;
	}
	return b1 * (pow(q, n) - 1) / (q - 1);
}

TGeomProgression TGeomProgression::operator+(const TGeomProgression& other) const {
	return TGeomProgression(this->b1 + other.b1, this->q + other.q);
}

TGeomProgression TGeomProgression::operator-(const TGeomProgression& other) const {
	return TGeomProgression(this->b1 - other.b1, this->q - other.q);
}