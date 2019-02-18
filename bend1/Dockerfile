FROM microsoft/dotnet:2.2-sdk AS build

WORKDIR /bend1
COPY . /bend1
RUN dotnet build -o output

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
COPY --from=build /bend1/output .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://0.0.0.0:5000

CMD ["dotnet", "bend1.dll"]