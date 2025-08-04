using System;
using System.Linq.Expressions;

class Program3
{
    static void Main3()
    {
        Console.WriteLine("=== Expression.Block Examples ===\n");

        Example1_SimpleBlock();
        Example2_BlockWithExplicitReturnType();
        Example3_BlockWithVariables();
        Example4_BlockWithVariablesAndReturnType();

        Console.WriteLine("\n=== All Expression.Block Overloads Demonstrated ===");
    }

    // 1️⃣ Overload 1: Block(params Expression[])
    static void Example1_SimpleBlock()
    {
        Console.WriteLine("1️⃣ Simple Block (No variables):");

        var block = Expression.Block(
            Expression.Call(typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })!,
                            Expression.Constant("Executing block...")),
            Expression.Add(Expression.Constant(5), Expression.Constant(10))
        );

        var lambda = Expression.Lambda<Func<int>>(block).Compile();
        Console.WriteLine("Result: " + lambda());  // Output: Executing block...  Result: 15
        Console.WriteLine();
    }

    // 2️⃣ Overload 2: Block(Type, params Expression[])
    static void Example2_BlockWithExplicitReturnType()
    {
        Console.WriteLine("2️⃣ Block with Explicit Return Type:");

        var block = Expression.Block(
            typeof(double),
            Expression.Constant(12.5),
            Expression.Constant(20.3) // Last expression determines return value
        );

        var lambda = Expression.Lambda<Func<double>>(block).Compile();
        Console.WriteLine("Result: " + lambda());  // Output: 20.3
        Console.WriteLine();
    }

    // 3️⃣ Overload 3: Block(IEnumerable<ParameterExpression>, IEnumerable<Expression>)
    static void Example3_BlockWithVariables()
    {
        Console.WriteLine("3️⃣ Block with Variables:");

        var x = Expression.Variable(typeof(int), "x");
        var y = Expression.Variable(typeof(int), "y");

        var block = Expression.Block(
            new[] { x, y },
            Expression.Assign(x, Expression.Constant(5)),
            Expression.Assign(y, Expression.Constant(7)),
            Expression.Add(x, y) // Last expression = return value
        );

        var lambda = Expression.Lambda<Func<int>>(block).Compile();
        Console.WriteLine("Result: " + lambda());  // Output: 12
        Console.WriteLine();
    }

    // 4️⃣ Overload 4: Block(Type, IEnumerable<ParameterExpression>, IEnumerable<Expression>)
    static void Example4_BlockWithVariablesAndReturnType()
    {
        Console.WriteLine("4️⃣ Block with Variables and Explicit Return Type:");

        var a = Expression.Variable(typeof(int), "a");
        var b = Expression.Variable(typeof(int), "b");

        var block = Expression.Block(
            typeof(int),
            new[] { a, b },
            Expression.Assign(a, Expression.Constant(10)),
            Expression.Assign(b, Expression.Constant(20)),
            Expression.Multiply(a, b) // Last expression = return value
        );

        var lambda = Expression.Lambda<Func<int>>(block).Compile();
        Console.WriteLine("Result: " + lambda());  // Output: 200
        Console.WriteLine();
    }
}