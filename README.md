
                                                  Portfolio Project
![](https://github.com/ekremgunes/PortfolioProject/blob/master/panel_111.gif)
![](https://github.com/ekremgunes/PortfolioProject/blob/master/panel_222.gif)
```
I used these technologies dotnet6 ,MVC -> ViewComponents ,
AJAX ,
N-Tier Architecture ,
EntityFramework (MSSQL) ,
Identity ,
AutoMapper ,
I tried SOLİD,DRY and Yagni principles ,
Error Handling Manually 
`app.UseStatusCodePagesWithReExecute("/Error/Error", "?code={0}"); // status page`.
```
<br>


> FluentAPI : 

```c#
    public class ImageConfigurations : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.imgPath).IsRequired();
            builder.HasOne(x => x.contentDetail).WithMany(x => x.images).HasForeignKey(x => x.contentDetailId);
        }
    }
```
> Clean code :

```c#
  public bool ContentIsValid(ContentCreateDto dto) => _createContentValidator.Validate(dto).IsValid;

  public bool ContentUpdateIsValid(ContentUpdateDto dto) => _updateContentValidator.Validate(dto).IsValid;
```

> Unit Of Work : 

```c#
    public class Uow : IUow 
    {
        private readonly PortfolioContext _context;

        public Uow(PortfolioContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
```
> Fluent Validation :
```c#
    public class ServiceUpdateDtoValidator : AbstractValidator<ServiceUpdateDto>
    {
        public ServiceUpdateDtoValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("data id not found");
            RuleFor(x => x.serviceName).NotEmpty().WithMessage("Service name can't be empty");
            RuleFor(x => x.rank).LessThan((short)101).WithMessage("Rank max value is 100");
            RuleFor(x => x.rank).GreaterThan((short)0).WithMessage("Rank min value is 1");
        }
    }
```

