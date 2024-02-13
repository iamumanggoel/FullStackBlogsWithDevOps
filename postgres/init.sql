-- Create database
CREATE DATABASE BlogDb;

-- Connect to the database
\c BlogDb;

-- Create tables for BlogServices microservice
CREATE TABLE IF NOT EXISTS "blogs" (
    "BlogId" UUID PRIMARY KEY,
    "Title" VARCHAR(100) NOT NULL,
    "ShortDescription" VARCHAR(500) NOT NULL,
    "Content" TEXT NOT NULL,
    "FeaturedImageUrl" TEXT NOT NULL,
    "PublishedDate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "Author" VARCHAR(50) NOT NULL,
    "UserEmail" TEXT NOT NULL
);

-- Create tables for CommentServices microservice
CREATE TABLE IF NOT EXISTS "Comments" (
    "CommentId" UUID PRIMARY KEY,
    "CommentText" VARCHAR(100) NOT NULL,
    "UserEmail" TEXT NOT NULL,
    "BlogId" UUID NOT NULL,
    "Name" TEXT NOT NULL
);

-- Create tables for UserServices microservice
CREATE TABLE IF NOT EXISTS "Users" (
    "Email" TEXT PRIMARY KEY,
    "FirstName" VARCHAR(50) NOT NULL,
    "LastName" VARCHAR(50) NOT NULL,
    "PhoneNumber" VARCHAR(15) NOT NULL,
    "DOB" TIMESTAMP WITH TIME ZONE NOT NULL,
    "Password" TEXT NOT NULL
);

