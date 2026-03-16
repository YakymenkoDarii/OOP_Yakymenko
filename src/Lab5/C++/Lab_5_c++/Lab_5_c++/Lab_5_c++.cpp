#include <iostream>
#include <vector>
#include "TGeomProgression.h"
#include "TGeomProgressionM.h"

using namespace std;

int main() {
	cout << "--- Testing Base Class ---" << endl;
	TGeomProgression gp1(2.0, 3.0);
	gp1.Output();
	cout << "4th term: " << gp1.GetNthTerm(4) << endl;
	cout << "Sum of first 4 terms: " << gp1.GetSum(4) << endl;

	cout << "\n--- Testing Overloads ---" << endl;
	TGeomProgression gp2(3.0, 2.0);
	TGeomProgression gp3 = gp1 + gp2;
	gp3.Output();

	cout << "\n--- Testing Derived Class ---" << endl;
	TGeomProgressionM gpM(2.0, 3.0);

	cout << "Is 54 a member of gpM? " << (gpM.IsMember(54.0) ? "Yes" : "No") << endl;
	cout << "Is 50 a member of gpM? " << (gpM.IsMember(50.0) ? "Yes" : "No") << endl;

	vector<int> seq1 = { 2, 4, 8, 16 };
	vector<int> seq2 = { 2, 4, 9, 16 };
	cout << "Is {2, 4, 8, 16} a geometric sequence? " << (gpM.IsGeometricSequence(seq1) ? "Yes" : "No") << endl;
	cout << "Is {2, 4, 9, 16} a geometric sequence? " << (gpM.IsGeometricSequence(seq2) ? "Yes" : "No") << endl;

	return 0;
}