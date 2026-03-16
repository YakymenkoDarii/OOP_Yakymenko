#ifndef TGEOMPROGRESSIONM_H
#define TGEOMPROGRESSIONM_H

#include "TGeomProgression.h"
#include <vector>

class TGeomProgressionM : public TGeomProgression {
public:
	TGeomProgressionM();
	TGeomProgressionM(double firstTerm, double ratio);
	TGeomProgressionM(const TGeomProgression& other);

	bool IsGeometricSequence(const std::vector<int>& sequence) const;
	bool IsMember(double number) const;
};

#endif