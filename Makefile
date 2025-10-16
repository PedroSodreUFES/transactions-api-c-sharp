PROJECT = src/CashFlow.API/CashFlow.API.csproj

default: run

run:
	dotnet run --project $(PROJECT)