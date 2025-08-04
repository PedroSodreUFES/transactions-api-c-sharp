# Projeto principal
PROJECT = src/CashFlow.API

# Comando padrão (executado quando você digita apenas "make")
default: run

# Rodar a aplicação
run:
	dotnet run --project $(PROJECT)