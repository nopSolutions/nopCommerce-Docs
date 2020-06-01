---
title: Coding Standards
uid: en/developer/tutorials/coding-standards
author: git.AndreiMaz
contributors: git.DmitriyKulagin
---

# Coding Standards

There are three supported .NET coding convention categories:

## Language conventions

### .NET code style settings

#### "this." qualifiers

This style rule can be applied to fields, properties, methods, or events.

- Prefer the code element *not* to be prefaced with `this.`
- Prefer fields *not* to be prefaced with `this.`

  ```csharp
  //Right
  capacity = 0;
  ```

  ```csharp
  //Wrong
  this.capacity = 0;
  ```

- Prefer properties *not* to be prefaced with `this.`

  ```csharp
  //Right
  ID = 0;
  ```

  ```csharp
  //Wrong
  this.ID = 0;
  ```

- Prefer methods *not* to be prefaced with `this.`

  ```csharp
  //Right
  Display();
  ```

  ```csharp
  //Wrong
  this.Display();
  ```

- Prefer events *not* to be prefaced with `this.`

  ```csharp
  //Right
  Elapsed += Handler;
  ```

  ```csharp
  //Wrong
  this.Elapsed += Handler;
  ```

#### Language keywords instead of framework type names for type references

This style rule can be applied to local variables, method parameters, and class members, or as a separate rule to type member access expressions.

- Prefer the language keyword for local variables, method parameters, and class members, instead of the type name, for types that have a keyword to represent them.

  ```csharp
  //Right
  private int _member;
  ```

  ```csharp
  //Wrong
  private Int32 _member;
  ```

- Prefer the language keyword for member access expressions, instead of the type name, for types that have a keyword to represent them.

  ```csharp
  //Right
  var local = int.MaxValue;
  ```

  ```csharp
  //Wrong
  var local = Int32.MaxValue;
  ```

#### Modifier preferences

The style rules in this section concern modifier preferences, including requiring accessibility modifiers, specifying the desired modifier sort order, and requiring the read-only modifier.

- Prefer accessibility modifiers to be declared except for public interface members.

  ```csharp
  //Right
  class MyClass
  {
      private const string thisFieldIsConst = "constant";
  }
  ```

  ```csharp
  //Wrong
  class MyClass
  {
      const string thisFieldIsConst = "constant";
  }
  ```

- Prefer the specified ordering:

	*`public, private, protected, internal, static, extern, new, virtual, abstract, sealed, override, readonly, unsafe, volatile, async:silent`*

  ```csharp
  //Right
  class MyClass
  {
      private static readonly int _daysInYear = 365;
  }
  ```

#### Parentheses preferences

The style rules in this section concern parentheses preferences, including the use of parentheses for arithmetic, relational, and other binary operators.

- Prefer parentheses to clarify arithmetic operator (*, /, %, +, -, <<, >>, &, ^, |) precedence

  ```csharp
  //Right
  var v = a + (b * c);
  ```

  ```csharp
  //Wrong
  var v = a + b * c;
  ```

- Prefer parentheses to clarify relational operator (>, <, <=, >=, is, as, ==, !=) precedence

  ```csharp
  //Right
  var v = (a < b) == (c > d);
  ```

  ```csharp
  //Wrong
  var v = a < b == c > d;
  ```

- Prefer parentheses to clarify other binary operator (&&, ||, ??) precedence

  ```csharp
  //Right
  var v = a || (b && c);
  ```

  ```csharp
  //Wrong
  var v = a || b && c;
  ```

- Prefer to not have parentheses when operator precedence is obvious

  ```csharp
  //Right
  var v = a.b.Length;
  ```

  ```csharp
  //Wrong
  var v = (a.b).Length;
  ```

#### Expression-level preferences

The style rules in this section concern expression-level preferences, including the use of object initializers, collection initializers, explicit or inferred tuple names, and inferred anonymous types.

- Prefer objects to be initialized using object initializers when possible

  ```csharp
  //Right
  var c = new Customer() { Age = 21 };
  ```

  ```csharp
  //Wrong
  var c = new Customer();
  c.Age = 21;
  ```

- Prefer collections to be initialized using collection initializers when possible

  ```csharp
  //Right
  var list = new List<int> { 1, 2, 3 };
  ```

  ```csharp
  //Wrong
  var list = new List<int>();
  list.Add(1);
  list.Add(2);
  list.Add(3);
  ```

- Prefer tuple names to ItemX properties

  ```csharp
  //Right
  (string name, int age) customer = GetCustomer();
  var name = customer.name;
  ```

  ```csharp
  //Wrong
  (string name, int age) customer = GetCustomer();
  var name = customer.Item1;
  ```

- Prefer inferred tuple element names

  ```csharp
  //Right
  var tuple = (age, name);
  ```

  ```csharp
  //Wrong
  var tuple = (age: age, name: name);
  ```

- Prefer explicit anonymous type member names

  ```csharp
  //Right
  var anon = new { age = age, name = name };
  ```

  ```csharp
  //Wrong
  var anon = new { age, name };
  ```

- Prefer autoproperties over properties with private backing fields

  ```csharp
  //Right
  private int Age { get; }
  ```

  ```csharp
  //Wrong
  private int age;

  public int Age
  {
      get
      {
          return age;
      }
  }
  ```

- Prefer using a null check with pattern-matching over *`object.ReferenceEquals`*

  ```csharp
  //Right
  if (value is null)
      return;
  ```

  ```csharp
  //Wrong
  if (object.ReferenceEquals(value, null))
      return;
  ```

- Prefer assignments with a ternary conditional over an if-else statement

  ```csharp
  //Right
  string s = expr ? "hello" : "world";
  ```

  ```csharp
  //Wrong
  string s;
  if (expr)
  {
      s = "hello";
  }
  else
  {
      s = "world";
  }
  ```

- Prefer return statements to use a ternary conditional over an if-else statement

  ```csharp
  //Right
  return expr ? "hello" : "world"
  ```

  ```csharp
  //Wrong
  if (expr)
  {
      return "hello";
  }
  else
  {
      return "world";
  }
  ```

- Prefer compound assignment expressions

  ```csharp
  //Right
  x += 1;
  ```

  ```csharp
  //Wrong
  x = x + 1;
  ```

#### Null-checking preferences

The style rules in this section concern null-checking preferences.

- Prefer null coalescing expressions to ternary operator checking

  ```csharp
  //Right
  var v = x ?? y;
  ```

  ```csharp
  //Wrong
  var v = x != null ? x : y; // or
  var v = x == null ? y : x;
  ```

- Prefer to use null-conditional operator when possible

  ```csharp
  //Right
  var v = o?.ToString();
  ```

  ```csharp
  //Wrong
  var v = o == null ? null : o.ToString(); // or
  var v = o != null ? o.String() : null;
  ```

### C# code style settings

#### Implicit and explicit types

The style rules in this section concern the use of the var keyword versus an explicit type in a variable declaration. This rule can be applied separately to built-in types, when the type is apparent, and elsewhere.

- Prefer *`var`* is used to declare variables with built-in system types such as *`int`*

  ```csharp
  //Right
  var x = 5;
  ```

  ```csharp
  //Wrong
  int x = 5;
  ```

- Prefer *`var`* when the type is already mentioned on the right-hand side of a declaration expression

  ```csharp
  //Right
  var obj = new Customer();
  ```

  ```csharp
  //Wrong
  Customer obj = new Customer();
  ```

- Prefer *`var`* over explicit type in all cases, unless overridden by another code style rule

  ```csharp
  //Right
  var f = this.Init();
  ```

  ```csharp
  //Wrong
  bool f = this.Init();
  ```

#### Expression-bodied members

The style rules in this section concern the use of [expression-bodied members](https://docs.microsoft.com/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members) when the logic consists of a single expression. This rule can be applied to methods, constructors, operators, properties, indexers, and accessors.

- Prefer block bodies for methods

  ```csharp
  //Right
  public int GetAge() { return this.Age; }
  ```

  ```csharp
  //Wrong
  public int GetAge() => this.Age;
  ```

- Prefer block bodies for constructors

  ```csharp
  //Right
  public Customer(int age) { Age = age; }
  ```

  ```csharp
  //Wrong
  public Customer(int age) => Age = age;
  ```

- Prefer block bodies for operators

  ```csharp
  //Right
  public static ComplexNumber operator + (ComplexNumber c1, ComplexNumber c2)
  { return new ComplexNumber(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary); }
  ```

  ```csharp
  //Wrong
  public static ComplexNumber operator + (ComplexNumber c1, ComplexNumber c2)
      => new ComplexNumber(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
  ```

- Prefer expression bodies for properties when they will be a single line

  ```csharp
  //Right
  public int Age => _age;
  ```

  ```csharp
  //Wrong
  public int Age { get { return _age; }}
  ```

- Prefer expression bodies for indexers

  ```csharp
  //Right
  public T this[int i] => _values[i];
  ```

  ```csharp
  //Wrong
  public T this[int i] { get { return _values[i]; } }
  ```

- Prefer expression bodies for accessors

  ```csharp
  //Right
  public int Age { get => _age; set => _age = value; }
  ```

  ```csharp
  //Wrong
  public int Age { get { return _age; } set { _age = value; } }
  ```

- Prefer expression bodies for lambdas

  ```csharp
  //Right
  Func<int, int> square = x => x * x;
  ```

  ```csharp
  //Wrong
  Func<int, int> square = x => { return x * x; };
  ```

#### Pattern matching

The style rules in this section concern the use of [pattern matching](https://docs.microsoft.com/dotnet/csharp/pattern-matching) in C#.

- Prefer pattern matching instead of is expressions with type casts

  ```csharp
  //Right
  if (o is int i) {...}
  ```

  ```csharp
  //Wrong
  if (o is int) {var i = (int)o; ... }
  ```

- Prefer pattern matching instead of *`as`* expressions with null checks to determine if something is of a particular type

  ```csharp
  //Right
  if (o is string s) {...}
  ```

  ```csharp
  //Wrong
  var s = o as string;
  if (s != null) {...}
  ```

#### Inlined variable declarations

This style rule concerns whether out variables are declared inline or not. Starting in C# 7, you can [declare an out variable in the argument list of a method call](https://docs.microsoft.com/dotnet/csharp/language-reference/keywords/out-parameter-modifier#calling-a-method-with-an-out-argument), rather than in a separate variable declaration.

- Prefer *`out`* variables to be declared inline in the argument list of a method call when possible

  ```csharp
  //Right
  if (int.TryParse(value, out int i) {...}
  ```

  ```csharp
  //Wrong
  int i;
  if (int.TryParse(value, out i) {...}
  ```

#### C# expression-level preferences

This style rule concerns using the [default literal for default value expressions](https://docs.microsoft.com/dotnet/csharp/programming-guide/statements-expressions-operators/default-value-expressions#default-literal-and-type-inference) when the compiler can infer the type of the expression.

- Prefer *`default`* over *`default(T)`*

  ```csharp
  //Right
  void DoWork(CancellationToken cancellationToken = default) { ... }
  ```

  ```csharp
  //Wrong
  void DoWork(CancellationToken cancellationToken = default(CancellationToken)) {   ... }
  ```

#### C# null-checking preferences

These style rules concern the syntax around null checking, including using throw expressions or throw statements, and whether to perform a null check or use the conditional coalescing operator (?.) when invoking a [lambda expression](https://docs.microsoft.com/dotnet/csharp/lambda-expressions).

- Prefer to use throw expressions instead of throw statements

  ```csharp
  //Right
  this.s = s ?? throw new ArgumentNullException(nameof(s));
  ```

  ```csharp
  //Wrong
  if (s == null) { throw new ArgumentNullException(nameof(s)); }
  this.s = s;
  ```

- Refer to use the conditional coalescing operator (?.) when invoking a lambda expression, instead of performing a null check

  ```csharp
  //Right
  func?.Invoke(args);
  ```

  ```csharp
  //Wrong
  if (func != null) { func(args); }
  ```

#### Code block preferences

This style rule concerns the use of curly braces { } to surround code blocks.

- Prefer no curly braces if allowed

  ```csharp
  //Right
  if (test) this.Display();
  ```

  ```csharp
  //Wrong
  if (test) { this.Display(); }
  ```

## Formatting conventions

### .NET formatting settings

### Organize using directives

These formatting rules concern the sorting and display of *`using`* directives and *`Imports`* statements.

- Sort System.* *`using`* directives alphabetically, and place them before other using directives.

  ```csharp
  //Right
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Octokit;
  ```

  ```csharp
  //Wrong
  using System.Collections.Generic;
  using Octokit;
  using System.Threading.Tasks;
  ```

- Do not place a blank line between using directive groups.

  ```csharp
  //Right
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Octokit;
  ```

  ```csharp
  //Wrong
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Octokit;
  ```

### C# formatting settings

The formatting rules in this section apply only to C# code.

#### New-line options

These formatting rules concern the use of new lines to format code.

- Require braces to be on a new line for all expressions ("Allman" style).

  ```csharp
  //Right
  void MyMethod()
  {
      if (...)
      {
          ...
      }
  }
  ```

  ```csharp
  //Wrong
  void MyMethod() {
      if (...) {
          ...
      }
  }
  ```

- Place else statements on a new line.

  ```csharp
  //Right
  if (...) {
      ...
  }
  else {
      ...
  }
  ```

  ```csharp
  //Wrong
  if (...) {
      ...
  } else {
      ...
  }
  ```

- Place catch statements on a new line.

  ```csharp
  //Right
  try {
      ...
  }
  catch (Exception e) {
      ...
  }
  ```

  ```csharp
  //Wrong
  try {
      ...
  } catch (Exception e) {
      ...
  }
  ```

- Require finally statements to be on a new line after the closing brace.

  ```csharp
  //Right
  try {
      ...
  }
  catch (Exception e) {
      ...
  }
  finally {
      ...
  }
  ```

  ```csharp
  //Wrong
  try {
      ...
  } catch (Exception e) {
      ...
  } finally {
      ...
  }
  ```

- Require members of object initializers to be on separate lines

  ```csharp
  //Right
  var z = new B()
  {
      A = 3,
      B = 4
  }
  ```

  ```csharp
  //Wrong
  var z = new B()
  {
      A = 3, B = 4
  }
  ```

- Require members of anonymous types to be on separate lines

  ```csharp
  //Right
  var z = new
  {
      A = 3,
      B = 4
  }
  ```

  ```csharp
  //Wrong
  var z = new
  {
      A = 3, B = 4
  }
  ```

- Require elements of query expression clauses to be on separate lines

  ```csharp
  //Right
  var q = from a in e
          from b in e
          select a * b;
  ```

  ```csharp
  //Wrong
  var q = from a in e from b in e
          select a * b;
  ```

#### Indentation options

These formatting rules concern the use of indentation to format code.

- Indent *`switch`* case contents

  ```csharp
  //Right
  switch(c) 
  {
      case Color.Red:
          Console.WriteLine("The color is red");
          break;
      case Color.Blue:
          Console.WriteLine("The color is blue");
          break;
      default:
          Console.WriteLine("The color is unknown.");
          break;
  }
  ```

  ```csharp
  //Wrong
  switch(c) {
      case Color.Red:
      Console.WriteLine("The color is red");
      break;
      case Color.Blue:
      Console.WriteLine("The color is blue");
      break;
      default:
      Console.WriteLine("The color is unknown.");
      break;
  }
  ```

- Indent *`switch`* labels

  ```csharp
  //Right
  switch(c) 
  {
      case Color.Red:
          Console.WriteLine("The color is red");
          break;
      case Color.Blue:
          Console.WriteLine("The color is blue");
          break;
      default:
          Console.WriteLine("The color is unknown.");
          break;
  }
  ```

  ```csharp
  //Wrong
  switch(c) {
  case Color.Red:
      Console.WriteLine("The color is red");
      break;
  case Color.Blue:
      Console.WriteLine("The color is blue");
      break;
  default:
      Console.WriteLine("The color is unknown.");
      break;
  }
  ```

- Labels are placed at the same indent as the current context

  ```csharp
  //Right
  class C
  {
      private string MyMethod(...)
      {          
          if (...) 
          {
              goto error;
          }
          error:
          throw new Exception(...);
      }
  }
  ```

  ```csharp
  //Wrong
  class C
  {
      private string MyMethod(...)
      {
          if (...) {
              goto error;
          }
  error:
          throw new Exception(...);
      }
  }
  ```

  ```csharp
  //Wrong
  class C
  {
      private string MyMethod(...)
      {
          if (...) {
              goto error;
          }
      error:
          throw new Exception(...);
      }
  }
  ```

#### Spacing options

These formatting rules concern the use of space characters to format code.

- Remove space between the cast and the value

  ```csharp
  //Right
  int y = (int)x;
  ```

  ```csharp
  //Wrong
  int y = (int) x;
  ```

- Place a space character after a keyword in a control flow statement such as a   *`for`* loop

  ```csharp
  //Right
  for (int i;i<x;i++) { ... }
  ```

  ```csharp
  //Wrong
  for(int i;i<x;i++) { ... }
  ```

- Place a space character before the colon for bases or interfaces in a type   declaration

  ```csharp
  //Right
  interface I
  {

  }

  class C : I
  {

  }
  ```

  ```csharp
  //Wrong
  interface I
  {

  }

  class C: I
  {

  }
  ```

- Place a space character after the colon for bases or interfaces in a type   declaration

  ```csharp
  //Right
  interface I
  {

  }

  class C : I
  {

  }
  ```

  ```csharp
  //Wrong
  interface I
  {

  }

  class C :I
  {

  }
  ```

- Insert space before and after the binary operator

  ```csharp
  //Right
  return x * (x - y);
  ```

  ```csharp
  //Wrong
  return x*(x-y);
  ```

  ```csharp
  //Wrong
  return x  *  (x-y);
  ```

- Remove space characters after the opening parenthesis and before the closing   parenthesis of a method declaration parameter list

  ```csharp
  //Right
  void Bark(int x) { ... }
  ```

  ```csharp
  //Wrong
  void Bark( int x ) { ... }
  ```

- Remove space within empty parameter list parentheses for a method declaration

  ```csharp
  //Right
  void Goo()
  {
      Goo(1);
  }

  void Goo(int x)
  {
      Goo();
  }
  ```

  ```csharp
  //Wrong
  void Goo( )
  {
      Goo(1);
  }

  void Goo(int x)
  {
      Goo();
  }

  ```

- Remove space characters between the method name and opening parenthesis in the   method declaration

  ```csharp
  //Right
  void M() { }
  ```

  ```csharp
  //Wrong
  void M () { }
  ```

- Remove space characters after the opening parenthesis and before the closing   parenthesis of a method call

  ```csharp
  //Right
  MyMethod(argument);
  ```

  ```csharp
  //Wrong
  MyMethod( argument );
  ```

- Remove space within empty argument list parentheses

  ```csharp
  //Right
  void Goo()
  {
      Goo(1);
  }

  void Goo(int x)
  {
      Goo();
  }
  ```

  ```csharp
  //Wrong
  void Goo()
  {
      Goo(1);
  }

  void Goo(int x)
  {
      Goo( );
  }
  ```

- Remove space between method call name and opening parenthesis

  ```csharp
  //Right
  void Goo()
  {
      Goo(1);
  }

  void Goo(int x)
  {
      Goo();
  }
  ```

  ```csharp
  //Wrong
  void Goo()
  {
      Goo(1);
  }

  void Goo(int x)
  {
      Goo ();
  }
  ```

- Insert space after a comma

  ```csharp
  //Right
  int[] x = new int[] { 1, 2, 3, 4, 5 };
  ```

  ```csharp
  //Wrong
  int[] x = new int[] { 1,2,3,4,5 }
  ```

- Remove space before a comma

  ```csharp
  //Right
  int[] x = new int[] { 1, 2, 3, 4, 5 };
  ```

  ```csharp
  //Wrong
  int[] x = new int[] { 1 , 2 , 3 , 4 , 5 };
  ```

- Insert space after each semicolon in a for statement

  ```csharp
  //Right
  for (int i = 0; i < x.Length; i++)
  ```

  ```csharp
  //Wrong
  for (int i = 0;i < x.Length;i++)
  ```

- Remove space before each semicolon in a for statement

  ```csharp
  //Right
  for (int i = 0; i < x.Length; i++)
  ```

  ```csharp
  //Wrong
  for (int i = 0 ; i < x.Length ; i++)
  ```

- Remove extra space characters in declaration statements

  ```csharp
  //Right
  int x = 0;
  ```

  ```csharp
  //Wrong
  int    x    =    0   ;
  ```

- Remove space before opening square brackets *`[`*

  ```csharp
  //Right
  int[] numbers = new int[] { 1, 2, 3, 4, 5 };
  ```

  ```csharp
  //Wrong
  int [] numbers = new int [] { 1, 2, 3, 4, 5 };
  ```

- Remove space between empty square brackets *`[]`*

  ```csharp
  //Right
  int[] numbers = new int[] { 1, 2, 3, 4, 5 };
  ```

  ```csharp
  //Wrong
  int[ ] numbers = new int[ ] { 1, 2, 3, 4, 5 };
  ```

- Remove space characters in non-empty square brackets *`[0]`*

  ```csharp
  //Right
  int index = numbers[0];
  ```

  ```csharp
  //Wrong
  int index = numbers[ 0 ];
  ```

#### Wrap options

These formatting rules concern the use of single lines versus separate lines for statements and code blocks.

- Leave statements and member declarations on different lines

  ```csharp
  //Right
  int i = 0;
  string name = "John";
  ```

  ```csharp
  //Wrong
  int i = 0; string name = "John";
  ```

- Leave code block on single line

  ```csharp
  //Right
  public int Foo { get; set; }
  ```

  ```csharp
  //Wrong
  public int MyProperty
  {
      get; set;
  }
  ```

## Naming conventions

- Constants are named only in capital letters with a delimiter *`_`*

  ```csharp
  //Right
  const int TEST_CONSTANT = 1;
  ```

  ```csharp
  //Wrong
  const int Test_Constant = 1;
  ```

- Fields with *`public`* access are referred to as PascalCase notation

  ```csharp
  //Right
  public int TestField;
  ```

  ```csharp
  //Wrong
  public int testField;
  ```

- Interface names must be in PascalCase notation and have the prefix *`I`*

  ```csharp
  //Right
  public interface ITestInterface;
  ```

  ```csharp
  //Wrong
  public interface testInterface;
  ```

- The names of classes, structures, methods, enums, events, properties,   namespaces, and delegates should be in PascalCase notation

  ```csharp
  //Right
  public class SomeClass;
  ```

  ```csharp
  //Wrong
  public class someClass;
  ```

- Assigned to the parameter of a generic type a descriptive name in the notation   PascalCase, unless enough of a letter and a descriptive name has no practical   value

  ```csharp
  //Right
  public interface ISessionChannel<TSession> { /*...*/ }
  public delegate TOutput Converter<TInput, TOutput>(TInput from);
  public class List<T> { /*...*/ }
  ```

- Use the name of the type *`T`* parameter for types that contain only a single letter type parameter

  ```csharp
  //Right
  public int IComparer<T>() { return 0; }
  public delegate bool Predicate<T>(T item);
  public struct Nullable<T> where T : struct { /*...*/ }
  ```

- Use the prefix *`T`* for descriptive names of type parameters

  ```csharp
  //Right
  public interface ISessionChannel<TSession>
  {
      TSession Session { get; }
  }
  ```

  Specify the constraints associated with the type parameter in its name. For example, an *`ISession`* constraint parameter may be called *`TSession`*.

- Private and protected class fields must begin with the prefix *`_`*

  ```csharp
  //Right
  private int _testField;
  protected int _testField;
  ```

  ```csharp
  //Wrong
  private int testField;
  protected int testField;
  ```

- All other code elements such as variables, method parameters and class fields (except open ones) are named in camelCase notation.

  ```csharp
  //Right
  var testVar = new Object();
  public void Foo(int firstParam, string secondParam)
  ```

  ```csharp
  //Wrong
  var TestVar = new Object();
  public void Foo(int FirstParam, string SecondParam)
  ```
