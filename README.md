
                                                  Portfolio Project
I used dotnet6 ,MVC ->ViewComponents<br>
AJAX ,<br>
N-Tier Architecture ,<br>
EntityFramework (MSSQL),<br>
Identity ,<br>
AutoMapper,<br>
Error Handling Manually<br>
#UI
adsfasdf
asd
fasd
f
asdf
as
df
asd
f
asd
f
ads
fas
df
sda
sdf
asdf

##Some examples

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


I tried SOLİD,DRY and Yagni principles <br>
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
Fluent Validation :
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
Clean code :
```c#
  public bool ContentIsValid(ContentCreateDto dto) => _createContentValidator.Validate(dto).IsValid;

  public bool ContentUpdateIsValid(ContentUpdateDto dto) => _updateContentValidator.Validate(dto).IsValid;
```
