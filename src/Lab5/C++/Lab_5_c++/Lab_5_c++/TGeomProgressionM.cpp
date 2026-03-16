#include "TGeomProgressionM.h"
#include <cmath>

using namespace std;

TGeomProgressionM::TGeomProgressionM() : TGeomProgression() {}
TGeomProgressionM::TGeomProgressionM(double firstTerm, double ratio) : TGeomProgression(firstTerm, ratio) {}
TGeomProgressionM::TGeomProgressionM(const TGeomProgression& other) : TGeomProgression(other) {}

bool TGeomProgressionM::IsGeometricSequence(const vector<int>& sequence) const {
	if (sequence.size() < 2) return true;

	double ratio = (double)sequence[1] / sequence[0];
	for (size_t i = 2; i < sequence.size(); ++i) {
		if (abs((double)sequence[i] / sequence[i - 1] - ratio) > 1e-9) {
			return false;
		}
	}
	return true;
}

bool TGeomProgressionM::IsMember(double number) const {
	if (b1 == 0) return number == 0;
	if (number == 0) return false;

	double ratio = number / b1;

	if (q > 0 && ratio < 0) return false;
	if (q == 1) return number == b1;
	if (q == 0) return number == b1;

	double n_minus_1 = log(abs(ratio)) / log(abs(q));

	if (n_minus_1 < -1e-9) return false;
	return abs(n_minus_1 - round(n_minus_1)) < 1e-9;
}