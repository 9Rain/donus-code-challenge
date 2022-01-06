FROM mcr.microsoft.com/mssql/server:2017-latest AS build
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Ibank#2021
ENV MSSQL_PID=Express

WORKDIR /tmp
COPY IBankDB/IBankDB.sql .

RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P Ibank#2021 -i /tmp/IBankDB.sql \
    && pkill sqlservr

FROM mcr.microsoft.com/mssql/server:2017-latest AS release
ENV ACCEPT_EULA=Y
COPY --from=build /var/opt/mssql/data /var/opt/mssql/data