namespace Cw7.Data;
using Cw7.Entities;
using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<PC> PCs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
    public DbSet<PCComponent> PCComponents { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<PCComponent>()
            .HasOne(pc => pc.PC)
            .WithMany(p => p.PCComponents)
            .HasForeignKey(pc => pc.PCId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<PCComponent>()
            .HasOne(pc => pc.Component)
            .WithMany(c => c.PCComponents)
            .HasForeignKey(pc => pc.ComponentCode)
            .OnDelete(DeleteBehavior.Cascade);
        
        SeedData(modelBuilder);
    }
    
    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Central Processing Unit" },
            new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Processing Unit" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Random Access Memory" },
            new ComponentType { Id = 4, Abbreviation = "SSD", Name = "Solid State Drive" }
        );
        
        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer 
            { 
                Id = 1, 
                Abbreviation = "INTL", 
                FullName = "Intel Corporation", 
                FoundationDate = new DateTime(1968, 7, 18) 
            },
            new ComponentManufacturer 
            { 
                Id = 2, 
                Abbreviation = "AMD", 
                FullName = "Advanced Micro Devices", 
                FoundationDate = new DateTime(1969, 5, 1) 
            },
            new ComponentManufacturer 
            { 
                Id = 3, 
                Abbreviation = "NVDA", 
                FullName = "NVIDIA Corporation", 
                FoundationDate = new DateTime(1993, 1, 1) 
            },
            new ComponentManufacturer 
            { 
                Id = 4, 
                Abbreviation = "SMSNG", 
                FullName = "Samsung Electronics", 
                FoundationDate = new DateTime(1969, 1, 13) 
            }
        );
        
        modelBuilder.Entity<Component>().HasData(
            new Component 
            { 
                Code = "CPU-I9-14", 
                Name = "Intel Core i9-14900K", 
                Description = "High-end desktop processor with 24 cores",
                ComponentManufacturersId = 1,
                ComponentTypesId = 1
            },
            new Component 
            { 
                Code = "GPU-4090", 
                Name = "NVIDIA GeForce RTX 4090", 
                Description = "Flagship gaming graphics card",
                ComponentManufacturersId = 3,
                ComponentTypesId = 2
            },
            new Component 
            { 
                Code = "RAM-DDR5", 
                Name = "Samsung DDR5 32GB", 
                Description = "High-speed DDR5 memory module",
                ComponentManufacturersId = 4,
                ComponentTypesId = 3
            },
            new Component 
            { 
                Code = "SSD-990P", 
                Name = "Samsung 990 Pro 2TB", 
                Description = "NVMe SSD with high read/write speeds",
                ComponentManufacturersId = 4,
                ComponentTypesId = 4
            }
        );
        
        modelBuilder.Entity<PC>().HasData(
            new PC 
            { 
                Id = 1, 
                Name = "Gaming Beast X", 
                Weight = 12.5, 
                Warranty = 36, 
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), 
                Stock = 5 
            },
            new PC 
            { 
                Id = 2, 
                Name = "Office Mini Pro", 
                Weight = 4.2, 
                Warranty = 24, 
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), 
                Stock = 12 
            },
            new PC 
            { 
                Id = 3, 
                Name = "Workstation Ultra", 
                Weight = 18.0, 
                Warranty = 48, 
                CreatedAt = new DateTime(2026, 3, 20, 10, 0, 0), 
                Stock = 3 
            }
        );
        
        modelBuilder.Entity<PCComponent>().HasData(
            new PCComponent { PCId = 1, ComponentCode = "CPU-I9-14", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "GPU-4090", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "RAM-DDR5", Amount = 2 },
            new PCComponent { PCId = 1, ComponentCode = "SSD-990P", Amount = 2 },
            new PCComponent { PCId = 2, ComponentCode = "RAM-DDR5", Amount = 1 },
            new PCComponent { PCId = 2, ComponentCode = "SSD-990P", Amount = 1 },
            new PCComponent { PCId = 3, ComponentCode = "CPU-I9-14", Amount = 2 },
            new PCComponent { PCId = 3, ComponentCode = "RAM-DDR5", Amount = 4 }
        );
    }
}
