PROJECT = src/CashFlow.API/CashFlow.API.csproj

default: run

run:
	dotnet watch run --project $(PROJECT)