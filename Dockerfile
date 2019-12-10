FROM microsoft/dotnet:2.1-sdk
WORKDIR /app
COPY ./bin/Release/netcoreapp2.1/publish/ .
ENTRYPOINT ["dotnet", "vstest", "Laren.E2ETests.dll"]