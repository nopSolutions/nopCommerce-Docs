---
標題: 編碼標準
uid: zh-Hant/developer/tutorials/coding-standards
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin
---

# 編碼標準
nopCommerce 擁有特定的編碼準則，開發人員在建立原始程式碼時應遵循這些準則。Visual Studio 中的 [.editorconfig](https://github.com/nopSolutions/nopCommerce/blob/develop/.editorconfig) 檔案有助於強制執行所需的程式碼風格。

共有三類支援的 .NET 編碼慣例：
- 語言慣例
- 格式慣例
- 命名慣例

## 語言慣例

### .NET 程式碼風格設定

#### "this." 限定詞

此風格規則適用於欄位、屬性、方法或事件。

- 偏好程式碼元素*不*使用 `this.` 作為前綴。
- 偏好欄位*不*使用 `this.` 作為前綴。

  ```csharp
  //正確
  capacity = 0;
  ```

  ```csharp
  //錯誤
  this.capacity = 0;
  ```

- 偏好屬性*不*使用 `this.` 作為前綴。

  ```csharp
  //正確
  ID = 0;
  ```

  ```csharp
  //錯誤
  this.ID = 0;
  ```

- 偏好方法*不*使用 `this.` 作為前綴。

  ```csharp
  //正確
  Display();
  ```

  ```csharp
  //錯誤
  this.Display();
  ```

- 偏好事件*不*使用 `this.` 作為前綴。

  ```csharp
  //正確
  Elapsed += Handler;
  ```

  ```csharp
  //錯誤
  this.Elapsed += Handler;
  ```

#### 使用語言關鍵字而非框架型別名稱來參照型別

此風格規則適用於區域變數、方法參數、類別成員，或是作為型別成員存取運算式的單獨規則。

- 對於有對應語言關鍵字的型別，在區域變數、方法參數及類別成員中，偏好使用語言關鍵字而非型別名稱。

  ```csharp
  //正確
  private int _member;
  ```

  ```csharp
  //錯誤
  private Int32 _member;
  ```

- 對於有對應語言關鍵字的型別，在成員存取運算式中，偏好使用語言關鍵字而非型別名稱。

  ```csharp
  //正確
  var local = int.MaxValue;
  ```

  ```csharp
  //錯誤
  var local = Int32.MaxValue;
  ```

#### 修飾詞偏好

本節中的風格規則涉及修飾詞偏好，包括要求存取修飾詞、指定所需的修飾詞排序，以及要求唯讀 (read-only) 修飾詞。

- 除了公開介面成員外，偏好宣告存取修飾詞。

  ```csharp
  //正確
  class MyClass
  {
      private const string thisFieldIsConst = "constant";
  }
  ```

  ```csharp
  //錯誤
  class MyClass
  {
      const string thisFieldIsConst = "constant";
  }
  ```

- 偏好指定的排序：

    *`public, private, protected, internal, static, extern, new, virtual, abstract, sealed, override, readonly, unsafe, volatile, async:silent`*

  ```csharp
  //正確
  class MyClass
  {
      private static readonly int _daysInYear = 365;
  }
  ```

#### 括號偏好

本節中的風格規則涉及括號偏好，包括針對算術、關係及其他二元運算子使用括號。

- 偏好使用括號來釐清算術運算子 (*, /, %, +, -, <<, >>, &, ^, |) 的優先順序

  ```csharp
  //正確
  var v = a + (b * c);
  ```

  ```csharp
  //錯誤
  var v = a + b * c;
  ```

- 偏好使用括號來釐清關係運算子 (>, <, <=, >=, is, as, ==, !=) 的優先順序

  ```csharp
  //正確
  var v = (a < b) == (c > d);
  ```

  ```csharp
  //錯誤
  var v = a < b == c > d;
  ```

- 偏好使用括號來釐清其他二元運算子 (&&, ||, ??) 的優先順序

  ```csharp
  //正確
  var v = a || (b && c);
  ```

  ```csharp
  //錯誤
  var v = a || b && c;
  ```

- 當運算子優先順序顯而易見時，偏好不使用括號

  ```csharp
  //正確
  var v = a.b.Length;
  ```

  ```csharp
  //錯誤
  var v = (a.b).Length;
  ```

#### 運算式層級偏好

本節中的風格規則涉及運算式層級偏好，包括使用物件初始化器、集合初始化器、明確或推斷的 Tuple 名稱，以及推斷的匿名型別。

- 盡可能偏好使用物件初始化器來初始化物件

  ```csharp
  //正確
  var c = new Customer() { Age = 21 };
  ```

  ```csharp
  //錯誤
  var c = new Customer();
  c.Age = 21;
  ```

- 盡可能偏好使用集合初始化器來初始化集合

  ```csharp
  //正確
  var list = new List<int> { 1, 2, 3 };
  ```

  ```csharp
  //錯誤
  var list = new List<int>();
  list.Add(1);
  list.Add(2);
  list.Add(3);
  ```

- 偏好使用 Tuple 名稱而非 ItemX 屬性

  ```csharp
  //正確
  (string name, int age) customer = GetCustomer();
  var name = customer.name;
  ```

  ```csharp
  //錯誤
  (string name, int age) customer = GetCustomer();
  var name = customer.Item1;
  ```

- 偏好推斷 Tuple 元素名稱

  ```csharp
  //正確
  var tuple = (age, name);
  ```

  ```csharp
  //錯誤
  var tuple = (age: age, name: name);
  ```

- 偏好明確的匿名型別成員名稱

  ```csharp
  //正確
  var anon = new { age = age, name = name };
  ```

  ```csharp
  //錯誤
  var anon = new { age, name };
  ```

- 偏好使用自動屬性 (Auto-properties) 而非具私有備份欄位的屬性

  ```csharp
  //正確
  private int Age { get; }
  ```

  ```csharp
  //錯誤
  private int age;

  public int Age
  {
      get
      {
          return age;
      }
  }
  ```

- 偏好使用模式比對的 null 檢查而非 *`object.ReferenceEquals`*

  ```csharp
  //正確
  if (value is null)
      return;
  ```

  ```csharp
  //錯誤
  if (object.ReferenceEquals(value, null))
      return;
  ```

- 偏好使用三元條件運算式進行賦值，而非 if-else 陳述式

  ```csharp
  //正確
  string s = expr ? "hello" : "world";
  ```

  ```csharp
  //錯誤
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

- 偏好在 return 陳述式中使用三元條件運算式，而非 if-else 陳述式

  ```csharp
  //正確
  return expr ? "hello" : "world";
  ```

  ```csharp
  //錯誤
  if (expr)
  {
      return "hello";
  }
  else
  {
      return "world";
  }
  ```

- 偏好使用複合賦值運算式

  ```csharp
  //正確
  x += 1;
  ```

  ```csharp
  //錯誤
  x = x + 1;
  ```

#### Null 檢查偏好

本節中的風格規則涉及 null 檢查偏好。

- 偏好使用 null 合併運算式而非三元運算子檢查

  ```csharp
  //正確
  var v = x ?? y;
  ```

  ```csharp
  //錯誤
  var v = x != null ? x : y; // 或
  var v = x == null ? y : x;
  ```

- 盡可能偏好使用 null 條件運算子

  ```csharp
  //正確
  var v = o?.ToString();
  ```

  ```csharp
  //錯誤
  var v = o == null ? null : o.ToString(); // 或
  var v = o != null ? o.String() : null;
  ```

### C# 程式碼風格設定

#### 隱含與明確型別

本節中的風格規則涉及在變數宣告中使用 var 關鍵字與明確型別。此規則可分別應用於內建型別、型別顯而易見的情況以及其他處。

- 偏好使用 *`var`* 來宣告具內建系統型別（如 *`int`*）的變數

  ```csharp
  //正確
  var x = 5;
  ```

  ```csharp
  //錯誤
  int x = 5;
  ```

- 當宣告運算式的右側已提及型別時，偏好使用 *`var`*

  ```csharp
  //正確
  var obj = new Customer();
  ```

  ```csharp
  //錯誤
  Customer obj = new Customer();
  ```

- 除非被其他程式碼風格規則覆寫，否則在所有情況下均偏好使用 *`var`* 而非明確型別

  ```csharp
  //正確
  var f = this.Init();
  ```

  ```csharp
  //錯誤
  bool f = this.Init();
  ```

#### 運算式主體成員

本節中的風格規則涉及當邏輯由單一運算式組成時，使用 [運算式主體成員 (expression-bodied members)](https://docs.microsoft.com/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members)。此規則可應用於方法、建構函式、運算子、屬性、索引子及存取子。

- 偏好為方法使用區塊主體

  ```csharp
  //正確
  public int GetAge() { return this.Age; }
  ```

  ```csharp
  //錯誤
  public int GetAge() => this.Age;
  ```

- 偏好為建構函式使用區塊主體

  ```csharp
  //正確
  public Customer(int age) { Age = age; }
  ```

  ```csharp
  //錯誤
  public Customer(int age) => Age = age;
  ```

- 偏好為運算子使用區塊主體

  ```csharp
  //正確
  public static ComplexNumber operator + (ComplexNumber c1, ComplexNumber c2)
  { return new ComplexNumber(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary); }
  ```

  ```csharp
  //錯誤
  public static ComplexNumber operator + (ComplexNumber c1, ComplexNumber c2)
      => new ComplexNumber(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
  ```

- 當屬性只有一行時，偏好使用運算式主體

  ```csharp
  //正確
  public int Age => _age;
  ```

  ```csharp
  //錯誤
  public int Age { get { return _age; }}
  ```

- 偏好為索引子使用運算式主體

  ```csharp
  //正確
  public T this[int i] => _values[i];
  ```

  ```csharp
  //錯誤
  public T this[int i] { get { return _values[i]; } }
  ```

- 偏好為存取子使用運算式主體

  ```csharp
  //正確
  public int Age { get => _age; set => _age = value; }
  ```

  ```csharp
  //錯誤
  public int Age { get { return _age; } set { _age = value; } }
  ```

- 偏好為 Lambda 使用運算式主體

  ```csharp
  //正確
  Func<int, int> square = x => x * x;
  ```

  ```csharp
  //錯誤
  Func<int, int> square = x => { return x * x; };
  ```

#### 模式比對

本節中的風格規則涉及 C# 中 [模式比對 (pattern matching)](https://docs.microsoft.com/dotnet/csharp/pattern-matching) 的使用。

- 偏好使用模式比對而非帶有型別轉換的 is 運算式

  ```csharp
  //正確
  if (o is int i) {...}
  ```

  ```csharp
  //錯誤
  if (o is int) {var i = (int)o; ... }
  ```

- 偏好使用模式比對而非帶有 null 檢查的 *`as`* 運算式，以判斷某個物件是否為特定型別

  ```csharp
  //正確
  if (o is string s) {...}
  ```

  ```csharp
  //錯誤
  var s = o as string;
  if (s != null) {...}
  ```

#### 內嵌變數宣告

此風格規則涉及 out 變數是否應內嵌宣告。從 C# 7 開始，您可以 [在方法呼叫的引數列表中宣告 out 變數](https://docs.microsoft.com/dotnet/csharp/language-reference/keywords/out-parameter-modifier#calling-a-method-with-an-out-argument)，而非在單獨的變數宣告中進行。

- 盡可能偏好將 *`out`* 變數內嵌宣告在方法呼叫的引數列表中

  ```csharp
  //正確
  if (int.TryParse(value, out int i)) {...}
  ```

  ```csharp
  //錯誤
  int i;
  if (int.TryParse(value, out i)) {...}
  ```

#### C# 運算式層級偏好

此風格規則涉及當編譯器可以推斷運算式的型別時，使用 [預設值運算式的 default 常值 (default literal)](https://docs.microsoft.com/dotnet/csharp/programming-guide/statements-expressions-operators/default-value-expressions#default-literal-and-type-inference)。

- 偏好使用 *`default`* 而非 *`default(T)`*

  ```csharp
  //正確
  void DoWork(CancellationToken cancellationToken = default) { ... }
  ```

  ```csharp
  //錯誤
  void DoWork(CancellationToken cancellationToken = default(CancellationToken)) {   ... }
  ```

#### C# Null 檢查偏好

這些風格規則涉及 null 檢查的語法，包括使用 throw 運算式或 throw 陳述式，以及在呼叫 [Lambda 運算式](https://docs.microsoft.com/dotnet/csharp/lambda-expressions) 時，是執行 null 檢查還是使用條件合併運算子 (?.)。

- 偏好使用 throw 運算式而非 throw 陳述式

  ```csharp
  //正確
  this.s = s ?? throw new ArgumentNullException(nameof(s));
  ```

  ```csharp
  //錯誤
  if (s == null) { throw new ArgumentNullException(nameof(s)); }
  this.s = s;
  ```

- 偏好在呼叫 Lambda 運算式時使用條件合併運算子 (?.)，而非執行 null 檢查

  ```csharp
  //正確
  func?.Invoke(args);
  ```

  ```csharp
  //錯誤
  if (func != null) { func(args); }
  ```

#### 程式碼區塊偏好

此風格規則涉及使用大括號 { } 來包含程式碼區塊。

- 若允許，偏好不使用大括號

  ```csharp
  //正確
  if (test) this.Display();
  ```

  ```csharp
  //錯誤
  if (test) { this.Display(); }
  ```

## 格式慣例

### .NET 格式設定

### 組織 using 指令

這些格式規則涉及 *`using`* 指令和 *`Imports`* 陳述式的排序與顯示。

- 將 *`System.*`* *`using`* 指令按字母順序排序，並將其置於其他 using 指令之前。

  ```csharp
  //正確
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Octokit;
  ```

  ```csharp
  //錯誤
  using System.Collections.Generic;
  using Octokit;
  using System.Threading.Tasks;
  ```

- 不要在 using 指令群組之間放置空行。

  ```csharp
  //正確
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Octokit;
  ```

  ```csharp
  //錯誤
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Octokit;
  ```

### C# 格式設定

本節中的格式規則僅適用於 C# 程式碼。

#### 新行選項

這些格式規則涉及使用新行來格式化程式碼。

- 要求所有運算式的大括號必須位於新行（"Allman" 風格）。

  ```csharp
  //正確
  void MyMethod()
  {
      if (...)
      {
          ...
      }
  }
  ```

  ```csharp
  //錯誤
  void MyMethod() {
      if (...) {
          ...
      }
  }
  ```

- 將 else 陳述式置於新行。

  ```csharp
  //正確
  if (...) 
  {
      ...
  }
  else 
  {
      ...
  }
  ```

  ```csharp
  //錯誤
  if (...) {
      ...
  } else {
      ...
  }
  ```

- 將 catch 陳述式置於新行。

  ```csharp
  //正確
  try 
  {
      ...
  }
  catch (Exception e) 
  {
      ...
  }
  ```

  ```csharp
  //錯誤
  try {
      ...
  } catch (Exception e) {
      ...
  }
  ```

- 要求 finally 陳述式在閉合大括號後必須位於新行。

  ```csharp
  //正確
  try 
  {
      ...
  }
  catch (Exception e) 
  {
      ...
  }
  finally 
  {
      ...
  }
  ```

  ```csharp
  //錯誤
  try {
      ...
  } catch (Exception e) {
      ...
  } finally {
      ...
  }
  ```

- 要求物件初始化器的成員必須位於單獨行

  ```csharp
  //正確
  var z = new B()
  {
      A = 3,
      B = 4
  }
  ```

  ```csharp
  //錯誤
  var z = new B()
  {
      A = 3, B = 4
  }
  ```

- 要求匿名型別的成員必須位於單獨行

  ```csharp
  //正確
  var z = new
  {
      A = 3,
      B = 4
  }
  ```

  ```csharp
  //錯誤
  var z = new
  {
      A = 3, B = 4
  }
  ```

- 要求查詢運算式子句的元素必須位於單獨行

  ```csharp
  //正確
  var q = from a in e
          from b in e
          select a * b;
  ```

  ```csharp
  //錯誤
  var q = from a in e from b in e
          select a * b;
  ```

#### 縮排選項

這些格式規則涉及使用縮排來格式化程式碼。

- 縮排 *`switch`* case 內容

  ```csharp
  //正確
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
  //錯誤
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

- 縮排 *`switch`* 標籤

  ```csharp
  //正確
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
  //錯誤
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

- 標籤放置在與當前內容相同的縮排層級

  ```csharp
  //正確
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
  //錯誤
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
  //錯誤
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

#### 間距選項

這些格式規則涉及使用空白字元來格式化程式碼。

- 移除強制轉型與值之間的空格

  ```csharp
  //正確
  int y = (int)x;
  ```

  ```csharp
  //錯誤
  int y = (int) x;
  ```

- 在控制流程陳述式（例如 *`for`* 迴圈）中的關鍵字後放置一個空格字元

  ```csharp
  //正確
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

- Place a space character after the colon for bases or interfaces in a type declaration

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
  int[] x = new int[] { 1,2,3,4,5 };
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

- Leave code block on a single line

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

- Use a descriptive name in PascalCase for the parameter of a generic type, unless a single letter is sufficient and a descriptive name adds no value.

  ```csharp
  //Right
  public interface ISessionChannel<TSession> { /*...*/ }
  public delegate TOutput Converter<TInput, TOutput>(TInput from);
  public class List<T> { /*...*/ }
  ```

- 對於僅包含單一字母型別參數的型別，請使用型別 *`T`* 參數的名稱

  ```csharp
  //正確
  public int IComparer<T>() { return 0; }
  public delegate bool Predicate<T>(T item);
  public struct Nullable<T> where T : struct { /*...*/ }
  ```

- 為型別參數的描述性名稱使用前綴 *`T`*

  ```csharp
  //正確
  public interface ISessionChannel<TSession>
  {
      TSession Session { get; }
  }
  ```

  在型別參數的名稱中指定與其關聯的限制。例如，*`ISession`* 限制參數可以命名為 *`TSession`*。

- 私有與保護類別欄位必須以字首 *`_`* 開頭

  ```csharp
  //正確
  private int _testField;
  protected int _testField;
  ```

  ```csharp
  //錯誤
  private int testField;
  protected int testField;
  ```

- 所有其他程式碼元素（如變數、方法參數及類別欄位，公開欄位除外）均以 camelCase 表示法命名。

  ```csharp
  //正確
  var testVar = new Object();
  public void Foo(int firstParam, string secondParam)
  ```

  ```csharp
  //錯誤
  var TestVar = new Object();
  public void Foo(int FirstParam, string SecondParam)
  ```