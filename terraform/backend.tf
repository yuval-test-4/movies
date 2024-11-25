terraform {
  backend "s3" {
    bucket = "terraform-state-demonstration"
    key    = "development/movies"
    region = "us-east-1"
  }
}