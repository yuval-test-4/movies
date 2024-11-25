resource "random_password" "movies_secret_password" {
  length  = 20
  special = false
}

resource "aws_secretsmanager_secret" "secrets_movies" {
  name = "movies_secrets"
}

resource "aws_secretsmanager_secret_version" "secrets_version_movies" {
  secret_id     = aws_secretsmanager_secret.secrets_movies.id
  secret_string = jsonencode({
    BCRYPT_SALT       = "10"
    JWT_EXPIRATION    = "2d"
    JWT_SECRET_KEY    = random_password.movies_secret_password.result
    DB_URL     = "Server=${module.rds_movies.db_instance_address};Port=5432;Database=${module.rds_movies.db_instance_name};User Id=${module.rds_movies.db_instance_username};Password=${random_password.movies_database_password.result};"
  })
}
