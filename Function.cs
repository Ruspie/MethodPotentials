namespace MethodPotentials
{
    public class Function
    {
        public override int GetHashCode()
        {
            unchecked {
                var hashCode = XCoefficient;
                hashCode = (hashCode * 397) ^ YCoefficient;
                hashCode = (hashCode * 397) ^ XYCoefficient;
                hashCode = (hashCode * 397) ^ FreeCoefficient;
                return hashCode;
            }
        }

        private int XCoefficient { get; }
        private int YCoefficient { get; }
        private int XYCoefficient { get; }
        private int FreeCoefficient { get; }

        public Function() { }

        public Function(int xCoef, int yCoef, int xyCoef, int freeCoef)
        {
            XCoefficient = xCoef;
            YCoefficient = yCoef;
            XYCoefficient = xyCoef;
            FreeCoefficient = freeCoef;
        }

        public int GetValue(Point point)
        {
            return FreeCoefficient + XCoefficient * point.X + YCoefficient * point.Y + XYCoefficient * point.X * point.Y;
        }

        public double GetY(double x)
        {
            return -(XCoefficient * x + FreeCoefficient) / (XYCoefficient * x + YCoefficient);
        }

        public static Function operator +(Function firstFunction, Function secondFunction)
        {
            return new Function(firstFunction.XCoefficient + secondFunction.XCoefficient,
                firstFunction.YCoefficient + secondFunction.YCoefficient,
                firstFunction.XYCoefficient + secondFunction.XYCoefficient,
                firstFunction.FreeCoefficient + secondFunction.FreeCoefficient);
        }

        public override string ToString()
        {
            if (XYCoefficient != 0)
                return "y=(" + -XCoefficient + "*x" + (-FreeCoefficient < 0 ? "" : "+") + -FreeCoefficient + ")/(" +
                       XYCoefficient + "*x" + (YCoefficient < 0 ? "" : "+") + YCoefficient + ")";
            if (YCoefficient != 0)
                return "y=" + -(double) XCoefficient / YCoefficient + "*x" +
                       (-(double) FreeCoefficient / YCoefficient < 0 ? "" : "+") +
                       -(double) FreeCoefficient / YCoefficient;
            return "x=" + -(double) FreeCoefficient / XYCoefficient;
        }

        public static Function operator *(int coef, Function function)
        {
            return new Function(coef * function.XCoefficient, coef * function.YCoefficient,
                coef * function.XYCoefficient, coef * function.FreeCoefficient);
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj != null && GetType() != obj.GetType()) return false;
            Function function = (Function) obj;
            if (function != null && (function.XCoefficient == XCoefficient && function.YCoefficient == YCoefficient &&
                                     function.XYCoefficient == XYCoefficient && function.FreeCoefficient == FreeCoefficient)) return true;
            return false;
        }
    }
}
