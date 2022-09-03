# WeLearn

## Set ASPNETCORE_ENVIRONMENT

```ps
$env:ASPNETCORE_ENVIRONMENT="Local"
```

## Identity-Server EF Scripts

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
Remove-Migration -StartupProject WeLearn.IdentityServer\WeLearn.IdentityServer -Project WeLearn.Data\WeLearn.Data -Context WeLearn.Data.Persistence.ApplicationDbContext
```

### Building IdentityServer

```bash
docker build . -t registry.redctesting.ddnsfree.com/wl-is -f .\WeLearn.IdentityServer\WeLearn.IdentityServer\Dockerfile
docker push registry.redctesting.ddnsfree.com/wl-is
```
