FROM microsoft/dotnet:2.2-sdk AS build

WORKDIR /bend2
COPY . /bend2
RUN dotnet publish -o output
RUN rm output/appsettings.Development.json

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
COPY --from=build /bend2/output .

EXPOSE 5002
ENV ASPNETCORE_URLS=http://0.0.0.0:5002

CMD ["dotnet", "bend2.dll"]