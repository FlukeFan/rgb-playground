
using System;
using System.Linq.Expressions;

namespace Demo
{
    public enum PersonGender
    {
        Male,
        Female,
    }

    public class Demo
    {
        [STAThread]
        public static void Main()
        {
            PersonGender gender = PersonGender.Male;
            Expression<Func<bool>> lambda = (() => gender == PersonGender.Female);
            BinaryExpression be = (BinaryExpression)lambda.Body;
            UnaryExpression ue = (UnaryExpression)be.Left; // Unexpectedly cast to an Int32
            MemberExpression me = (MemberExpression)ue.Operand;
            ConstantExpression ce = (ConstantExpression) be.Right;
            Console.WriteLine(me.Type); // PersonGender
            Console.WriteLine(ce.Type); // Int32
        }
    }
}

