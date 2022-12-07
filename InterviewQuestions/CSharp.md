# C# Interview Questions

<details>
<summary>Generic collection vs non-generic collections</summary>

## Array
```csharp
string[] names = new string[4];
```
You need to allocate the array to be the right size to start with.
    Problem - suppose you are reading from a file, you'd either need to find out how many names there are before you started, or you'd need to write more complicated code.

## ArrayList
```csharp
ArrayList names = new ArrayList();

foreach(string name in names)
{
    Console.WriteLine(name); //What happens if the ArrayList contains a nonstring?
}
```
There's nothing to stop you from adding a nonstring to the collection. The foreach loop above hides an implicit cast, from object to string, because of the type of the name variable. That cast can fail in the normal way with an InvalidCastException.

## StringCollection
```csharp
StringCollection names = new StringCollection();
```
That's great if you always need only strings. But if you need a collection of some other type, you have to either hope that there's already a suitable collection type in the framework or write one yourself.

## Generic collection List<T>
```csharp
List<string> names = new List<string>();
```
List<string> can be used to replace StringCollection everywhere.
Problems that it'll solve
1. No need to know the size of the collection beforehand, unlike with Arrays.
2. The exposed API uses T everywhere it needs to refer to the element type, so you know that a List<string> will only contain string references. You'll get compile-time error if you try to add anything else, unlike with ArrayList.
3. Use it with any element type without worrying about generating code and managing the result, unlike with StringCollection and similar types.

</details>

<details>
<summary>Anonymous Method in c#?</summary>
As the name suggests, an anonymous method is a method without a name. Anonymous methods in C# can be defined using the delegate keyword and can be assigned to a variable of delegate type.

```csharp
public delegate void Print(int value);
static void Main(string[] args)
{
    Print print = delegate(int val) { 
        Console.WriteLine("Inside Anonymous method. Value: {0}", val); 
    };

    print(100);
}
```
</details>

<details>
<summary>Type parameters and type arguments</summary>

```csharp
public class List<T> //type parameter
{
    //
}
//
List<string> list = new List<string>(); //type arguments
```
</details>

<details>
<summary>Arity of Generic Types and Methods</summary>
The generic arity of a declaration is the number of type parameters it has.
A nongeneric declaration can be think of as the one with generic arity 0.

```csharp
public void Method(){} //generic arity 0
public void Method<T>(){} //generic arity 1
public void Method<T1, T2>(){} //generic arity 2

//Although the generic arity keeps declarations separate, type parameter names don't.
public void Method<TFirst>(){}
public void Method<TSecond>(){} //compile-time error; can't overload solely by type parameter name
public void Method<T, T>(){} //compile-time error; duplicate type parameter T
```
</details>

<details>
<summary>What can be generic?</summary>
Can
1. Class
2. Struct
3. Interface
4. Delegate
5. Methods
6. Nested types

Can't
1. Enum
2. Fields
3. Properties
4. Indexers
5. Constructors
6. Events
7. Finalizers
</details>

<details>
<summary>Type constraints</summary>
When a type parameter is declared by a generic type or method, it can also specify type constraints that restrict which types can be provided as type arguments.

```csharp
//problem
static void PrintItems(List<IFormattable> items){}
//you couldn't pass a List<decimal> to it, for ex, even though decimal implements IFormattable; a List<decimal> isn't convertible to List<IFormattable>.

//solution
static void PrintItems<T>(List<T> items) where T : IFormattable {}
//The way we've constrained T here doesn't just change which values can be passed to the method; it also changes what you can do with a value of type T within the method. The compiler knows that T implements IFormattable, so it allows IFormattable.ToString(string, IFormatProvider) method to be called on any T value.
```
Following type constraints are available
1. Reference type constraint - where T : class (it can be any reference type, including interfaces and delegates.)
2. Value type constraint - where T : struct (must be a non nullable value type struct, or an enum)
3. Constructor constraint - where T : new() (type argument must have a public parameterless constructor. This enables use of new T() within the body of the code)
4. Conversion constraint - where T : SomeType (can be a class, interface, or another type parameter)
- where T : Control
- where T : IFormattable
- where T1 : T2
</details>

<details>
<summary>Difference between hashtable and dictionary?</summary>

## Dictionary
1. Dictionary is generic type Dictionary<TKey,TValue>
2. Dictionary class is a strong type < TKey,TValue > Hence, you must specify the data types for key and value.
3. There is no need of boxing/unboxing.
4. When you try to access non existing key dictionary, it gives runtime error.
5. Dictionary maintains an order of the stored values.
6. There is no need of boxing/unboxing, so it is faster than Hashtable.

## Hashtable
1. Hashtable is non-generic type.
2. Hashtable is a weakly typed data structure, so you can add keys and values of any object type.
3. Values need to have boxing/unboxing.
4. When you try to access non existing key Hashtable, it gives null values.
5. Hashtable never maintains an order of the stored values.
6. Hashtable needs boxing/unboxing, so it is slower than Dictionary.
</details>

<details>
<summary>Difference btween var and dynamic in c#?</summary>
var is Implicitly Typed Local Variables and actual type is resolved by the value assigned to the variable at time of initialization.
dynamic is object whose actual type is not checked at compile time. Hence the intellisense does not have any information about its properites. Also there will not be any compile time error in case of accessing any not existing member.
</details>

<details>
<summary>difference between webclient and httpclient in c#?</summary>

### HttpWebRequest 
HttpWebRequest gives you control over every aspect of the request/response object, like timeouts, cookies, headers, protocols. Another great thing is that HttpWebRequest class does not block the user interface thread. For instance, while you�re downloading a big file from a sluggish API server, your application�s UI will remain responsive.
```csharp
HttpWebRequest http = (HttpWebRequest)WebRequest.Create("http://example.com");
WebResponse response = http.GetResponse();

MemoryStream stream = response.GetResponseStream();
StreamReader sr = new StreamReader(stream);
string content = sr.ReadToEnd();
```

### WebClient
WebClient is a higher-level abstraction built on top of HttpWebRequest to simplify the most common tasks. it requires less code, is easier to use, and you�re less likely to make a mistake when using it.

```csharp
var client = new WebClient();
var text = client.DownloadString("http://example.com/page.html");
```
### HttpClient
HttpClient provides powerful functionality with better syntax support for newer threading features, e.g. it supports the await keyword. It also enables threaded downloads of files with better compiler checking and code validation
```csharp
HttpResponseMessage response = await client.GetAsync(uri);
    if (response.IsSuccessStatusCode)
    {
        author = await response.Content.ReadAsAsync<Author>();
    }
    return author;
```
</details>

<details>
<summary>what is statd for ?? in c#</summary>

### ??
The null-coalescing operator ?? returns the value of its left-hand operand if it isn't null; otherwise, it evaluates the right-hand operand and returns its result. The ?? operator doesn't evaluate its right-hand operand if the left-hand operand evaluates to non-null.

### ??=
The null-coalescing assignment operator ??= assigns the value of its right-hand operand to its left-hand operand only if the left-hand operand evaluates to null. The ??= operator doesn't evaluate its right-hand operand if the left-hand operand evaluates to non-null.
</details>

<details>
<summary>Difference between singlton and static?</summary>
The most important point that you need to keep in mind is that Static is a language feature whereas Singleton is a design pattern. So both belong to two different areas. With this keep in mind let�s discuss the differences between Singleton vs static class in C#.

1. We cannot create an instance of a static class in C#. But we can create a single instance of a singleton class and then can reuse that singleton instance.
2. When the compiler compiles the static class then internally it treats the static class as an abstract and sealed class. This is the reason why neither we create an instance nor extend a static class in C#.
3. The Singleton class constructor is always marked as private. This is the reason why we cannot create an instance from outside the singleton class. It provides either public static property or a public static method whose job is to create the singleton instance only once and then return that singleton instance each and every time when we called that public static property/method from outside the singleton class.
4. A Singleton class can be initialized lazily or can be loaded automatically by CLR (Common Language Runtime) when the program or namespace containing the Singleton class is loaded. whereas a static class is generally initialized when it is first loaded for the first time and it may lead to potential classloader issues.
5. It is not possible to pass the static class as a method parameter whereas we can pass the singleton instance as a method parameter in C#.
6. In C#, it is possible to implement interfaces, inherit from other classes and allow inheritance with the Singleton class. These are not possible with a static class. So the Singleton class is more flexible as compared to static classes.
7. We can clone the Singleton class object whereas it is not possible to clone a static class. It is possible to dispose of the objects of a singleton class whereas it is not possible to dispose of a static class.
8. We cannot implement the Dependency Injection design pattern using Static class because the static class is not interface-driven.
9. Singleton means a single object across the application lifecycle, so the scope is at the application level. As we know the static class does not have any Object pointer, so the scope is at the App Domain level.
</details>


<details>
<summary>Dependency Injection vs Service Locator?</summary>
To implement the IoC, you have the choice of two main patterns: Service Locator and Dependency Injection. 
The Service Locator allows you to "resolve" a dependency within a class and the Dependency Injection allows you to "inject" a dependency from outside the class.

Service Locator Example:<br>
Create a Singleton or Static Class that contains a method which can retunr object of requested type. This Singleton or Static class will act as Service Locator.

DI Example:<br>
Default IoC provided by the .net Core. Here all classes get the required object in its construtor fom outside by framework/IoC.
</details>

40)Static classs and static variable when and where store in memory allocation?
41)SQl Server-https://solutioncenter.apexsql.com/recover-sql-server-data-from-delete-truncate-drop/





General Question
1)difference between http and httpclient in angular?
https://blog.fullstacktraining.com/angular-http-vs-httpclient/

2)New version of c#7 feature?

17)c# 7 New future?