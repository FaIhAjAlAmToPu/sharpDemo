## One-To-One
**PrincipalSide**: Independent entity, defines the relationship.  
**DependentSide**: Dependent entity, primary key doubles as foreign key to PrincipalSide.

```csharp
public class PrincipalSide
{
    public int PrincipalSideId { get; set; } // Primary key
    public DependentSide? DependentSide { get; set; } // Navigation property
}

public class DependentSide
{
    public int PrincipalSideId { get; set; } // Primary key and foreign key
    public PrincipalSide PrincipalSide { get; set; } // Navigation property
    public string Details { get; set; } // Example property
}
```

**Fluent API**:
```csharp
modelBuilder.Entity<PrincipalSide>()
    .HasKey(p => p.PrincipalSideId);

modelBuilder.Entity<DependentSide>()
    .HasKey(d => d.PrincipalSideId);

modelBuilder.Entity<DependentSide>()
    .HasOne(d => d.PrincipalSide)
    .WithOne(p => p.DependentSide)
    .HasForeignKey<DependentSide>(d => d.PrincipalSideId);
```

## One-To-Many
**OneSide**: One instance in the relationship.  
**ManySide**: Multiple instances associated with one OneSide.

```csharp
public class OneSide
{
    public int OneSideId { get; set; } // Primary key
    public ICollection<ManySide> ManySides { get; set; } = new List<ManySide>(); // Collection navigation
}

public class ManySide
{
    public int ManySideId { get; set; } // Primary key
    public int OneSideId { get; set; } // Foreign key
    public OneSide OneSide { get; set; } // Navigation property
}
```

**Fluent API**:
```csharp
modelBuilder.Entity<OneSide>()
    .HasKey(o => o.OneSideId);

modelBuilder.Entity<ManySide>()
    .HasKey(m => m.ManySideId);

modelBuilder.Entity<ManySide>()
    .HasOne(m => m.OneSide)
    .WithMany(o => o.ManySides)
    .HasForeignKey(m => m.OneSideId);
```

## Many-To-Many
**SideA**: One side of the many-to-many relationship.  
**SideB**: Other side of the many-to-many relationship.  
**SideASideB**: Junction table breaking the many-to-many into two one-to-many relationships.

```csharp
public class SideA
{
    public int SideAId { get; set; } // Primary key
    public List<SideASideB> SideASideBs { get; set; } = new List<SideASideB>(); // Collection navigation
}

public class SideB
{
    public int SideBId { get; set; } // Primary key
    public List<SideASideB> SideASideBs { get; set; } = new List<SideASideB>(); // Collection navigation
}

public class SideASideB
{
    public int SideASideBId { get; set; } // Primary key
    public int SideAId { get; set; } // Foreign key
    public SideA SideA { get; set; } // Navigation property
    public int SideBId { get; set; } // Foreign key
    public SideB SideB { get; set; } // Navigation property
}
```

**Inside Overridden OnModelCreating inside your DbContext class**:
```csharp
modelBuilder.Entity<SideA>()
    .HasKey(a => a.SideAId);

modelBuilder.Entity<SideB>()
    .HasKey(b => b.SideBId);

// Using single primary key
modelBuilder.Entity<SideASideB>()
    .HasKey(j => j.SideASideBId);

// Optional: Composite key instead
// modelBuilder.Entity<SideASideB>()
//     .HasKey(j => new { j.SideAId, j.SideBId });

modelBuilder.Entity<SideASideB>()
    .HasOne(j => j.SideA)
    .WithMany(a => a.SideASideBs)
    .HasForeignKey(j => j.SideAId);

modelBuilder.Entity<SideASideB>()
    .HasOne(j => j.SideB)
    .WithMany(b => b.SideASideBs)
    .HasForeignKey(j => j.SideBId);
```