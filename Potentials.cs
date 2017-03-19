using System;

namespace MethodPotentials
{
    public class Potentials
    {
        private Function _decisiveFunction;
        private int _correction;
        private int NumberClasses { get; }

        public Potentials(int numberClasses)
        {
            NumberClasses = numberClasses;
        }

        public Function GetDecisiveFunction(Point[][] teachingPoints)
        {
            _correction = 1;
            _decisiveFunction = new Function();
            bool isError;
            int iterationNumber = 0;
            do {
                isError = DoOneIteration(teachingPoints, ref _decisiveFunction);
                iterationNumber++;
            } while (isError && iterationNumber < 1000);

            return iterationNumber == 1000 ? null : _decisiveFunction;
        }

        private bool DoOneIteration(Point[][] teachingPoints, ref Function decisiveFunction)
        {
            if (NumberClasses != teachingPoints.Length)
                throw new ArgumentException(
                    "The number of shared classes does not match the number of classes in the training sample");

            bool isError = false;

            for (int classNumber = 0; classNumber < teachingPoints.Length; classNumber++) {
                for (int pointNumber = 0; pointNumber < teachingPoints[classNumber].Length; pointNumber++) {
                    decisiveFunction += _correction * GetPotentialFunction(teachingPoints[classNumber][pointNumber]);
                    int nextPointNumber = (pointNumber + 1) % teachingPoints[classNumber].Length;
                    int nextClassNumber = nextPointNumber == 0 ? (classNumber + 1) % NumberClasses : classNumber;
                    Point nextPoint = teachingPoints[nextClassNumber][nextPointNumber];
                    _correction = GetCorrection(nextPoint, nextClassNumber);
                    if (_correction != 0) isError = true;
                }
            }

            return isError;
        }

        private int GetCorrection(Point point, int classNumber)
        {
            int functionValue = _decisiveFunction.GetValue(point);
            if ((functionValue <= 0) && (classNumber == 0)) return 1;
            if ((functionValue > 0) && (classNumber == 1)) return -1;
            return 0;
        }

        public int GetClassNumberForPoint(Point point)
        {
            int functionValue = _decisiveFunction.GetValue(point);
            if (functionValue > 0) return 0;
            if (functionValue < 0) return 1;
            return -1;
        }

        private Function GetPotentialFunction(Point point)
        {
            return new Function(4 * point.X, 4 * point.Y, 16 * point.X * point.Y, 1);
        }
    }
}
