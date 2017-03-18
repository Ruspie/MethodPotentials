namespace MethodPotentials
{
    public class Function
    {
        private int XCoefficient { get; set; }
        private int YCoefficient { get; set; }
        private int XYCoefficient { get; set; }
        private int FreeCoefficient { get; set; }

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

        public int GetY(int x)
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

        public static Function operator *(int coef, Function function)
        {
            return new Function(coef * function.XCoefficient, coef * function.YCoefficient,
                coef * function.XYCoefficient, coef * function.FreeCoefficient);
        }
    }
}
