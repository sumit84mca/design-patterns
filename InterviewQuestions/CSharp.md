# C# Interview Questions

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
HttpWebRequest gives you control over every aspect of the request/response object, like timeouts, cookies, headers, protocols. Another great thing is that HttpWebRequest class does not block the user interface thread. For instance, while you’re downloading a big file from a sluggish API server, your application’s UI will remain responsive.
```csharp
HttpWebRequest http = (HttpWebRequest)WebRequest.Create("http://example.com");
WebResponse response = http.GetResponse();

MemoryStream stream = response.GetResponseStream();
StreamReader sr = new StreamReader(stream);
string content = sr.ReadToEnd();
```

### WebClient
WebClient is a higher-level abstraction built on top of HttpWebRequest to simplify the most common tasks. it requires less code, is easier to use, and you’re less likely to make a mistake when using it.

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
The most important point that you need to keep in mind is that Static is a language feature whereas Singleton is a design pattern. So both belong to two different areas. With this keep in mind let’s discuss the differences between Singleton vs static class in C#.

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


38)what is core middleware work?
39)Liskov Substitution Principle violation example c#?
40)Static classs and static variable when and where store in memory allocation?
41)SQl Server-https://solutioncenter.apexsql.com/recover-sql-server-data-from-delete-truncate-drop/





General Question
1)difference between http and httpclient in angular?
https://blog.fullstacktraining.com/angular-http-vs-httpclient/

2)New version of c#7 feature?

17)c# 7 New future?