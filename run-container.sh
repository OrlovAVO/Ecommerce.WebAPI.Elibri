#!/bin/bash
echo "Waiting for PostgreSQL to start..."

# Function to check PostgreSQL availability
wait_for_postgres() {
    until PGPASSWORD="pass" psql -h "postgres" -U "postgres" -d "Elibri" -c '\q'; do
        >&2 echo "PostgreSQL is unavailable - waiting"
        sleep 1
    done
}

wait_for_postgres

echo "PostgreSQL is up and running"

PG_HOST="postgres"
PG_USER="postgres"
PG_DB="Elibri"
BACKUP_DIR="/initdb.d/"

# Check if the backup directory exists
if [ -d "$BACKUP_DIR" ]; then
    # Restoring the database from uncompressed backup files
    echo "Restoring the database from uncompressed backup files..."
    cat "$BACKUP_DIR"/* | psql -h "$PG_HOST" -U "$PG_USER" -d "$PG_DB" || { echo "Error restoring the database." && exit 1; }
else
    echo "Backup directory '$BACKUP_DIR' not found. Skipping database restoration."
fi

echo "Applying EF Core migrations..."
dotnet ef database update || { echo "Error applying EF Core migrations." && exit 1; }

exec dotnet Elibri.Hosting.dll