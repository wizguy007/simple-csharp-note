
## Description

Simple note rest api application using c#

## Configure the application

* create a new mysql database
*  appsettings.Development.json and configure the DB connection string

## Running the app

```bash
# enter into the nested simple-note directory
$ cd simple-note

# restore packages
$ dotnet restore

# build app
$ dotnet build

# run database migration
$ dotnet ef database update

# run app
$ dotnet run

```