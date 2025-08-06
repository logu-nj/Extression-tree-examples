using System;
using System.Linq.Expressions;

namespace ExpressionLoopExample
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Expression.Loop Examples ===\n");

            Example1_BasicLoop();
            Example2_LoopWithBreakCondition();
            Example3_LoopWithReturnValue();
 Console.WriteLine("=== Expression.Loop with Continue Example ===\n");
            Example_LoopWithContinue();
            Console.WriteLine("\n=== All Expression.Loop Overloads Demonstrated ===");
        }

        // 1️⃣ Example: Infinite loop with manual break (counter < 3)
        static void Example1_BasicLoop()
        {
            Console.WriteLine("1️⃣ Basic Loop Example:");

            var counter = Expression.Variable(typeof(int), "i");
            var breakLabel = Expression.Label("LoopBreak");

            var block = Expression.Block(
                new[] { counter },
                Expression.Assign(counter, Expression.Constant(0)),

                Expression.Loop(
                    Expression.Block(
                        Expression.Call(typeof(Console).GetMethod("WriteLine", new[] { typeof(int) })!, counter),
                        Expression.IfThen(
                            Expression.GreaterThanOrEqual(counter, Expression.Constant(2)),
                            Expression.Break(breakLabel)
                        ),
                        Expression.PostIncrementAssign(counter)
                    ),
                    breakLabel
                )
            );

            Expression.Lambda<Action>(block).Compile()();
            // Output:
            // 0
            // 1
            // 2
            Console.WriteLine();
        }

        // 2️⃣ Example: Loop with condition (like while loop)
        static void Example2_LoopWithBreakCondition()
        {
            Console.WriteLine("2️⃣ Loop with condition (while-like):");

            var counter = Expression.Variable(typeof(int), "n");
            var breakLabel = Expression.Label("LoopBreak");

            var block = Expression.Block(
                new[] { counter },
                Expression.Assign(counter, Expression.Constant(5)),

                Expression.Loop(
                    Expression.Block(
                        Expression.IfThen(
                            Expression.LessThanOrEqual(counter, Expression.Constant(0)),
                            Expression.Break(breakLabel)
                        ),
                        Expression.Call(typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })!, Expression.Constant("Counting down...")),
                        Expression.PostDecrementAssign(counter)
                    ),
                    breakLabel
                )
            );

            Expression.Lambda<Action>(block).Compile()();
            // Output:
            // Counting down... (5 times)
            Console.WriteLine();
        }

        // 3️⃣ Example: Loop with return value (break expression value)
        static void Example3_LoopWithReturnValue()
        {
            Console.WriteLine("3️⃣ Loop with return value:");

            var counter = Expression.Variable(typeof(int), "i");
            var breakLabel = Expression.Label(typeof(int), "ReturnLabel");

            var block = Expression.Block(
                new[] { counter },
                Expression.Assign(counter, Expression.Constant(0)),

                Expression.Loop(
                    Expression.Block(
                        Expression.IfThen(
                            Expression.GreaterThanOrEqual(counter, Expression.Constant(5)),
                            Expression.Break(breakLabel, counter) // returns counter
                        ),
                        Expression.PostIncrementAssign(counter)
                    ),
                    breakLabel
                )
            );

            var lambda = Expression.Lambda<Func<int>>(block).Compile();
            Console.WriteLine("Final count: " + lambda()); // Output: Final count: 5

            Console.WriteLine();
        }
         static void Example_LoopWithContinue()
        {
            var counter = Expression.Variable(typeof(int), "i");

            // Labels for controlling loop flow
            var breakLabel = Expression.Label("LoopBreak");
            var continueLabel = Expression.Label("LoopContinue");

            // WriteLine methods
            var writeLineInt = typeof(Console).GetMethod("WriteLine", new[] { typeof(int) })!;
            var writeLineStr = typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })!;

            // Loop body
            var loopBody = Expression.Block(
                Expression.IfThen(
                    Expression.GreaterThan(counter, Expression.Constant(10)),
                    Expression.Break(breakLabel) // stop loop if counter > 10
                ),

                // If counter is even, skip printing and go to next iteration
                Expression.IfThen(
                    Expression.Equal(Expression.Modulo(counter, Expression.Constant(2)), Expression.Constant(0)),
                    Expression.Block(
                        Expression.Call(writeLineStr, Expression.Constant("Even number skipped")),
                        Expression.PostIncrementAssign(counter),
                        Expression.Continue(continueLabel) // jump to start of loop
                    )
                ),

                // Otherwise, print the odd number
                Expression.Call(writeLineInt, counter),
                Expression.PostIncrementAssign(counter)
            );

            // Full block
            var block = Expression.Block(
                new[] { counter },
                Expression.Assign(counter, Expression.Constant(0)),

                Expression.Loop(loopBody, breakLabel, continueLabel)
            );

            Expression.Lambda<Action>(block).Compile()();

            // Output:
            // Even number skipped
            // 1
            // Even number skipped
            // 3
            // ...
        }
    }
}
