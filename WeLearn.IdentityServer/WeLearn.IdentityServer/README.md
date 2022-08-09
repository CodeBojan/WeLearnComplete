# WeLearn.IdentityServer

## Installing

- Clone the repository
- Install the version of the .NET SDK specified in WeLearn.IdentityServer.csproj
- Install [Excubo.WebCompiler](https://github.com/excubo-ag/WebCompiler)
- Install [Node.Js (>=18.0)](https://nodejs.org/en/)
- Install [Yarn](https://yarnpkg.com/)
- Run `yarn install`

## Developing

Run the following in a terminal shell

```pwsh
dotnet watch
```

For runtime Tailwind and SCSS compilation, also run

```pwsh
dotnet watch msbuild /t:WebCompiler /t:PostCSS
```

## Running

TODO
