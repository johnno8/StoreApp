# StoreApp 
Web application built as a candidate developer assessment.

### Pre-requisites
The application was developed on a Mac using VS Code. For this reason the app as configured connects to MS SQL Server running in a Docker container. To install Docker go to https://docs.docker.com/docker-for-windows/install or https://docs.docker.com/docker-for-mac/install

To install .NET Core for Mac, go to https://www.microsoft.com/net/download/macos

### Clone the repo
Clone the repo:

```sh
$ git clone https://github.com/johnno8/StoreApp.git
```

If Git is not installed locally the contents of the repo can be downloaded as a ZIP file from the 'Clone or Download' tab above.

### Start the DB container
Once Docker is installed, spin up an MS SQL Server container with the following command:

```sh
$ docker run -e 'HOMEBREW_NO_ENV_FILTERING=1' -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=SQLsql111' -p 1433:1433 -h sql1 --name sql1 -d microsoft/mssql-server-linux
```

This command specifies name and password that match the connection string in appsettings.json and maps container port 1433 to host port 1433.

If Docker is not required ie. if MS SQL Server is installed locally replace appsettings.json line 3 with the following:

```sh
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StoreAppDB;Trusted_Connection=True;MultipleActiveResultSets=true"
```

### Start the App

Change to the app root directory and run:

```sh
dotnet run
```

The app will be available at localhost:5000