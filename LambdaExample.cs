using System;
using System.Linq.Expressions;

namespace ExpressionLambdaExamples
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Expression.Lambda Examples ===\n");

            Example1_BasicLambda();
            Example2_StronglyTypedLambda();
            Example3_LambdaWithParameters();
            Example4_AdvancedLambdaWithTailCall();

            Console.WriteLine("\n=== All Expression.Lambda Overloads Demonstrated ===");
        }

        // 1️⃣ Overload 1: Lambda(Expression body)
        static void Example1_BasicLambda()
        {
            Console.WriteLine("1️⃣ Lambda with no parameters:");

            var body = Expression.Constant("Hello Expression Tree");
            var lambda = Expression.Lambda<Func<string>>(body);

            Console.WriteLine(lambda.Compile()());  // Output: Hello Expression Tree
            Console.WriteLine();
        }

        // 2️⃣ Overload 2: Lambda<TDelegate>(Expression body, params ParameterExpression[])
        static void Example2_StronglyTypedLambda()
        {
            Console.WriteLine("2️⃣ Strongly-typed Lambda (Func<int,int>):");

            var param = Expression.Parameter(typeof(int), "x");
            var body = Expression.Multiply(param, Expression.Constant(2));

            var lambda = Expression.Lambda<Func<int, int>>(body, param);
            Console.WriteLine(lambda.Compile()(5)); // Output: 10
            Console.WriteLine();
        }

        // 3️⃣ Overload 3: Lambda(Expression body, params ParameterExpression[])
        static void Example3_LambdaWithParameters()
        {
            Console.WriteLine("3️⃣ Lambda with two parameters (x + y):");

            var x = Expression.Parameter(typeof(int), "x");
            var y = Expression.Parameter(typeof(int), "y");

            var body = Expression.Add(x, y);

            var lambda = Expression.Lambda(body, x, y); // returns untyped LambdaExpression
            var compiled = (Func<int, int, int>)lambda.Compile();

            Console.WriteLine(compiled(7, 3));  // Output: 10
            Console.WriteLine();
        }

        // 4️⃣ Overload 4: Lambda(Expression body, string name, bool tailCall, IEnumerable<ParameterExpression>)
        // A tail call occurs when a function returns the result of another function call directly, without extra work afterward.
        // With tail-call optimization, the runtime reuses the current stack frame, avoiding additional memory usage.
        // This is particularly useful for recursive functions, preventing stack overflows.

        static void Example4_AdvancedLambdaWithTailCall()
        {
            Console.WriteLine("4️⃣ Lambda with name and tail call optimization:");

            var x = Expression.Parameter(typeof(int), "x");
            var body = Expression.Multiply(x, x);

            var lambda = Expression.Lambda<Func<int, int>>(body, "SquareFunction", true, new[] { x });
            Console.WriteLine("Lambda Name: " + lambda.Name);     // Output: SquareFunction
            Console.WriteLine("Result: " + lambda.Compile()(6));  // Output: 36
            Console.WriteLine();
        }
    }
}
