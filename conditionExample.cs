using System.Linq.Expressions;

class Program4
{
    static void Main76()
    {
        Console.WriteLine("=== Expression.Condition Examples ===\n");

        Example1_BasicCondition();
        Example2_ConditionWithExplicitType();

        Console.WriteLine("\n=== All Expression.Condition Overloads Demonstrated ===");
    }

    // 1️⃣ Overload 1: Condition(Expression test, Expression ifTrue, Expression ifFalse)
    static void Example1_BasicCondition()
    {
        Console.WriteLine("1️⃣ Basic Condition (bool test):");

        // test: 5 > 3
        var test = Expression.GreaterThan(Expression.Constant(5), Expression.Constant(3));

        // ifTrue: "Yes", ifFalse: "No"
        var ifTrue = Expression.Constant("Yes");
        var ifFalse = Expression.Constant("No");

        var condition = Expression.Condition(test, ifTrue, ifFalse);

        var lambda = Expression.Lambda<Func<string>>(condition).Compile();
        Console.WriteLine("Result: " + lambda()); // Output: Yes
        Console.WriteLine();
    }

    // 2️⃣ Overload 2: Condition(Expression test, Expression ifTrue, Expression ifFalse, Type type)
    static void Example2_ConditionWithExplicitType()
    {
        Console.WriteLine("2️⃣ Condition with Explicit Return Type:");

        // test: 10 < 3
        var test = Expression.LessThan(Expression.Constant(10), Expression.Constant(3));

        // ifTrue: 100, ifFalse: 200
        var ifTrue = Expression.Constant(100);
        var ifFalse = Expression.Constant(200);

        // Explicit return type = int
        var condition = Expression.Condition(test, ifTrue, ifFalse, typeof(int));

        var lambda = Expression.Lambda<Func<int>>(condition).Compile();
        Console.WriteLine("Result: " + lambda()); // Output: 200
        Console.WriteLine();
    }

    // 3️⃣ Overload 3: Different branch types (explicit common type required)
    static void Example3_ConditionWithDifferentReturnTypes()
    {
        Console.WriteLine("3️⃣ Condition with Different Branch Types (forced type):");
        var test = Expression.Constant(true);

        var ifTrue = Expression.Convert(Expression.Constant(123), typeof(object));
        var ifFalse = Expression.Convert(Expression.Constant(45.6), typeof(object));

        var condition = Expression.Condition(test, ifTrue, ifFalse, typeof(object));

        var lambda = Expression.Lambda<Func<object>>(condition).Compile();
        Console.WriteLine("Result: " + lambda());  // ✅ Output: 123
    }
}