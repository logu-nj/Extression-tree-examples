using System;
using System.Linq.Expressions;

namespace ExpressionGotoExamples
{
    class Program
    {
        static void Main3()
        {
            Console.WriteLine("=== Expression.Goto Examples ===\n");

            Example1_BasicGoto();
            Example2_GotoWithReturnValue();
            Example3_GotoWithType();
            Example4_GotoWithKind();

            Console.WriteLine("\n=== All Expression.Goto Overloads Demonstrated ===");
        }

        // 1️⃣ Overload 1: Goto(LabelTarget)
        static void Example1_BasicGoto()
        {
            Console.WriteLine("1️⃣ Basic Goto (no value):");

            var label = Expression.Label("MyLabel");
            var block = Expression.Block(
                Expression.Call(typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })!,
                                Expression.Constant("Before Goto")),
                Expression.Goto(label),
                Expression.Call(typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })!,
                                Expression.Constant("This line is skipped")),
                Expression.Label(label),
                Expression.Call(typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })!,
                                Expression.Constant("After Goto"))
            );

            var lambda = Expression.Lambda<Action>(block).Compile();
            lambda();

            Console.WriteLine();
        }

        // 2️⃣ Overload 2: Goto(LabelTarget, Expression value)
        static void Example2_GotoWithReturnValue()
        {
            Console.WriteLine("2️⃣ Goto with return value:");

            var target = Expression.Label(typeof(int), "ResultLabel");

            var block = Expression.Block(
                Expression.Goto(target, Expression.Constant(100)),
                Expression.Label(target, Expression.Constant(0)) // default return value
            );

            var lambda = Expression.Lambda<Func<int>>(block).Compile();
            Console.WriteLine("Result: " + lambda()); // Output: 100

            Console.WriteLine();
        }

        // 3️⃣ Overload 3: Goto(LabelTarget, Type type)
        static void Example3_GotoWithType()
        {
            Console.WriteLine("3️⃣ Goto with explicit type (no value):");

            var target = Expression.Label(typeof(void), "VoidLabel");

            var block = Expression.Block(
                Expression.Goto(target, typeof(void)),
                Expression.Label(target),
                Expression.Constant("Done") // final expression just to complete block
            );

            var lambda = Expression.Lambda<Func<string>>(block).Compile();
            Console.WriteLine("Result: " + lambda()); // Output: Done

            Console.WriteLine();
        }

        // 4️⃣ Overload 4: Goto(LabelTarget, Expression value, Type type)
        static void Example4_GotoWithKind()
        {
            Console.WriteLine("4️⃣ Goto with value and explicit type:");

            var target = Expression.Label(typeof(string), "StringLabel");

            var block = Expression.Block(
                Expression.Goto(target, Expression.Constant("Hello from Goto"), typeof(string)),
                Expression.Label(target, Expression.Constant("Default"))
            );

            var lambda = Expression.Lambda<Func<string>>(block).Compile();
            Console.WriteLine("Result: " + lambda()); // Output: Hello from Goto

            Console.WriteLine();
        }
    }
}
