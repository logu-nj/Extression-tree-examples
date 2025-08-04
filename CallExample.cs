using System.Linq.Expressions;

public class Calculator
{
    public int Add(int a, int b) => a + b;
}

public class GenericClass
{
    public T Echo<T>(T value) => value;
}

public static class MathUtil
{
    public static int Square(int x) => x * x;
}

class Program2
{
    static void Main1()
    {
        Console.WriteLine("=== Expression.Call Examples ===\n");

        Example1_InstanceMethod_NoParams();
        Example2_InstanceMethod_WithParams();
        Example3_StaticMethod();
        Example4_MethodByName_GenericMethod();
        Example5_ParameterlessInstanceMethod();

        Console.WriteLine("\n=== All Expression.Call Overloads Demonstrated ===");
    }

    // 1️⃣ Overload 1: Call(Expression, MethodInfo)
    static void Example1_InstanceMethod_NoParams()
    {
        Console.WriteLine("1️⃣ Instance Method (No Parameters):");

        var instance = Expression.Parameter(typeof(string), "s");
        var method = typeof(string).GetMethod("ToUpper", Type.EmptyTypes)!;

        var callExpr = Expression.Call(instance, method);
        var lambda = Expression.Lambda<Func<string, string>>(callExpr, instance).Compile();

        Console.WriteLine(lambda("hello")); // Output: HELLO
        Console.WriteLine();
    }

    // 2️⃣ Overload 2: Call(Expression, MethodInfo, Expression[])
    static void Example2_InstanceMethod_WithParams()
    {
        Console.WriteLine("2️⃣ Instance Method (With Parameters):");

        var instance = Expression.Parameter(typeof(Calculator), "calc");
        var method = typeof(Calculator).GetMethod("Add")!;

        var callExpr = Expression.Call(instance, method,
            Expression.Constant(5),
            Expression.Constant(10)
        );

        var lambda = Expression.Lambda<Func<Calculator, int>>(callExpr, instance).Compile();
        Console.WriteLine(lambda(new Calculator())); // Output: 15
        Console.WriteLine();
    }

    // 3️⃣ Overload 3: Call(MethodInfo, Expression[])
    static void Example3_StaticMethod()
    {
        Console.WriteLine("3️⃣ Static Method:");

        var method = typeof(MathUtil).GetMethod("Square")!;
        var callExpr = Expression.Call(method, Expression.Constant(6));

        var lambda = Expression.Lambda<Func<int>>(callExpr).Compile();
        Console.WriteLine(lambda()); // Output: 36
        Console.WriteLine();
    }

    // 4️⃣ Overload 4: Call(Expression, string, Type[], Expression[])
    static void Example4_MethodByName_GenericMethod()
    {
        Console.WriteLine("4️⃣ Method By Name (Generic Method):");

        var instance = Expression.Parameter(typeof(GenericClass), "g");

        var callExpr = Expression.Call(
            instance,
            "Echo",
            new Type[] { typeof(int) },
            Expression.Constant(42)
        );

        var lambda = Expression.Lambda<Func<GenericClass, int>>(callExpr, instance).Compile();
        Console.WriteLine(lambda(new GenericClass())); // Output: 42
        Console.WriteLine();
    }

    // 5️⃣ Overload 5: Call(Expression, MethodInfo) (Parameterless Instance Method Example)
    static void Example5_ParameterlessInstanceMethod()
    {
        Console.WriteLine("5️⃣ Parameterless Instance Method:");

        var instance = Expression.Parameter(typeof(MyClass), "obj");
        var method = typeof(MyClass).GetMethod("Hello")!;

        var callExpr = Expression.Call(instance, method);
        var lambda = Expression.Lambda<Func<MyClass, string>>(callExpr, instance).Compile();

        Console.WriteLine(lambda(new MyClass())); // Output: Hello, world!
        Console.WriteLine();
    }
}

public class MyClass
{
    public string Hello() => "Hello, world!";
}