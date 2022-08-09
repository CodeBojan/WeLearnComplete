# WeLearn

## EF Scripts

### Set ASPNETCORE_ENVIRONMENT

```ps
$env:ASPNETCORE_ENVIRONMENT="Local"
```

### Add Migration

```ps
Add-Migration Add_InitialCreate -StartupProject WeLearn.IdentityServer\WeLearn.IdentityServer -Project WeLearn.Data\WeLearn.Data -OutputDir Migrations -Context WeLearn.Data.Persistence.ApplicationDbContext
```

### Update Database

```ps
Update-Database -StartupProject WeLearn.IdentityServer\WeLearn.IdentityServer -Project WeLearn.Data -Context WeLearn.Data.Persistence.ApplicationDbContext
```

### Remove Migration

```ps
Remove-Migration -StartupProject WeLearn.Api -Project WeLearn.Data -Context WeLearn.Data.Persistence.ApplicationDbContext
```
