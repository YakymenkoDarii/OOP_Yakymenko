#ifndef TGEOMPROGRESSION_H
#define TGEOMPROGRESSION_H

#include <iostream>

class TGeomProgression {
protected:
	double b1;
	double q;

public:
	TGeomProgression();
	TGeomProgression(double firstTerm, double ratio);
	TGeomProgression(const TGeomProgression& other);

	virtual void Input();
	virtual void Output() const;

	double GetNthTerm(int n) const;
	double GetSum(int n) const;

	TGeomProgression operator+(const TGeomProgression& other) const;
	TGeomProgression operator-(const TGeomProgression& other) const;
};

#endif