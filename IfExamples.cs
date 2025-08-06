using System;
using System.Linq.Expressions;

namespace ExpressionIfExamples
{
    class Program
    {
        static void Main34()
        {
            Console.WriteLine("=== Expression.IfThen & IfThenElse Examples ===\n");

            Example1_IfThen();
            Example2_IfThenElse();
            Example3_IfThen_WithBlock();
            Example4_IfThenElse_WithVariables();

            Console.WriteLine("\n=== All IfThen & IfThenElse Overloads Demonstrated ===");
        }

        // 1️⃣ Example: IfThen(condition, ifTrue)
        static void Example1_IfThen()
        {
            Console.WriteLine("1️⃣ IfThen Example:");

            var x = Expression.Parameter(typeof(int), "x");
            var writeLine = typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })!;

            var condition = Expression.GreaterThan(x, Expression.Constant(10));
            var ifTrue = Expression.Call(writeLine, Expression.Constant("x is greater than 10"));

            var block = Expression.Block(Expression.IfThen(condition, ifTrue));

            var lambda = Expression.Lambda<Action<int>>(block, x).Compile();
            lambda(15); // ✅ prints message
            lambda(5);  // ❌ does nothing

            Console.WriteLine();
        }

        // 2️⃣ Example: IfThenElse(condition, ifTrue, ifFalse)
        static void Example2_IfThenElse()
        {
            Console.WriteLine("2️⃣ IfThenElse Example:");

            var x = Expression.Parameter(typeof(int), "x");
            var writeLine = typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })!;

            var condition = Expression.LessThanOrEqual(x, Expression.Constant(10));
            var ifTrue = Expression.Call(writeLine, Expression.Constant("x is less than or equal to 10"));
            var ifFalse = Expression.Call(writeLine, Expression.Constant("x is greater than 10"));

            var block = Expression.Block(Expression.IfThenElse(condition, ifTrue, ifFalse));

            var lambda = Expression.Lambda<Action<int>>(block, x).Compile();
            lambda(8);   // Output: x is less than or equal to 10
            lambda(20);  // Output: x is greater than 10

            Console.WriteLine();
        }

        // 3️⃣ Example: IfThen with multiple expressions (Block)
        static void Example3_IfThen_WithBlock()
        {
            Console.WriteLine("3️⃣ IfThen Example with multiple statements:");

            var x = Expression.Parameter(typeof(int), "x");
            var writeLineInt = typeof(Console).GetMethod("WriteLine", new[] { typeof(int) })!;

            var condition = Expression.GreaterThan(x, Expression.Constant(0));

            var blockTrue = Expression.Block(
                Expression.Call(writeLineInt, x),
                Expression.Call(typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })!,
                                Expression.Constant("Positive number"))
            );

            var ifThen = Expression.IfThen(condition, blockTrue);

            var lambda = Expression.Lambda<Action<int>>(ifThen, x).Compile();
            lambda(5);   // Prints 5 then "Positive number"
            lambda(-2);  // Prints nothing

            Console.WriteLine();
        }

        // 4️⃣ Example: IfThenElse with variables
        static void Example4_IfThenElse_WithVariables()
        {
            Console.WriteLine("4️⃣ IfThenElse Example with variables:");

            var x = Expression.Parameter(typeof(int), "x");
            var y = Expression.Variable(typeof(string), "y");

            var condition = Expression.GreaterThanOrEqual(x, Expression.Constant(18));

            var ifTrue = Expression.Assign(y, Expression.Constant("Eligible to vote"));
            var ifFalse = Expression.Assign(y, Expression.Constant("Not eligible to vote"));

            var ifThenElse = Expression.IfThenElse(condition, ifTrue, ifFalse);

            var block = Expression.Block(new[] { y }, ifThenElse, y);

            var lambda = Expression.Lambda<Func<int, string>>(block, x).Compile();

            Console.WriteLine(lambda(20)); // Output: Eligible to vote
            Console.WriteLine(lambda(15)); // Output: Not eligible to vote

            Console.WriteLine();
        }
    }
}
