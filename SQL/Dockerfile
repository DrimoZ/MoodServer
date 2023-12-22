FROM mcr.microsoft.com/mssql/server:2022-latest

ENV MSSQL_SA_PASSWORD "yourStrong(!)Password"
ENV ACCEPT_EULA Y

COPY ./sqlfiles/setup.sql /setup.sql
COPY ./sqlfiles/image.sql /image.sql
COPY ./sqlfiles/other.sql /other.sql


RUN ( /opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'yourStrong(!)Password' -i "./setup.sql" \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'yourStrong(!)Password' -i "./image.sql" \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'yourStrong(!)Password' -i "./other.sql" \
    && pkill sqlservr


# Exposez le port SQL Server (par défaut 1433)
EXPOSE 1433

#docker build -t school/sql_server_with_data_playground:v1 .
#docker run --rm -p 1433:1433 school/sql_server_with_data_playground:v1
