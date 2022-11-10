<details>
<summary>Left join in Linq?</summary>

In LINQ left outer join can be performed by calling the **DefaultIfEmpty** method on the results of a group join.

```csharp
var query =
    from person in people
    join pet in pets on person equals pet.Owner into gj
    from subpet in gj.DefaultIfEmpty()
    select new
    {
        person.FirstName,
        PetName = subpet?.Name ?? string.Empty
    };
```

</details>

<details>
<summary>Difference between LAzy,Earger and Explict Loading</summary>

**Eager loading** means that the related data is loaded from the database as part of the initial query.<br>

```csharp
using (var context = new BloggingContext())
{
    var blogs = context.Blogs
        .Include(blog => blog.Posts)
        .ThenInclude(post => post.Author)
        .ToList();
}
```
**Explicit loading** means that the related data is explicitly loaded from the database at a later time. You can explicitly load a navigation property via the DbContext.Entry(...) API.<br>
```csharp
using (var context = new BloggingContext())
{
    var blog = context.Blogs
        .Single(b => b.BlogId == 1);

    context.Entry(blog)
        .Collection(b => b.Posts)
        .Load();

    context.Entry(blog)
        .Reference(b => b.Owner)
        .Load();
}
```
**Lazy loading** means that the related data is transparently loaded from the database when the navigation property is accessed. The simplest way to use lazy-loading is by installing the Microsoft.EntityFrameworkCore.Proxies package and enabling it with a call to UseLazyLoadingProxies. For example:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseLazyLoadingProxies()
        .UseSqlServer(myConnectionString);
```
EF Core will then enable lazy loading for any navigation property that can be overridden--that is, it must be virtual and on a class that can be inherited from.
```csharp
public class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Post> Posts { get; set; }
}
```
</details>