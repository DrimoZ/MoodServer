docker build -t mood/sql_server:v1 .
docker run -p 1433:1433 --name mood_sql_server mood/sql_server:v1