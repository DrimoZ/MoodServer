# Utilisez l'image SQL Server officielle
FROM mcr.microsoft.com/mssql/server:2022-latest

# Définissez la variable d'environnement pour le mot de passe SA
ENV SA_PASSWORD "yourStrong(!)Password"
ENV MSSQL_SA_PASSWORD "yourStrong(!)Password"
ENV ACCEPT_EULA Y

# Copiez le script SQL pour créer la base de données, la table et les données
COPY setup.sql /setup.sql

RUN ( /opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'yourStrong(!)Password' -i "./setup.sql" \
    && pkill sqlservr

WORKDIR /uploads/image

# Exposez le port SQL Server (par défaut 1433)
EXPOSE 1433

#docker build -t school/sql_server_with_data_playground:v1 .
#docker run --rm -p 1433:1433 school/sql_server_with_data_playground:v1
