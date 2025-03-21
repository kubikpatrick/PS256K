# PS256K

An open-source photo album manager that runs locally written in C# and ASP.NET Core.

## Installation

First, you will want to download the [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download) (the runtime will not work because you must compile the project by yourself). To fully complete the installation, restart your computer.

Once installed, open a CLI and clone the project with `git` :

```bash
git clone https://github.com/kubikpatrick/PS256K
```

Once downloaded, to apply migrations, you will need EntityFrameworkCore tool :

```bash
dotnet tool install --global dotnet-ef
```

Next, restore the dependencies and apply the database migrations :

```bash
dotnet ef database update
```

To finish, build the project and publish it in its release version : 

```bash
dotnet publish -c Release
```

Once all previous steps done, move to `./PS256K/bin/Release/net9.O/` and execute `PS256K.exe`.
