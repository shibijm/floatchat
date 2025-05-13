build:
	dotnet publish FloatChat.csproj -o out
	cp LICENSE COPYRIGHT NOTICE README.md out/

format:
	dotnet format FloatChat.csproj --verbosity diag --severity info --include-generated --exclude-diagnostics SYSLIB1054
