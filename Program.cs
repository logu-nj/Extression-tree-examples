
using System.Linq.Expressions;
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public void setAge(int age){
        Age = age;
    }
}
public class Program
{
    public static int AddExpression(int aa, int bb)
    {
        var a = Expression.Parameter(typeof(int), "");
        var b = Expression.Parameter(typeof(int), "");

        var AddOperation = Expression.Add(a, b);
        var AddFunction = Expression.Lambda<Func<int, int, int>>(AddOperation, a, b).Compile();
        return AddFunction(aa, bb);
    }

    public static bool isTrue(bool val)
    {
        var res = Expression.Constant(val);
        var valParameter = Expression.Parameter(typeof(bool), "");
        var isTrueValue = Expression.Lambda<Predicate<bool>>(res, valParameter).Compile()(val);
        return isTrueValue;
    }

    public static void AssignCheck()
    {
        // To demonstrate the assignment operation, we create a variable.
        ParameterExpression variableExpr = Expression.Variable(typeof(String), "sampleVar");

        // This expression represents the assignment of a value
        // to a variable expression.
        // It copies a value for value types, and
        // copies a reference for reference types.
        Expression assignExpr = Expression.Assign(variableExpr, Expression.Constant("Hello World!"));
        Console.WriteLine(assignExpr.ToString());
    }

    public static void BindValue()
    {
        // Property: Name
        var nameMember = typeof(Person).GetProperty("Name");
        var ageMethod = typeof(Person).GetMethod("setAge");

        var nameBind = Expression.Bind(nameMember, Expression.Constant("Logu")); // using PropertyInfo

    }

    public static void Main(String[] arg)
    {
        Console.WriteLine(AddExpression(10, 20));
        Console.WriteLine(isTrue(true));
        AssignCheck();
    }
}
