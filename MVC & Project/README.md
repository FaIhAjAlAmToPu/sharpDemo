## Visit Microsoft Learn
- [Create a MVC Web App project](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-8.0)

## Steps
### Create New Project
![alt text](image.png)
### Search and Select MVC Web App (C#)
![alt text](image-1.png)
### Write Your Choice of Project Name
![alt text](image-2.png)
### Keep as it is
![alt text](image-3.png)
### Run it
![alt text](image-4.png)
### Got this?
![alt text](image-6.png)

## Database Connection
### Visit [Supabase](https://supabase.com/) and Create Project->Database(please remember the password of your database)
### Select Connect
![alt text](image-7.png)
### View Parameters of Session Pooler
You will need these in future
- host

- port

- database

- user



### Goto appsettings.json
![alt text](image-8.png)
### Add you connection string at the top
```sh
"ConnectionStrings": {
    "SupabaseConnection": "Host=<your_supabase_host>;Port=<your_supabase_port>;Database=<your_database_name>;Username=<your_username>;Password=<your_password>"
}
```

**Remember to replace the string including <>**
### appsettings.json should look like this (replace the parameters with your database credentials)
![alt text](image-9.png)

<!-- ### goto nuget package manager
![alt text](image-10.png)

### goto browse and search for **Npgsql.EntityFrameworkCore.PostgreSQL**
![alt text](image-11.png)
### install it (apply -> I accept)
![alt text](image-12.png)

### Do same for **Microsoft.EntityFrameworkCore.Tools**
![alt text](image-16.png) -->


### Install Required Packages
![alt text](image-18.png)

Run these in the **Package Manager Console**:

```bash
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL
```

### Create ***Data/ApplicationDbContext.cs (ApplicationDbContext.cs inside Data folder)***
![alt text](image-20.png)
### Then inherit from DbContext
![alt text](image-14.png)
### Data/ApplicationDbContext.cs should look like this
```csharp
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options){ }
}

```


### goto Program.cs
![alt text](image-13.png)

### add dbcontext (after the line that initializes builder)
```sh
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection")));
```
### final Program.cs
![alt text](image-15.png)

## MVC
![alt text](image-17.png)

### Try to Create Some models
**Inside ```Models``` folder**
### update **ApplicationDbContext.cs**
```csharp
public DbSet<ModelName> PropName { get; set; }
```
> **ModelName** → The class that represents your database table.  
**PropName** → The name used in `DbContext` to access that table from code.

### To migrate and see updated tables
- Tools → NuGet Package Manager → Package Manager Console
```sh
Add-Migration Init
Update-Database
```

### See the result in supabase
You can access the ``Table Editor`` and ``SQL Editor`` from the **navigation bar** on the **left side** of the screen